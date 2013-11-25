using UnityEngine;
using System.Collections;

public class Server : Singleton<Server>
{

	private string registeredServerName = "DUMMYSERVERNAME";
	private string serverName = "Default Server Name";

	void Awake ()
	{
		serverName = PlayerPrefs.GetString ("serverName");
		if (serverName == "")
				serverName = "Default Server Name";

		Network.InitializeServer (16, 25002, false);
		MasterServer.RegisterHost (registeredServerName, serverName);
	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log ("Server registration successful.");
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError err)
	{

	}

	void OnPlayerConnected(NetworkPlayer player)
	{

	}

	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnApplicationQuit()
	{
		Network.Disconnect(200);
		MasterServer.UnregisterHost();
	}
}
