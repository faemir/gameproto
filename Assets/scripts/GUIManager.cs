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
public class GUIManager : MonoBehaviour
{
	public int buttonHeight = 25;
	public GUIWindow mainMenu = new GUIWindow();
	public GUIWindow levelSelect = new GUIWindow();
	public GUIWindow options = new GUIWindow();
	public GUIWindow credits = new GUIWindow();
	public GUIWindow dialogue = new GUIWindow();

	public float playerSpeed;


	public void ShowMainMenu()
	{
		state = GUIState.MainMenu;
	}

	public void ShowOptions()
	{
		state = GUIState.Options;
	}

	public void ShowCredits()
	{
		state = GUIState.Credits;
	}

	public void ShowNoWindows()
	{
		if (showDialogue)
						state = GUIState.ShowDialogue;
				else
						state = GUIState.NoWindows;
	}


	private enum GUIState
	{
		MainMenu,
		LevelSelect,
		Options,
		Credits,
		ShowDialogue,
		NoWindows
	}

	private GUIState state = GUIState.MainMenu;
	private string playerName = "Player 1";


	private bool showDialogue = false;
	private string dialogueCharacter = "";
	private string dialogueMessage = "";
	private float dialogueTime = 0f;

	private Rect windowSize = new Rect();
	private GUISkin menuSkin;
	private GUISkin transparentWindow;

	void Awake()
	{
		menuSkin = (GUISkin)Resources.Load ("Menus", typeof(GUISkin));
		transparentWindow = (GUISkin)Resources.Load ("TransparentWindow",typeof(GUISkin));
	}

	/// <summary>
	/// Main Menu GUI function.
	/// </summary>
	void wMainMenu(int windowID)
	{
		// Gap for the header
		GUILayout.Space(15);


		// Game first launched
		if (GameManager.Instance.State == GameManager.GameState.MainMenu) 
		{
			if (GUILayout.Button ("New Game", menuSkin.button, GUILayout.Height (buttonHeight))) 
			{
				state = GUIState.LevelSelect;
			}
		}


		// Player has died
		if (GameManager.Instance.State == GameManager.GameState.GameOver ) 
		{
			if (GUILayout.Button ("Retry", menuSkin.button, GUILayout.Height (buttonHeight))) 
			{
				GameManager.Instance.NewGame(Application.loadedLevel);
			}
			GUILayout.Space(20);
			if (GUILayout.Button ("New Game", menuSkin.button, GUILayout.Height (buttonHeight))) 
			{
				state = GUIState.LevelSelect;
			}
		}



		// Resume game
		if (GameManager.Instance.State == GameManager.GameState.Pause )
		{
			if (GUILayout.Button ("Resume Game", menuSkin.button, GUILayout.Height (buttonHeight)))
			{
				GameManager.Instance.State = GameManager.GameState.Play;
			}
			GUILayout.Space(20);
			if (GUILayout.Button ("Retry", menuSkin.button, GUILayout.Height (buttonHeight))) 
			{
				GameManager.Instance.NewGame(Application.loadedLevel);
			}
			GUILayout.Space(5);
			if (GUILayout.Button ("New Game", menuSkin.button, GUILayout.Height (buttonHeight))) 
			{
				state = GUIState.LevelSelect;
			}
		}

		// Options
		GUILayout.Space(5);
		if ( GUILayout.Button ("Options", menuSkin.button, GUILayout.Height (buttonHeight)) )
			state = GUIState.Options;

		// Credits
		GUILayout.Space(5);
		if (GUILayout.Button ("Credits", menuSkin.button, GUILayout.Height (buttonHeight)))
			state = GUIState.Credits;

		// Quit
		GUILayout.Space(5);
		if ( GUILayout.Button ("Quit", menuSkin.button, GUILayout.Height (buttonHeight)) )
			Application.Quit();
	}

	void wLevelSelect(int windowID)
	{
		GUILayout.Space (15);
		// level 1
		if (GUILayout.Button ("Stage 1", menuSkin.button, GUILayout.Height (buttonHeight))) 
		{
			GameManager.Instance.NewGame("scene1");
		}

		// level 2
		GUILayout.Space(5);
		if (GUILayout.Button ("Stage 2", menuSkin.button, GUILayout.Height (buttonHeight))) 
		{
			GameManager.Instance.NewGame("scene2");
		}

		// back to main menu
		GUILayout.Space(20);
		if ( GUILayout.Button ("Main Menu", menuSkin.button, GUILayout.Height(buttonHeight)) )
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
		if ( GUILayout.Button ("Main Menu", menuSkin.button, GUILayout.Height(buttonHeight)) )
		{
			PlayerPrefs.SetString("playerName", playerName);
			state = GUIState.MainMenu;
		}
	}

