using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Numerics;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _localID = _packet.ReadInt(); // AKA _myId

        Debug.Log($"Message from server: {_msg}");
        Client.instance.localID = _localID;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void UDPTest(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log("Received packet via UDP. Message: {_msg}");
        ClientSend.UDPTestReceived();
    }
}
