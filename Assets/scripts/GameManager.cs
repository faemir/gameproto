using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>  
{
	/*GUIManager changes:
	 * no longer a singleton because any instances created by 
	 * the singleton pattern do not preserve settings that 
	 * we need in the prefab (stuff like window sizes / alignments)
	 * Move all GUIManager.Instance functionality to GameManager.Instance 
	 */
	public Transform GUIManagerPrefab;

	public enum GameState
	{
		MainMenu,
		Play,
		Pause,
		GameOver
	}




	private GameState state = GameState.MainMenu;
	private GUIManager GUIMan;

	private string playerName = "Player 1";


	void Awake()
	{
		DontDestroyOnLoad (this);
		
		playerName = PlayerPrefs.GetString("playerName");
		if ( playerName == "" ) playerName = "Player 1";

		GUIManagerPrefab = Instantiate(GUIManagerPrefab, Vector3.zero, Quaternion.identity) as Transform;
		DontDestroyOnLoad (GUIManagerPrefab);
		GUIMan = GUIManagerPrefab.GetComponent<GUIManager> ();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	 * Public GameManager interfaces
	 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	 */
	public GameState State 
	{
		get 
		{ 
			return state; 
		} 
		set
		{
			switch (value)
			{
			case GameState.MainMenu:
				Application.LoadLevel ("mainmenu");
				state = GameState.MainMenu;
				GUIMan.ShowMainMenu();
				break;
			case GameState.Play:
				state = GameState.Play;
				GUIMan.ShowNoWindows();
				break;
			case GameState.Pause:
				state = GameState.Pause;
				GUIMan.ShowMainMenu();
				break;
			case GameState.GameOver:
				state = GameState.GameOver;
				GUIMan.ShowCredits();
				break;
			default:
				Debug.LogError("Tried to set GameManager.State to invalid value.");
				break;
			}
		}
	}

	public void NewGame(int level)
	{
		Application.LoadLevel (level);
		State = GameState.Play;
	}

	public void NewGame(string level)
	{
		Application.LoadLevel (level);
		State = GameState.Play;
	}

	public void StartDialogue(string character, string message, float showTime)
	{
		GUIMan.ShowDialogue (character, message, showTime);
	}

}
