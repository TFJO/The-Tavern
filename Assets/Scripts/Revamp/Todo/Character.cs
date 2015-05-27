using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public bool IsPlayerControllable=false;
	private Arbitrator arbitrator;


	// Use this for initialization
	void Start () {
		Need needA = new Need("needA:200",200f);
		Need needB = new Need("needB:400",400f);
		NeedList needs = new NeedList(needA, needB);
		Debug.Log(needs.MostSevereBySeverityToMax.Name);
		Debug.Log(needs.MostSevereByDegree.Name);
		needA.Severity=100f;
		needB.Severity=250f;
//		Debug.Log(needs.MostSevereBySeverityToMax.Name);
//		Debug.Log(needs.MostSevereByDegree.Name);

		arbitrator = new Arbitrator ();
		DefaultBehaviour defaultbehaviour = new DefaultBehaviour (this, arbitrator);
		FollowOrderBehaviour followOrder = new FollowOrderBehaviour (this, arbitrator, 5);

		arbitrator.Arbitrate ();
		Debug.Log (arbitrator.CurrentBehaviour);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
