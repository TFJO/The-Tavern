using UnityEngine;
using System.Collections.Generic;

public class Arbitrator{
	private List<AbstractBehaviour> _behaviours;
	private AbstractBehaviour _currentBehaviour;
	private BehaviourPriorityComparer _comparer;

	public List<AbstractBehaviour> Behaviours{
		get{
			return _behaviours;
		}
		set {
			_behaviours = value;
		}
	}

	public AbstractBehaviour CurrentBehaviour{
		get{
			return _currentBehaviour;
		}
	}

	public bool Arbitrate(){
		if (_currentBehaviour!=null && _currentBehaviour.IsActive() && !_currentBehaviour.CanRelease())
			return true;
		foreach (AbstractBehaviour behaviour in _behaviours) {
			if(behaviour.IsRequested()){
				_currentBehaviour=behaviour;
				return true;
			}
		}
		return false;
	}

	public bool Execute(){
		if (_currentBehaviour != null && _currentBehaviour.IsActive ()) {
			_currentBehaviour.Execute();
			return true;
		}
		return false;
	}

	public void AddBehaviour(AbstractBehaviour behaviour){
		if(!_behaviours.Contains(behaviour))
			_behaviours.Add (behaviour);
		_behaviours.Sort (_comparer);
		behaviour.CurrentArbitrator = this;
	}
	public void RemoveBehaviour(AbstractBehaviour behaviour){
		_behaviours.Remove (behaviour);
	}

	public void SortBehaviours(){
		_behaviours.Sort (_comparer);
	}
	public void SetCharacterToAllBehaviours(Character character){
		foreach (AbstractBehaviour behaviour in _behaviours) {
			behaviour.CurrentCharacter = character;
		}
	}

	public Arbitrator(){
		_behaviours = new List<AbstractBehaviour> ();
		_comparer = new BehaviourPriorityComparer ();
	}

	public Arbitrator(List<AbstractBehaviour> behaviours):this(){
		_behaviours = behaviours;
		_behaviours.Sort (_comparer);
		foreach (AbstractBehaviour behaviour in _behaviours)
			behaviour.CurrentArbitrator = this;
	}

	public Arbitrator(params AbstractBehaviour[] behaviours):this(){
		_behaviours = new List<AbstractBehaviour> (behaviours);
		_behaviours.Sort (_comparer);
		foreach (AbstractBehaviour behaviour in _behaviours)
			behaviour.CurrentArbitrator = this;
	}

	public class BehaviourPriorityComparer : IComparer<AbstractBehaviour>{
		public int Compare(AbstractBehaviour x, AbstractBehaviour y){
			return x.Priority - y.Priority;
		}
	}
}
