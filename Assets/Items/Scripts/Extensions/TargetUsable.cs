using GameSystems.Effects;
using UnityEngine;

namespace GameSystems.Items{
	public class TargetUsable : UsableItem, IHasTargetEffects
	{
		/// <summary>
		/// the target of the item, assigned on runtime
		/// </summary>
		private GameObject _target;
		public GameObject Target {
			get {return _target;}
			set {_target = value;}
		}

		#region IHasTargetEffects implementation

		public GameObject GetTarget ()
		{
			return Target;
		}

		#endregion
	}
}

