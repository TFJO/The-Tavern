using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Collider LagerRaum;
	public Camera mainCam;
	private NavMeshAgent agent;
//	public Transform target;
	[SerializeField]
	private bool storageAccess=false;
//	public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl controller;
	public AIController.Need inventory = AIController.Need.NoNeed;

	IService service;
	// Use this for initialization
	void Start () {
//		target.position =transform.position;
		agent = GetComponent<NavMeshAgent>();
//		controller.target= target;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			if(Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit)){
				agent.destination=hit.point;
	//			target.position = hit.point;
			}
		}
		if(storageAccess){
			if(Input.GetKeyDown(KeyCode.Q))
				inventory = AIController.Need.Food;
			if(Input.GetKeyDown(KeyCode.W))
				inventory = AIController.Need.Drink;
		}
		if(Input.GetKeyDown(KeyCode.Space) && service!=null){
			if(inventory.Equals(service.GetProvidedNeed())){
				inventory = AIController.Need.NoNeed;
				service.Recharge();
			}
		}
	}

	void OnGUI(){
		if(storageAccess)
			GUI.Label(new Rect(0,0,200,50),"Q to take Food\nW to take Drinks");

	}
	
	void OnTriggerEnter(Collider other){
		IService tmp;
		if(other.Equals(LagerRaum))
			storageAccess=true;
		else if(ServiceManager.getInstance().MatchColliderToService(other,out tmp))
			service = tmp;
	}

	void OnTriggerExit(Collider other){
		if(other.Equals(LagerRaum))
			storageAccess=false;
		service = null;
	}

}
