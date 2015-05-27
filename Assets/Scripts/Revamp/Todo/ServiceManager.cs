using UnityEngine;
using System.Collections.Generic;

public class ServiceManager{

	private Dictionary<AIController.Need, List<IService>> serviceElements;
	private Dictionary<Collider, IService> colliderDictionary;

	private static ServiceManager self;

	private ServiceManager(){
		serviceElements = new Dictionary<AIController.Need, List<IService>>();
		colliderDictionary = new Dictionary<Collider, IService>();
	}

	public static ServiceManager getInstance(){
		if(self==null)
			self = new ServiceManager();
		return self;
	}

	public void RegisterService(AIController.Need need, IService service, Collider col){
		if(!serviceElements.ContainsKey(need)){
			serviceElements.Add(need, new List<IService>());
		}
		List<IService> list;
		serviceElements.TryGetValue(need, out list);
		list.Add(service);
		colliderDictionary.Add (col, service);
	}

	public bool FindService(AIController.Need need, out IService service){
		List<IService> list;
		if(!serviceElements.TryGetValue(need, out list)){
			service = null;
			return false;
		}
		foreach(IService provider in list){
			if(!provider.IsBlocked()&&!provider.IsClaimed()){
				service = provider;
				return true;
			}
		}
		service=null;
		return false;
	}

	public bool MatchColliderToService(Collider col, out IService service){
		if(colliderDictionary.TryGetValue(col, out service))
			return true;
		service = null;
		return false;
	}
}
