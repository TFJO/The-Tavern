using UnityEngine;
using System.Collections;

public class Service_Bett : MonoBehaviour, IService {

	public static float useTime=2f;
	private AIController controller;
	public AIController.Need providedNeed = AIController.Need.Sleep;
	[SerializeField]
	private bool claimed=false;

	void Start(){
		Collider tmp=null;
		Collider[] colliders = GetComponentsInChildren<Collider>();
		foreach(Collider col in colliders)
			if(col.isTrigger)
				tmp=col;
		ServiceManager.getInstance().RegisterService(providedNeed, this, tmp);
	}


	public bool IsBlocked ()
	{
		return controller!=null;
	}

	public bool ProvideService (AIController controller)
	{
		if(IsBlocked())
			return false;
		this.controller = controller;
		StartCoroutine(Sleep ());
		return true;
	}

	public AIController GetCurrentServiceUser ()
	{
		return controller;
	}

	public Transform GetTransform(){
		return transform;
	}

	public AIController.Need GetProvidedNeed(){
		return providedNeed;
	}

	private IEnumerator Sleep(){
		yield return new WaitForSeconds(useTime);

//		controller bedienen
		controller.ProvideNeed(providedNeed);
		controller = null;
		claimed=false;
	}
	public bool Claim(){
		if(claimed)
			return false;
		claimed = true;
		return true;
	}
	public bool IsClaimed(){
		return claimed;
	}
	public void Recharge(){
		Debug.Log("The fuck are you doing, mate?");
	}
}
