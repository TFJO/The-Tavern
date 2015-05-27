using UnityEngine;
using System.Collections;

public class Service_Bar : MonoBehaviour, IService {
	
	public static float useTime=2f;
	private AIController controller;
	public AIController.Need providedNeed = AIController.Need.Drink;

	public Color Standard;
	public Color Depleted;

	public int maxUses=10;
	[SerializeField]
	private int usesLeft=10;
	[SerializeField]
	private bool claimed=false;
	private Material mat;
//	private bool currentlyInUse=false;

	void Start(){
		Collider tmp = null;
		Collider[] colliders = GetComponentsInChildren<Collider>();
		foreach(Collider col in colliders)
			if(col.isTrigger)
				tmp=col;
		ServiceManager.getInstance().RegisterService(providedNeed, this, tmp);
		usesLeft = maxUses;
		mat = GetComponent<Renderer>().materials[0];
		mat.color = Standard;
	}

	public bool IsBlocked ()
	{
		return controller!=null || usesLeft < 1;
	}
	
	public bool ProvideService (AIController controller)
	{
		if(IsBlocked())//||currentlyInUse)
			return false;
//		currentlyInUse=true;
		this.controller = controller;
		StartCoroutine(Drink ());
		return true;
	}
	public Transform GetTransform(){
		return transform;
	}
	
	public AIController GetCurrentServiceUser ()
	{
		return controller;
	}

	public AIController.Need GetProvidedNeed(){
		return providedNeed;
	}

	
	private IEnumerator Drink(){
		yield return new WaitForSeconds(useTime);
		
		//		controller bedienen
		usesLeft--;
		mat.color = Color.Lerp(Standard, Depleted, 1f-(float)usesLeft/(float)maxUses);
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
		usesLeft = maxUses;
		mat.color = Color.Lerp(Standard, Depleted, 1f-(float)usesLeft/(float)maxUses);
	}
}
