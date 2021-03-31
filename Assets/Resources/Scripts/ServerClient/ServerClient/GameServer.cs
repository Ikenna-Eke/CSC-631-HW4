using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";

            Server.Start(2, 30033); // Start(max players, port)

            Console.ReadKey();
        }
    }
}