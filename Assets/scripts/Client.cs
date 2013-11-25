using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : Singleton<Client> 
{
	private List<string> playerNames = new List<string>();

	public void StartClient()
	{
		Debug.Log("Client created.");
	}

	public string[] GetPlayerNames()
	{
		string[] names = new string[0];
		playerNames.CopyTo(names);
		return names;
	}

	[RPC]
	void AddPlayerNameToList(string newPlayerName)
	{
		playerNames.Add(newPlayerName);
	}
	[RPC]
	void RemovePlayerNameFromList(string disconnectedPlayerName)
	{
		playerNames.Remove(disconnectedPlayerName);
	}

	void OnConnectedToServer()
	{
		networkView.RPC ("AddPlayerNameToList", RPCMode.AllBuffered, PlayerPrefs.GetString("playerName"));
	}

	void OnDisconnectedToServer(NetworkDisconnection info)
	{
		networkView.RPC ("RemovePlayerNameFromList", RPCMode.AllBuffered, PlayerPrefs.GetString("playerName"));
	}

	void OnFailedToConnect(NetworkConnectionError err)
	{

	}


}
