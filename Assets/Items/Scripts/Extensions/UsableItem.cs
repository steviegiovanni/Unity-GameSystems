using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSystems.Effects;

namespace GameSystems.Items{
	public class UsableItem : Item
	{
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

		public IEnumerator ItemCoroutine(){
			yield return null;
		}
	}
}

