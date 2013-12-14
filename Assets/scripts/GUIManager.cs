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
	public GUIWindow options = new GUIWindow();

	public bool gameOver = true;
	
	private enum GUIState
	{
		MainMenu,
		Options,
		NoWindows
	}

	private GUIState state = GUIState.MainMenu;

	private string playerName = "Player 1";

	/// <summary>
	/// Initialisation.
	/// Load player pref values.
	/// </summary>
	void Awake()
	{
		DontDestroyOnLoad (this);

		playerName = PlayerPrefs.GetString("playerName");
		if ( playerName == "" ) playerName = "Player 1";
	}


	/// <summary>
	/// Main Menu GUI function.
	/// </summary>
	void wMainMenu(int windowID)
	{
		/* Main Menu
		 * Resume Game 
		 * Options
		 * Quit
		 */

		// Gap for the header
		GUILayout.Space(15);


		// Resume game
		if (gameOver) 
		{
			if (GUILayout.Button ("New Game", GUILayout.Height (buttonHeight))) 
			{

				Application.LoadLevel ("scene1");
				gameOver = false;
				state = GUIState.NoWindows;
			}
		}
		else
		{
			if (GUILayout.Button ("Resume Game", GUILayout.Height (buttonHeight)))
			{
				state = GUIState.NoWindows;
			}
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
		case GUIState.Options:
			thisWindow = options;
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
		case GUIState.Options:
			GUILayout.Window (1, windowSize, wOptions, "Options");
			break;
		case GUIState.NoWindows:
			break;
		default:
			break;
		}
	}
	
}
