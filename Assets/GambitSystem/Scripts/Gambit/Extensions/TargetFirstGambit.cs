﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// extension to the gambit class that will target the first entry on the table of percepts
/// </summary>
public class TargetFirstGambit : Gambit, ITargetGambit {
	/// <summary>
	/// The target
	/// </summary>
	private GameObject _target;

	/// <summary>
	/// Gets or sets the target
	/// </summary>
	public GameObject Target{
		get{ return _target;}
		set{ _target = value;}
	}

	/// <summary>
	/// The perception
	/// </summary>
	private Perception _perception;

	/// <summary>
	/// Gets or sets the perception.
	/// </summary>
	public Perception Perception{
		get{ return _perception;}
		set{ _perception = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetGambit"/> class.
	/// </summary>
	public TargetFirstGambit():base(){
		_target = null;
		_perception = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetFirstGambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="target">Target.</param>
	/// <param name="perception">Perception.</param>
	public TargetFirstGambit(int priority, Skill skill, GameObject target, Perception perception):base(priority,skill){
		_target = target;
		_perception = perception;
	}

	/// <summary>
	/// override coroutine of this gambit
	/// </summary>
	public override IEnumerator Coroutine(){
		while (true) {
			if (Target == null)
				Target = FindTarget ();

			if(Target != null)
				Debug.Log (string.Format("targetting {0}",Target.name));
			yield return null;
		}
	}

	#region ITargetGambit implementation
	/// <summary>
	/// Finds the target.
	/// </summary>
	/// <returns>The target.</returns>
	public GameObject FindTarget ()
	{
		if (Perception == null)
			return null;

		GameObject target = null;
		foreach (var key in Perception.Percepts.Keys) {
			if (Perception.Percepts [key].Entity != null) {
				target = Perception.Percepts [key].Entity;
				break;
			}
		}

		return target;
	}

	#endregion
}
