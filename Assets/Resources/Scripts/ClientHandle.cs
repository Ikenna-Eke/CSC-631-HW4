using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        // !!!! AGAIN don't know why there are errors his code looks exactly like this
        string _msg = _packet.ReadString();
        int _localID = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.localID = _localID;
        ClientSend.WelcomeReceived();
    }
}
