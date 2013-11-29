// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

/// <summary>
/// This component is needed on scenes featuring GameObjects Physics 2D Colliders with Fsms listening to "TRIGGER XXX 2D" and "COLLISION XXX 2D" global events.
/// </summary>
public class PlayMakerUnity2d : MonoBehaviour {

	static PlayMakerFSM fsmProxy;
	public static string PlayMakerUnity2dProxyName = "PlayMaker Unity 2D";

	static FsmOwnerDefault goTarget; // simple cache.

	public static string OnCollisionEnter2DEvent	= "COLLISION ENTER 2D";
	public static string OnCollisionExit2DEvent		= "COLLISION EXIT 2D";
	public static string OnCollisionStay2DEvent		= "COLLISION STAY 2D";
	public static string OnTriggerEnter2DEvent		= "TRIGGER ENTER 2D";
	public static string OnTriggerExit2DEvent		= "TRIGGER EXIT 2D";
	public static string OnTriggerStay2DEvent		= "TRIGGER STAY 2D";


	void Awake () {
		fsmProxy = this.GetComponent<PlayMakerFSM>();

		if (fsmProxy==null)
		{
			Debug.LogError("'PlayMaker Unity 2D' is missing." ,this);
		}

		// set the target to be this gameObject.
		goTarget = new FsmOwnerDefault();
		goTarget.GameObject = new FsmGameObject();
		goTarget.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
		
		// send the event to this gameObject
		FsmEventTarget eventTarget = new FsmEventTarget();
		eventTarget.excludeSelf = false;
		eventTarget.target = FsmEventTarget.EventTarget.GameObject;
		eventTarget.gameObject = goTarget;
		eventTarget.sendToChildren = false;
	}

	static public bool isAvailable()
	{
		return fsmProxy!=null;
	}
	

	static public void ForwardEventToGameObject(GameObject target,string eventName)
	{

		// set the target to be this gameObject.
		goTarget.GameObject.Value = target;

		// send the event to this gameObject
		FsmEventTarget eventTarget = new FsmEventTarget();
		eventTarget.target = FsmEventTarget.EventTarget.GameObject;
		eventTarget.gameObject = goTarget;

		// create the event
		FsmEvent fsmEvent = new FsmEvent(eventName);
		
		// send the event
		fsmProxy.Fsm.Event(eventTarget,fsmEvent.Name); // Doesn't work if we pass just the fsmEvent itself.
	}


}
