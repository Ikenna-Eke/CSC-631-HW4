using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer{
    class Server{
        public static int maxPlayers {get; private set;}
        public static int port {get; private set;}
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        private static TcpListener TCPListener;

        public static void Start(int _maxPlayers, int _port){
            maxPlayers = _maxPlayers;
            port = _port;

            Console.WriteLine("Starting server...");

            InitializeServerData();

            TCPListener = new TcpListener(IPAddress.Any, port);
            TCPListener.Start();
            TCPListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null); // Originally TCPConnectionCallback

            Console.WriteLine("Server started on {port}");
        }

        private static void TCPConnectCallback(IAsyncResult _result){
            TcpClient _client = TCPListener.EndAcceptTcpClient(_result);
            TCPListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null); // Originally TCPConnectionCallback

            Console.WriteLine("Incoming connetion from {_client.Client.RemoteEndPoint}...");

            for(int i = 1; i <= maxPlayers; i++){
                if(clients[i].tcp.socket == null){
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine("{_client.Client.RemoteEndPoint} failed to connect: Server full");
        }

        private static void InitializeServerData(){
            for(int i = 1; i <= maxPlayers; i++){
                clients.Add(i, new Client(i));
            }
        }
    }
}