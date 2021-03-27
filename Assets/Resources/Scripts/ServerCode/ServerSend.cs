using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer{
    class ServerSend{
        public static void SendTCPDataint (int _toClient, Packet _packet){
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        public static void SendUDPData(int _toClient, Packet _packet){
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet){
            _packet.WriteLength();
            for(int i = 1; i <= Server.maxPlayers; i++){
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet){
            _packet.WriteLength();
            for(int i = 1; i <= Server.maxPlayers; i++){
                if(i != _exceptClient){
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet){
            _packet.WriteLength();
            for(int i = 1; i <= Server.maxPlayers; i++){
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet){
            _packet.WriteLength();
            for(int i = 1; i <= Server.maxPlayers; i++){
                if(i != _exceptClient){
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        public static void Welcome(int _toClient, string _msg){ // (who to send the message to, what the message is)
            using (Packet _packet = new Packet((int)ServerPackets.welcome)){
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPDataint(_toClient, _packet);
            }
        }

        public static void UDPTest(int _toClient){
            using(Packet _packet = new Packet((int)ServerPackets.udpTest)){
                _packet.Write("UDP sending and receiving works!");

                SendUDPData(_toClient, _packet);
            }
        }

    }
}