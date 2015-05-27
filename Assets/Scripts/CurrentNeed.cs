using UnityEngine;
using System.Collections.Generic;

public class CurrentNeed: System.IComparable {

	public AIController.Need need;

	public float timeStamp=0;


	private CurrentNeed(){}

	public CurrentNeed(float timeStamp, AIController.Need need){
		this.timeStamp = timeStamp;
		this.need = need;
	}

	public float GetPriority(){
		return (Time.time - timeStamp);// * priorityMultiplier;
	}

	public int CompareTo (object obj)
	{
		if(obj is CurrentNeed){
			return (int)((CurrentNeed)obj).GetPriority()-(int)GetPriority();
		}
		throw new UnityException("This list should only contain CurrentNeed Objects");
	}

/*	public override bool Equals (object obj)
	{
		if(obj is CurrentNeed && ((CurrentNeed)obj).need == this.need)
			return true;
		return false;
	}*/
}
