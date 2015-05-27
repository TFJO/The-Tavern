using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// A specific need of a specific <see cref="Character"/> .
/// </summary>
public class Need {



	private string _name;
	private int _potentialDanger;

	public int PotentialDanger{
		get{ return _potentialDanger;}
	}
	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <value>The name.</value>
	public string Name {
		get{return _name;}
	}
	private float _severity;
	/// <summary>
	/// Gets or sets the severity.
	/// </summary>
	/// <value>The severity.</value>
	public float Severity{
		get{return _severity;}
		set{_severity = value;}
	}
	private float _maxSeverity;
	/// <summary>
	/// Gets the max severity.
	/// </summary>
	/// <value>The max severity.</value>
	public float MaxSeverity{
		get{return _maxSeverity;}
	}
	/// <summary>
	/// Gets the remaining severity to reach the severity maximum.
	/// </summary>
	/// <value>The severity to max.</value>
	public float SeverityToMax{
		get{return _maxSeverity - _severity;}
	}
	/// <summary>
	/// Gets or sets the degree of the severity [0...1].
	/// </summary>
	/// <value>The degree.</value>
	public float Degree{
		get{return _severity/_maxSeverity;}
		set{
			value = Mathf.Clamp(value,0f,1f);
			_severity = value * _maxSeverity;
		}
	}
	/// <summary>
	/// Gets a value indicating whether this <see cref="Need"/> is satisfied.
	/// </summary>
	/// <value><c>true</c> if this need is satisfied; otherwise, <c>false</c>.</value>
	public bool IsSatisfied{
		get{return Degree<.25f;}
	}
	/// <summary>
	/// Gets a value indicating whether this <see cref="Need"/> is dangerous.
	/// </summary>
	/// <value><c>true</c> if this need is dangerous; otherwise, <c>false</c>.</value>
	public bool isDangerous{
		get{return Degree>.75f;}
	}

	private Need(){}
	
	public Need(string name, float maxSeverity, int potentialDanger=0){
		_name = name;
		_maxSeverity = maxSeverity;
		_potentialDanger = potentialDanger;
	}

	public void IncreaseSeverity(float increase){
		Severity+=increase;
	}
	public void DecreaseSeverity(float decrease){
		Severity-=decrease;
	}
	
	public class NeedComparerBySeverityToMax : IComparer<Need>{
		public int Compare (Need x, Need y)
		{
			return (int)(x.SeverityToMax*1000f - y.SeverityToMax*1000f);
		}
	}

	public class NeedComparerByDegree : IComparer<Need>{
		public int Compare (Need x, Need y)
		{
			return (int)(y.Degree*1000f - x.Degree*1000f);
		}
	}
	/// <summary>
	/// Orders by most dangerous to least
	/// </summary>
	public class NeedComparerByPotentialDanger : IComparer<Need>{
		public int Compare (Need x, Need y)
		{
			return y.PotentialDanger - x.PotentialDanger;
		}
	}
}
