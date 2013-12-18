﻿using UnityEngine;
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
	public GUIWindow options = new GUIWindow();
	public GUIWindow credits = new GUIWindow();
	public GUIWindow dialogue = new GUIWindow();


	public bool gameOver = true;
	public bool finaleMode = false;
	public float finaleTime = 0f;
	public float playerSpeed;

	public void ToggleFinaleMode()
	{
		finaleMode = !finaleMode;
		finaleTime = Time.time;
	}

	public void SetGameOver()
	{
		gameOver = true;
		state = GUIState.Credits;
	}

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
		state = GUIState.NoWindows;
	}


	private enum GUIState
	{
		MainMenu,
		Options,
		Credits,
		ShowDialogue,
		NoWindows
	}

	private GUIState state = GUIState.MainMenu;

	private string playerName = "Player 1";


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

		// Credits
		GUILayout.Space(5);
		if (GUILayout.Button ("Credits", GUILayout.Height (buttonHeight)))
			state = GUIState.Credits;

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
		if ( GUILayout.Button ("Main Menu", GUILayout.Height(buttonHeight)) )
			state = GUIState.MainMenu;

	}

	public bool showDialogue = false;
	string dialogueCharacter = "";
	string dialogueMessage = "";
	float dialogueTime = 0f;
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
		GUILayout.Space (15);
		GUILayout.Label (dialogueMessage);
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

		Rect windowSize = new Rect(leftIndent, topIndent, thisWindow.width, thisWindow.height);

		// Draw thisWindow (GUILayout.Window)
		switch ( state )
		{
		case GUIState.MainMenu:
			GUILayout.Window (1, windowSize, wMainMenu, "RedLines");
			break;
		case GUIState.Options:
			GUILayout.Window (1, windowSize, wOptions, "Options");
			break;
		case GUIState.Credits:
			GUILayout.Window (1, windowSize, wCredits, "Credits");
			break;
		case GUIState.ShowDialogue:
			GUILayout.Window (1, windowSize, wDialogue, dialogueCharacter);
			break;
		case GUIState.NoWindows:
			break;
		default:
			break;
		}
	}
	
}
