using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseLogin : ExtendedEventArgs
{
    public short status { get; set; } // Success or failure status of process
    public int user_id { get; set; } // Player's UID
    public int op_id { get; set; } // Opponent's UID
    public string op_name { get; set; } // Opponent's name
    public bool op_ready { get; set; } // Is opponent ready

    public ResponseLogin()
    {
        event_id = Constants.SMSG_LOGIN;
    }
}

public class ResponseLoginResponse : NetworkResponse
{
    private short status;
    private int user_id;
    private int op_id;
    private string op_name;
    private bool op_ready;

    public ResponseLoginResponse()
    {

    }

    public override void parse()
    {
        status = DataReader.ReadShort(dataStream);
        if (status == 0)
        {
            user_id = DataReader.ReadInt(dataStream);
            op_id = DataReader.ReadInt(dataStream);
            op_name = DataReader.ReadString(dataStream);
            op_ready = DataReader.ReadBool(dataStream);
        }
    }

    public override ExtendedEventArgs process()
    {
        ResponseLogin args = new ResponseLogin
        {
            status = status
        };

        if(status == 0)
        {
            args.user_id = user_id;
            args.op_id = op_id;
            args.op_name = op_name;
            args.op_ready = op_ready;
        }

        return args;
    }
}
