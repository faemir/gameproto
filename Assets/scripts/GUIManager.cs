using UnityEngine;
using System.Collections;

[System.Serializable]
public class GUIWindow
{
	/// <summary>
	/// Overrides the left indent.
	/// </summary>
	public bool alignVertical = false;
	/// <summary>
	/// Overrides the top indent.
	/// </summary>
	public bool alignHorizontal = false;
	public int top = 10;
	public int left = 10;
	public int height = 100;
	public int width = 100;
}


/// <summary>
/// All the GUI code exists in this class.
/// </summary>
public class GUIManager : Singleton<GUIManager> 
{
	public int buttonHeight = 25;
	public GUIWindow mainMenu = new GUIWindow();
	public GUIWindow startServer = new GUIWindow();
	public GUIWindow serverList = new GUIWindow();
	public GUIWindow playerList = new GUIWindow();
	public GUIWindow options = new GUIWindow();
	public GUIWindow chatLog = new GUIWindow();
	public GUIWindow eventLog = new GUIWindow();

	
	private enum GUIState
	{
		MainMenu,
		StartServer,
		ServerList,
		PlayerList,
		Options,
		ChatLog,
		EventLog,
		NoWindows
	}

	private GUIState state = GUIState.MainMenu;

	private string serverName = "Default Server Name";
	private string playerName = "Player 1";

	/// <summary>
	/// Initialisation.
	/// Load player pref values.
	/// </summary>
	void Awake()
	{
		serverName = PlayerPrefs.GetString("serverName");
		if ( serverName == "" ) serverName = "Default Server Name";

		playerName = PlayerPrefs.GetString("playerName");
		if ( playerName == "" ) playerName = "Player 1";
	}


	/// <summary>
	/// Main Menu GUI function.
	/// </summary>
	void wMainMenu(int windowID)
	{
		/* Main Menu
		 * Resume Game (if Client)
		 * Start Server
		 * Browse Servers
		 * Options
		 * Quit
		 */

		// Gap for the header
		GUILayout.Space(15);


		// Resume game
		if (Network.isClient || Network.isServer)
		{
			if ( GUILayout.Button ("Resume Game", GUILayout.Height (buttonHeight)) )
				state = GUIState.NoWindows;
		}

		// Start server
		GUILayout.Space(5);
		if ( GUILayout.Button ("Host Game", GUILayout.Height(buttonHeight)) )
			state = GUIState.StartServer;
		

		// Browse Servers
		GUILayout.Space(5);
		if ( GUILayout.Button ("Browse Servers", GUILayout.Height (buttonHeight)) )
		{
			state = GUIState.ServerList;
			if (!refreshingHostData)
				StartCoroutine("RefreshHostData");
		}

		// Options
		GUILayout.Space(5);
		if ( GUILayout.Button ("Options", GUILayout.Height (buttonHeight)) )
			state = GUIState.Options;

		// Quit
		GUILayout.Space(5);
		if ( GUILayout.Button ("Quit", GUILayout.Height (buttonHeight)) )
			Application.Quit();
	}

	/// <summary>
	/// StartServer GUI Function.
	/// </summary>
	void wStartServer(int windowID)
	{
		// Gap for header
		GUILayout.Space(15);

		GUILayout.Label ("Server Name:");
		serverName = GUILayout.TextField(serverName);

		GUILayout.Space(5);

		if ( GUILayout.Button("Start Server", GUILayout.Height(buttonHeight)) )
		{
			PlayerPrefs.SetString("serverName", serverName);
			Server.Instance.StartServer();
			Client.Instance.StartClient();
			state = GUIState.NoWindows;
		}

		// Back to main menu button
		GUILayout.Space(5);
		if ( GUILayout.Button ("Main Menu", GUILayout.Height(buttonHeight)) )
			state = GUIState.MainMenu;
	}

	void wOptions(int windowID)
	{
		// Gap for header
		GUILayout.Space(15);

		GUILayout.Label("Player Name:");
		playerName = GUILayout.TextField(playerName);

		// Back to main menu button
		GUILayout.Space(5);
		if ( GUILayout.Button ("Main Menu", GUILayout.Height(buttonHeight)) )
		{
			PlayerPrefs.SetString("playerName", playerName);
			state = GUIState.MainMenu;
		}
	}

