using UnityEngine;
using System.Collections.Generic;

public class NeedList {

	private List<Need> _needs;
	private Need.NeedComparerBySeverityToMax sevToMaxComparer;
	private Need.NeedComparerByDegree degreeComparer;
	private Need.NeedComparerByPotentialDanger dangerComparer;

	public Need MostSevereBySeverityToMax{
		get{
			_needs.Sort(sevToMaxComparer);
			return _needs[0];
		}
	}
	public Need MostSevereByDegree{
		get{
			_needs.Sort(degreeComparer);
			return _needs[0];
		}
	}
	public Need MostPotentiallyDangerous{
		get{
			_needs.Sort(dangerComparer);
			return _needs[0];
		}
	}

	public bool HasDangerousNeed{
		get{
			foreach(Need need in _needs)
				if(need.isDangerous)
					return true;
			return false;
		}
	}

	public NeedList(){
		_needs = new List<Need>();
		sevToMaxComparer = new Need.NeedComparerBySeverityToMax();
		degreeComparer = new Need.NeedComparerByDegree();
		dangerComparer = new Need.NeedComparerByPotentialDanger ();
	}
	public NeedList(List<Need> needs):this(){
		_needs = needs;
	}
	public NeedList(params Need[] needs):this(){
		_needs.AddRange(needs);
//		Debug.Log(gottaTryThis);
	}


}
