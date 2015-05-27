using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public bool NeedToSleep=false;
	public bool NeedToEat=false;
	public bool NeedToDrink=false;

	public Transform DeathAnim;

	public float TimeToGenerateSleepNeed;
	public float TimeToGenerateEatNeed;
	public float TimeToGenerateDrinkNeed;

	private Coroutine EatCoroutine;
	private Coroutine DrinkCoroutine;
	private Coroutine SleepCoroutine;

	private NavMeshAgent agent;

	public int numberOfNeeds=0;
	private bool claimedAService=false;
	private bool beingServiced=false;

	private Need needBeingFulfilled = Need.NoNeed;
	private ArrayList needs;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		SleepCoroutine= StartCoroutine(GenerateNeedToSleep());
		DrinkCoroutine= StartCoroutine(GenerateNeedToDrink());
		EatCoroutine = StartCoroutine(GenerateNeedToEat());
		needs = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		numberOfNeeds = needs.Count;
		if(needs.Count<1 || claimedAService || beingServiced)
			return;
		needs.Sort();
//		Debug.Log (needs.Count);
		IService service;
		foreach (object obj in needs){
			if(ServiceManager.getInstance().FindService(((CurrentNeed)obj).need, out service)){
				agent.destination = service.GetTransform().position;
				service.Claim();
				needBeingFulfilled = ((CurrentNeed)obj).need;
				needs.Remove(obj);
				claimedAService=true;
				return;
			}
		}

/*		if(!NeedToDrink&&!NeedToEat&&!NeedToSleep)
			return;
		IService service;
		if(needBeingFulfilled!=Need.NoNeed)
			return;
		if(NeedToSleep && ServiceManager.getInstance().FindService(Need.Sleep, out service)){
			agent.destination = service.GetTransform().position;
			service.Claim();
			needBeingFulfilled = Need.Sleep;
		}
		else if(NeedToDrink && ServiceManager.getInstance().FindService(Need.Drink, out service)){
			agent.destination = service.GetTransform().position;
			service.Claim();
			needBeingFulfilled = Need.Drink;
		}
		else if(NeedToEat && ServiceManager.getInstance().FindService(Need.Food, out service)){
			agent.destination = service.GetTransform().position;
			service.Claim();
			needBeingFulfilled = Need.Food;
		}
*/		//Write a ServiceManager to query for solutions
	}

	void OnTriggerEnter(Collider other){
		IService service;
		if(ServiceManager.getInstance().MatchColliderToService(other, out service)){
			if(service.GetProvidedNeed().Equals(needBeingFulfilled) && !service.IsBlocked()){
				beingServiced=true;
				service.ProvideService(this);
				switch(service.GetProvidedNeed()){
				case Need.Drink:
					if(DrinkCoroutine!=null)
						StopCoroutine(DrinkCoroutine);
					DrinkCoroutine=null;
					break;
				case Need.Food:
					if(EatCoroutine!=null)
						StopCoroutine(EatCoroutine);
					EatCoroutine = null;
					break;
				case Need.Sleep:
					if(SleepCoroutine!=null)
						StopCoroutine(SleepCoroutine);
					SleepCoroutine = null;
					break;
				}
			}
//			else if(service.GetProvidedNeed().Equals(needBeingFulfilled) && service.IsBlocked()){
//				needBeingFulfilled= Need.NoNeed;
//			}
		}
	}

	void OnTriggerStay(Collider other){
		if(DrinkCoroutine!= null && EatCoroutine != null && SleepCoroutine != null){
			OnTriggerEnter(other);
		}
	}


	public void ProvideNeed(Need need){
		switch(need){
		case Need.Drink:
			NeedToDrink = false;
			DrinkCoroutine = StartCoroutine(GenerateNeedToDrink());
			break;
		case Need.Food:
			NeedToEat=false;
			EatCoroutine = StartCoroutine(GenerateNeedToEat());
			break;
		case Need.Sleep:
			NeedToSleep=false;
			SleepCoroutine = StartCoroutine(GenerateNeedToSleep());
			break;
		}
		claimedAService=false;
		beingServiced=false;
		needBeingFulfilled=Need.NoNeed;
	}

	private IEnumerator GenerateNeedToSleep(){
		while(true){
			yield return new WaitForSeconds(TimeToGenerateSleepNeed);
			if(NeedToSleep){
				DeathAnim.parent=null;
				DeathAnim.gameObject.SetActive(true);
				Destroy(gameObject);
			}
			NeedToSleep = true;
			CurrentNeed newNeed =new CurrentNeed(Time.time, Need.Sleep);
			if(!needs.Contains(newNeed))
				needs.Add(newNeed);
		}
	}
	private IEnumerator GenerateNeedToDrink(){
		while(true){
			yield return new WaitForSeconds(TimeToGenerateDrinkNeed);
			if(NeedToDrink){
				DeathAnim.parent=null;
				DeathAnim.gameObject.SetActive(true);
				Destroy(gameObject);
			}
			NeedToDrink=true;
			CurrentNeed newNeed =new CurrentNeed(Time.time, Need.Drink);
			if(!needs.Contains(newNeed))
				needs.Add(newNeed);
		}
	}
	private IEnumerator GenerateNeedToEat(){
		while(true){
			yield return new WaitForSeconds(TimeToGenerateEatNeed);
			if(NeedToEat){
				DeathAnim.parent=null;
				DeathAnim.gameObject.SetActive(true);
				Destroy(gameObject);
			}
			NeedToEat=true;
			CurrentNeed newNeed =new CurrentNeed(Time.time, Need.Food);
			if(!needs.Contains(newNeed))
				needs.Add(newNeed);
		}
	}
	public enum Need {Sleep, Food, Drink, NoNeed}
}
