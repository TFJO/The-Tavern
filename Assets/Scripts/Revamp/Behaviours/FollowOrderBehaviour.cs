using UnityEngine;
using System.Collections;

public class FollowOrderBehaviour : AbstractBehaviour {


	public FollowOrderBehaviour(Character character, Arbitrator arbitrator, int priority) : base (arbitrator,character, priority){

	}
	public override bool IsRequested ()
	{
		return false;
	}

	public override bool IsActive ()
	{
		throw new System.NotImplementedException ();
	}

	public override bool CanRelease ()
	{
		throw new System.NotImplementedException ();
	}

	public override void Execute ()
	{
		throw new System.NotImplementedException ();
	}
}
