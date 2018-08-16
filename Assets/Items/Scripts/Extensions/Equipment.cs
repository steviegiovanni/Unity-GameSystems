using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPGSystems.StatSystem;

namespace GameSystems.Items{
	public class Equipment: Item
	{
		private List<RPGStatModifier> _mods;
		public List<RPGStatModifier> Mods{
			get{
				if (_mods == null)
					_mods = new List<RPGStatModifier> ();
				return _mods;
			}
		}

		//public Equipment():base(){
		//}

		public Equipment(EquipmentAsset asset):base(asset){
			foreach (var mod in asset.Mods) {
				this.Mods.Add (mod.CreateInstance ());
			}
		}
	}
}
