using UnityEngine;
using System.Collections;

public class Service_Table : MonoBehaviour, IService {
	
	public static float useTime=5f;
	private AIController controller;
	public AIController.Need providedNeed = AIController.Need.Food;
	[SerializeField]
	private bool claimed=false;
	public int maxUses=10;
	[SerializeField]
	private int usesLeft=10;
	private Material mat;

	public Color Standard;
	public Color Depleted;

	void Start(){
		Collider tmp=null;
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
		return controller!=null || usesLeft <1;
	}
	
	public bool ProvideService (AIController controller)
	{
		if(IsBlocked())
			return false;
		this.controller = controller;
		StartCoroutine(Eat ());
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

	private IEnumerator Eat(){
		yield return new WaitForSeconds(useTime);
		
		//		controller bedienen
		controller.ProvideNeed(providedNeed);
		controller = null;
		usesLeft--;
		mat.color = Color.Lerp(Standard, Depleted, 1f-(float)usesLeft/(float)maxUses);
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