	void wCredits(int windowID)
	{
		GUILayout.Space (15);

		GUILayout.Label ("Primary author: Matt 'explosivose' Blickem");
		GUILayout.Space (5);
		GUILayout.Label ("Music: 'The Life and Death of a Certain K. Zabriskie, Patriarch'" +
						" by Chris Zabriskie");
		GUILayout.Space (5);
		GUILayout.Label ("SFX: Bfxr, explosivose");
		GUILayout.Space (5);
		GUILayout.Label ("This game was originally created for Ludum Dare 28 (December 2013). " + 
						"The author would like to thank the organisers of Ludum Dare, " + 
						"the developers of the Unity Engine, and all those helpful people " +
						"that answer questions / cries for help on the internet. Special " +
		                "thanks to friends that gave feedback and inspiration!"); 
		GUILayout.Space (5);
		GUILayout.Label ("No barrel rolls were done during the making of this game."); 
		// Back to main menu button
		GUILayout.Space(20);
		if ( GUILayout.Button ("Main Menu", menuSkin.button, GUILayout.Height(buttonHeight)) )
			state = GUIState.MainMenu;

	}


	public void ShowDialogue (string character, string message, float time)
	{
		dialogueCharacter = character;
		dialogueMessage = message;
		dialogueTime = time;
		StartCoroutine ("ShowDialogueMessage");
	}

	IEnumerator ShowDialogueMessage()
	{
		GUIState previousState = state;
		showDialogue = true;
		state = GUIState.ShowDialogue;
		yield return new WaitForSeconds (dialogueTime);
		state = previousState;
		showDialogue = false;
	}

	void wDialogue(int windowID)
	{
		//GUI.skin = transparentWindow;
		//Rect textRect = new Rect (0f, 0f, windowSize.width, windowSize.height);
		//GUI.TextArea (textRect, dialogueMessage);
		GUILayout.Label (dialogueMessage,transparentWindow.window);
	}




	/// <summary>
	/// Called every fixed update.
	/// Decide which window to draw.
	/// </summary>
	void FixedUpdate()
	{
		
		if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.State == GameManager.GameState.Pause) 
		{
			GameManager.Instance.State = GameManager.GameState.Play;
		}
		else if ( Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.State == GameManager.GameState.Play)
		{
			GameManager.Instance.State = GameManager.GameState.Pause;
		}

		// If there's no GUI being displayed, then check for input.
		if (state == GUIState.NoWindows || state == GUIState.ShowDialogue)
		{
			Screen.lockCursor = true;	// hide cursor whilst no GUI is shown
		}
		else
		{
			Screen.lockCursor = false;
		}
	}

	public Texture commander;

	/// <summary>
	/// Draws the window.
	/// </summary>
	void OnGUI()
	{
		GUI.Label (new Rect(5,5,200,20), "Current Speed: " + playerSpeed.ToString());

		int topIndent = 5;
		int leftIndent = 5;
		GUIWindow thisWindow = new GUIWindow();

		// Copy GUIWindow settings to thisWindow
		switch ( state )
		{
		case GUIState.MainMenu:
			thisWindow = mainMenu;
			break;
		case GUIState.LevelSelect:
			thisWindow = levelSelect;
			break;
		case GUIState.Options:
			thisWindow = options;
			break;
		case GUIState.Credits:
			thisWindow = credits;
			break;
		case GUIState.ShowDialogue:
			thisWindow = dialogue;
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

		windowSize = new Rect(leftIndent, topIndent, thisWindow.width, thisWindow.height);

		// Draw thisWindow (GUILayout.Window)
		switch ( state )
		{
		case GUIState.MainMenu:

			GUILayout.Window (1, windowSize, wMainMenu, "RedLines", menuSkin.window);
			break;
		case GUIState.LevelSelect:
			GUILayout.Window (1, windowSize, wLevelSelect, "Choose a level", menuSkin.window);
			break;
		case GUIState.Options:
			GUILayout.Window (1, windowSize, wOptions, "Options", menuSkin.window);
			break;
		case GUIState.Credits:
			GUILayout.Window (1, windowSize, wCredits, "Credits", menuSkin.window);
			break;
		case GUIState.ShowDialogue:
			GUILayout.Window (1, windowSize, wDialogue, commander, transparentWindow.window);
			break;
		case GUIState.NoWindows:
			break;
		default:
			break;
		}
	}
	
}
