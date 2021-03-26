using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer{
    class Client{
        public int ClientID;
        public TCP tcp;
        public int id;
        public static int buffer = 4096; //4MB buffer

        public Client(int _ClientID){
            id = _ClientID;
            tcp = new TCP(id);
        }

        public class TCP{
            public TcpClient socket;
            private readonly int id; // Client ID
            private NetworkStream stream;
            private byte[] receiveBuffer;
            private Packet receivedData;
    
            public TCP(int _id){
                id = _id;
            }

            public void Connect(TcpClient _socket){
                socket = _socket;
                socket.ReceiveBufferSize = buffer;
                socket.SendBufferSize = buffer;

                stream = socket.GetStream();
                

                receivedData = new Packet();
                receiveBuffer = new byte[buffer];

                stream.BeginRead(receiveBuffer, 0, buffer,  ReceiveCallback, null);

                ServerSend.Welcome(id, "THE WELCOME METHOD IS WORKING"); // !!!! we probably won't need this, this is just to test by calling the method to send a message
            }

            public void SendData(Packet _packet){ // !!!!!! I don't know why Packet is giving an error his code looks like this with no extra imports !!!!!!!
                try{
                    if(socket != null){
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }catch(Exception _ex){
                    Console.WriteLine("Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult _result){
                try{
                    int _byteLength = stream.EndRead(_result);
                    if(_byteLength <= 0){
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    receivedData.Reset(HandleData(_data));
                    stream.BeginRead(receiveBuffer, 0, buffer, ReceiveCallback, null);
                }catch(Exception _ex){
                    Console.WriteLine("Error receiving TCP data, ERROR: {_ex}");
                }
            }

            private bool HandleData(byte[] _data)
        {
            int _packetLength = 0;

            receivedData.SetBytes(_data); // !!!!! I don't know why SetBytes is throwing an error

            if(receivedData.UnreadLength() >= 4)
            {
                _packetLength = receivedData.ReadInt();
                if(_packetLength <= 0)
                {
                    return true;
                }
            }


            // !!!!! Bunch of errors here
            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetID = _packet.ReadInt();
                        Server.packetHandlers[_packetID](id, _packet);
                    }
                });

                _packetLength = 0;

                if(receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if(_packetLength <= 1)
            {
                return true;
            }

            return false;
        }
        }
    }
}