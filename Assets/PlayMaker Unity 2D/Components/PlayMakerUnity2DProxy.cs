// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

/// <summary>
/// This component is needed on gameObjects that have Physics 2D Colliders with Fsms listening to "TRIGGER 2D XXX" and "COLLISION 2D XXX" global events.
/// </summary>
public class PlayMakerUnity2DProxy : MonoBehaviour {

	private bool debug = false;

	// Flags to avoid unnecessary processing, if no fsm implements a particular Collider callback, nothing will be processed.
	[HideInInspector]
	public bool enableCollisionEnterCallBacks = false;

	[HideInInspector]
	public bool enableCollisionExitCallBacks = false;

	[HideInInspector]
	public bool enableCollisionStayCallBacks = false;

	[HideInInspector]
	public bool enableTriggerEnterCallBacks = false;

	[HideInInspector]
	public bool enableTriggerExitCallBacks = false;

	[HideInInspector]
	public bool enableTriggerStayCallBacks = false;

	[HideInInspector]
	public Collision2D lastCollision2DInfo;
	[HideInInspector]
	public Collider2D lastTrigger2DInfo;
	


	[ContextMenu("Help")]
	public void help ()
	{
		Application.OpenURL ("https://hutonggames.fogbugz.com/default.asp?W1150");
	}

	
	public void Start()
	{
		if ( ! PlayMakerUnity2d.isAvailable() )
		{
			Debug.LogError("PlayMakerUnity2DProxy requires the 'PlayMaker Unity 2D' Prefab in the Scene.\n" +
				"Use the menu 'PlayMaker/Addons/Unity 2D/Components/Add PlayMakerUnity2D to Scene' to correct the situation",this);
			this.enabled = false;
			return;
		}
		RefreshImplementation();
	}

	public void RefreshImplementation()
	{
		CheckGameObjectEventsImplementation();
	}

	#region Physics 2D Messages

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (debug) Debug.Log("OnCollisionEnter2D "+enableCollisionEnterCallBacks);

		if (enableCollisionEnterCallBacks)
		{
			lastCollision2DInfo = coll;

			PlayMakerUnity2d.ForwardEventToGameObject(this.gameObject,PlayMakerUnity2d.OnCollisionEnter2DEvent);
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (debug) Debug.Log("OnCollisionExit2D "+enableCollisionExitCallBacks);

		if (enableCollisionExitCallBacks)
		{
			lastCollision2DInfo = coll;

			PlayMakerUnity2d.ForwardEventToGameObject(this.gameObject,PlayMakerUnity2d.OnCollisionExit2DEvent);
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (debug) Debug.Log("OnCollisionStay2D "+enableCollisionStayCallBacks);

		if (enableCollisionStayCallBacks)
		{
			lastCollision2DInfo = coll;

			PlayMakerUnity2d.ForwardEventToGameObject(this.gameObject,PlayMakerUnity2d.OnCollisionStay2DEvent);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (debug) Debug.Log(this.gameObject.name+" OnTriggerEnter2D "+coll.gameObject.name);

		if (enableTriggerEnterCallBacks)
		{
			lastTrigger2DInfo = coll;

			PlayMakerUnity2d.ForwardEventToGameObject(this.gameObject,PlayMakerUnity2d.OnTriggerEnter2DEvent);
		}
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		if (debug) Debug.Log(this.gameObject.name+" OnTriggerExit2D "+coll.gameObject.name);

		if (enableTriggerExitCallBacks)
		{
			lastTrigger2DInfo = coll;

			PlayMakerUnity2d.ForwardEventToGameObject(this.gameObject,PlayMakerUnity2d.OnTriggerExit2DEvent);
		}
	}
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (debug) Debug.Log(this.gameObject.name+" OnTriggerStay2D "+coll.gameObject.name);

		if (enableTriggerStayCallBacks)
		{
			lastTrigger2DInfo = coll;

			PlayMakerUnity2d.ForwardEventToGameObject(this.gameObject,PlayMakerUnity2d.OnTriggerStay2DEvent);
		}
	}

	
	#endregion


	#region Internal

	void CheckGameObjectEventsImplementation()
	{
		PlayMakerFSM[] fsms = this.GetComponents<PlayMakerFSM>();
		foreach(PlayMakerFSM fsm in fsms)
		{
			CheckFsmEventsImplementation(fsm);
		}
	}

	void CheckFsmEventsImplementation(PlayMakerFSM fsm)
	{
		foreach(FsmTransition _transition in fsm.FsmGlobalTransitions)
		{
			CheckTransition(_transition.EventName);
		}
		
		foreach(FsmState _state in fsm.FsmStates)
		{
			
			foreach(FsmTransition _transition in _state.Transitions)
			{
				CheckTransition(_transition.EventName);
			}
		}
	}

	void CheckTransition(string transitionName)
	{
		if (transitionName.Equals(PlayMakerUnity2d.OnCollisionEnter2DEvent))
		{
			enableCollisionEnterCallBacks = true;
		}
		if (transitionName.Equals(PlayMakerUnity2d.OnCollisionExit2DEvent))
		{
			enableCollisionExitCallBacks = true;
		}
		if (transitionName.Equals(PlayMakerUnity2d.OnCollisionStay2DEvent))
		{
			enableCollisionStayCallBacks = true;
		}
		if (transitionName.Equals(PlayMakerUnity2d.OnTriggerEnter2DEvent))
		{
			enableTriggerEnterCallBacks = true;
		}
		if (transitionName.Equals(PlayMakerUnity2d.OnTriggerExit2DEvent))
		{
			enableTriggerExitCallBacks = true;
		}
		if (transitionName.Equals(PlayMakerUnity2d.OnTriggerStay2DEvent))
		{
			enableTriggerStayCallBacks = true;
		}
	}

	#endregion
	
}
