using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer{
    class Client{
        public int ClientID;
        public TCP tcp;
        public static int buffer = 4096; //4MB buffer

        public Client(int _ClientID){
            id = _ClientID;
            tcp = new TCP(id);
        }

        public class TCP{
            public TcpClient socket;
            private readonly int CID; // Client ID
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(int _id){
                id = _id;
            }

            public void Connect(TcpClient _socket){
                socket = _socket;
                socket.ReceiveBufferSize = buffer;
                socket.SendBufferSize = buffer;

                stream = socket.GetStream();

                receiveBuffer = new byte[buffer];

                stream.BeginRead(receiveBuffer, 0, buffer,  ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult _result){
                try{
                    int _byteLength = stream.EndRead(_result);
                    if(_byteLength <= 0){
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    stream.BeginRead(receiveBuffer, 0, buffer, ReceiveCallback, null);
                }catch(Exception _ex){
                    Console.WriteLine("Error receiving TCP data, ERROR: {_ex}");
                }
            }
        }
    }
}