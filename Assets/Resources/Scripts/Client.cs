using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int buffer = 4096; // Buffer size 4MB

    public string ip = "127.0.0.1"; // localhost
    public int port = 30033;
    public int localID = 0;
    public TCP tcp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {   
            //Making sure only one instance of the client class exists
            Debug.Log("Instance already exists");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }

    public void ConnectToServer()
    {
        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        public byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = buffer,
                SendBufferSize = buffer
            };

            receiveBuffer = new byte[buffer];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                return;
            }

            stream = socket.GetStream();

            stream.BeginRead(receiveBuffer, 0, buffer, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                stream.BeginRead(receiveBuffer, 0, buffer, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("Error receiving TCP data, ERROR: {_ex}");
            }
        }
    }
}
