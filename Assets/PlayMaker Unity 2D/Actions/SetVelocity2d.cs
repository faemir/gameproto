// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2D")]
	[Tooltip("Sets the Velocity of a 2D Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody 2D.")]
	public class SetVelocity2D : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmVector2 vector;
		
		public FsmFloat x;
		public FsmFloat y;
		//public FsmFloat z;
		
		public Space space;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			//z = new FsmFloat { UseVariable = true };
			space = Space.Self;
			everyFrame = false;
		}

        public override void Awake()
        {
            Fsm.HandleFixedUpdate = true;
        }		

		// TODO: test this works in OnEnter!
		public override void OnEnter()
		{
			DoSetVelocity();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnFixedUpdate()
		{
			DoSetVelocity();
			
			if (!everyFrame)
				Finish();
		}

		void DoSetVelocity()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null || go.rigidbody2D == null)
			{
				return;
			}
			
			// init position
			
			Vector2 velocity;

			if (vector.IsNone)
			{
				velocity = go.transform.InverseTransformDirection(go.rigidbody2D.velocity);
			}
			else
			{
				velocity = vector.Value;
			}
			
			// override any axis

			if (!x.IsNone) velocity.x = x.Value;
			if (!y.IsNone) velocity.y = y.Value;
			//if (!z.IsNone) velocity.z = z.Value;

			// apply
			
			go.rigidbody2D.velocity = go.transform.TransformDirection(velocity);
		}
	}
}