	private HostData[] hostData;
	private bool refreshingHostData = false;
	/// <summary>
	/// ServerList GUI Function.
	/// </summary>
	void wServerList(int windowID)
	{
		// Gap for header
		GUILayout.Space(15);

		// Refresh list button
		if ( GUILayout.Button("Refresh List", GUILayout.Height (buttonHeight)) )
		{
			if (!refreshingHostData)
				StartCoroutine("RefreshHostData");
		}

		// List of active servers
		// Shown as buttons, connect to server on button click
		if ( hostData != null )
		{
			for ( int i = 0; i < hostData.Length; i++ )
			{
				GUILayout.Space(5);
				if ( GUILayout.Button(hostData[i].gameName, GUILayout.Height (buttonHeight)) )
				{
					Client.Instance.StartClient();
					Debug.Log ("Connecting to..." + hostData[i].gameName);	
					Network.Connect(hostData[i]);
				}
			}

			if ( hostData.Length == 0 )
			{
				GUILayout.Space(5);
				if (refreshingHostData)
					GUILayout.Label("Refreshing...");
				else
					GUILayout.Label("No servers found.");
			}
		}

		// Back to main menu button
		GUILayout.Space(5);
		if ( GUILayout.Button ("Main Menu", GUILayout.Height(buttonHeight)) )
			state = GUIState.MainMenu;
	}

	/// <summary>
	/// Refreshes the host data.
	/// </summary>
	IEnumerator RefreshHostData()
	{
		refreshingHostData = true;
		Debug.Log("Refreshing host list...");
		MasterServer.RequestHostList(Server.Instance.registeredServerName);

		float timeToWaitForServers = 2.5f;
		float timeToEnd = Time.time + timeToWaitForServers;
		while (Time.time < timeToEnd)
		{
			hostData = MasterServer.PollHostList();
			yield return new WaitForEndOfFrame();
		}

		if ( hostData == null || hostData.Length == 0 )
			Debug.Log("No active servers have been found.");
		else
			Debug.Log(hostData.Length + " server(s) have been found.");
		refreshingHostData = false;
	}

	string[] playerNames;
	void wPlayerList(int windowID)
	{
		// Gap for header
		GUILayout.Space(15);

		playerNames = Client.Instance.GetPlayerNames();
		if (playerNames != null)
		{
			for (int i = 0; i < playerNames.Length; i++)
			{
				GUILayout.Space (5);
				GUILayout.Label (playerNames[i]);
			}
		}
	}

	/// <summary>
	/// Called every fixed update.
	/// Decide which window to draw.
	/// </summary>
	void FixedUpdate()
	{

		// If there's no GUI being displayed, then check for input.
		if (state == GUIState.NoWindows)
		{
			Screen.lockCursor = true;	// hide cursor whilst no GUI is shown

			if ( Input.GetKeyDown(KeyCode.Escape) )
			{
				state = GUIState.MainMenu;
			}
		}
		else
		{
			Screen.lockCursor = false;
		}

		if ( Network.peerType != NetworkPeerType.Disconnected )
		{
			if ( Input.GetKeyDown(KeyCode.Tab) && state == GUIState.NoWindows)
			{
				state = GUIState.PlayerList;
			}

			if (Input.GetKeyUp(KeyCode.Tab))
				state = GUIState.NoWindows;
		}



	}

	/// <summary>
	/// Draws the window.
	/// </summary>
	void OnGUI()
	{
		int topIndent = 5;
		int leftIndent = 5;
		GUIWindow thisWindow = new GUIWindow();

		// Copy GUIWindow settings to thisWindow
		switch ( state )
		{
		case GUIState.MainMenu:
			thisWindow = mainMenu;
			break;
		case GUIState.StartServer:
			thisWindow = startServer;
			break;
		case GUIState.ServerList:
			thisWindow = serverList;
			break;
		case GUIState.PlayerList:
			thisWindow = playerList;
			break;
		case GUIState.Options:
			thisWindow = options;
			break;
		case GUIState.ChatLog:
			thisWindow = chatLog;
			break;
		case GUIState.EventLog:
			thisWindow = eventLog;
			break;
		default:
			break;
		}

		// Calculate window size for thisWindow
		topIndent = thisWindow.top;
		if ( thisWindow.alignHorizontal )
			topIndent = Screen.height / 2 - thisWindow.height / 2;

		leftIndent = thisWindow.left;
		if ( thisWindow.alignVertical )
			leftIndent = Screen.width / 2 - thisWindow.width / 2;

		Rect windowSize = new Rect(leftIndent, topIndent, thisWindow.width, thisWindow.height);

		// Draw thisWindow (GUILayout.Window)
		switch ( state )
		{
		case GUIState.MainMenu:
			GUILayout.Window (1, windowSize, wMainMenu, "Main Menu");
			break;
		case GUIState.StartServer:
			GUILayout.Window(1, windowSize, wStartServer, "Host A Game");
			break;
		case GUIState.ServerList:
			GUILayout.Window (1, windowSize, wServerList, "Server List");
			break;
		case GUIState.Options:
			GUILayout.Window (1, windowSize, wOptions, "Options");
			break;
		case GUIState.PlayerList:
			GUILayout.Window (1, windowSize, wPlayerList, "Player List");
			break;
		case GUIState.ChatLog:
			break;
		case GUIState.EventLog:
			break;
		case GUIState.NoWindows:
			break;
		default:
			break;
		}
	}
	
}
