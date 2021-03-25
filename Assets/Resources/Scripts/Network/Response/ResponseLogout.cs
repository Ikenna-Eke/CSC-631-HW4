using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseLogout : ExtendedEventArgs
{
    public int user_id { get; set; } // Who is logging out

	public ResponseLeaveEventArgs()
	{
		event_id = Constants.SMSG_LOGOUT;
	}
}

public class ResponseLeaveResponse : NetworkResponse
{

}
