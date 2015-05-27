using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractBehaviour {

	private Arbitrator _arbitrator;
	private Character _character;
	private int _priority;

	abstract public bool IsRequested();
	abstract public bool IsActive();
	abstract public bool CanRelease();
	abstract public void Execute ();

	public AbstractBehaviour(Arbitrator arbitrator, Character character, int priority){
		_arbitrator = arbitrator;
		_character = character;
		_priority = priority;
		if(arbitrator!=null)
			arbitrator.AddBehaviour (this);
	}

	public Arbitrator CurrentArbitrator{
		set{
			if (value.Equals(_arbitrator))
				return;
			if (_arbitrator != null)
				_arbitrator.RemoveBehaviour (this);
			_arbitrator = value;
			_arbitrator.AddBehaviour (this);
		}
		get{
			return _arbitrator;
		}
	}
	public Character CurrentCharacter{
		set{
			_character = value;
		}
		get{
			return _character;
		}
	}
	public int Priority{
		get{
			return _priority;
		}
		set{
			_priority = value;
			if(_arbitrator!= null)
				_arbitrator.SortBehaviours();
		}
	}
}
