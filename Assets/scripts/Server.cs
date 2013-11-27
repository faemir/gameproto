using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class Server : Singleton<Server>
{

	public string registeredServerName
	{
		get; private set;
	}
	private string serverName = "Default Server Name";


	void Awake()
	{
		networkView.stateSynchronization = NetworkStateSynchronization.Off;
		registeredServerName = "DUMMYSERVERNAME";
	}

	public void StartServer()
	{
		if (Network.isServer || Network.isClient)
		{
			Debug.Log("StartServer call whilst already connected." + 
			          "Destroy current connection.");
			Network.Disconnect(200);
			MasterServer.UnregisterHost();
		}

		serverName = PlayerPrefs.GetString ("serverName");
		if (serverName == "")
			serverName = "Default Server Name";
		
		Network.InitializeServer (16, 25002, false);
		MasterServer.RegisterHost (registeredServerName, serverName);
		Debug.Log ("Server started.");
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
