using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSystems.Effects;

namespace GameSystems.Items{
	/// <summary>
	/// item class that is usable
	/// </summary>
	public class UsableItem : Item
	{
		/// <summary>
		/// the user of the item, assigned during runtime
		/// </summary>
		private GameObject _user;
		public GameObject User{
			get{ return _user; }
			set{ _user = value; }
		}

		/// <summary>
		/// The effects
		/// </summary>
		private List<Effect> _effects;
		public List<Effect> Effects{
			get{ 
				if (_effects == null)
					_effects = new List<Effect> ();
				return _effects;
			}
		}

		/// <summary>
		/// will be called when the item is used. just go through each effect and apply
		/// unlike with skills all effects are instantaneous
		/// </summary>
		public void UseItem(){
			Debug.Log ("Using " + Name);
			for (int i = 0; i < Effects.Count; i++) {
				Effects [i].ApplyEffect ();
			}
		}
	}
}

