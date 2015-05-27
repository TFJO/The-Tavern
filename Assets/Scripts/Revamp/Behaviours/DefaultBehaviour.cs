using UnityEngine;
using System.Collections;

public class DefaultBehaviour : AbstractBehaviour {

	public DefaultBehaviour(Character character, Arbitrator arbitrator) : base (arbitrator,character, int.MaxValue){
		
	}
	public override bool IsRequested ()
	{
		return true;
	}

	public override bool IsActive ()
	{
		return true;
	}

	public override void Execute ()
	{
		return;
	}
	public override bool CanRelease()
	{
		return true;
	}
}
