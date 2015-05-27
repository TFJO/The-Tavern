using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public Transform targetPosition;

	void OnTriggerEnter(Collider col){
//		col.gameObject.transform.position = ;
		NavMeshAgent agent=col.gameObject.GetComponent<NavMeshAgent>();
		agent.Warp(targetPosition.position);
//		agent.Resume();
	}
}
