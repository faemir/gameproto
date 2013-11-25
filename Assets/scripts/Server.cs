using UnityEngine;
using System.Collections;

public class Server : Singleton<Server> 
{

	private string registeredServerName = "DUMMYSERVERNAME";
	private string serverName = "Default Server Name";


	void Awake() 
	{
		serverName = PlayerPrefs.GetString("serverName");
		if (serverName == "") serverName = "Default Server Name";

		Network.InitializeServer(16, 25002, false);
		MasterServer.RegisterHost(registeredServerName, serverName);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
