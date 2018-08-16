using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGSystems.StatSystem;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Items{
	public class EquipmentAsset : ItemAsset {
		private List<RPGStatModifierAsset> _mods;
		public List<RPGStatModifierAsset> Mods{
			get{ 
				if (_mods == null)
					_mods = new List<RPGStatModifierAsset> ();
				return _mods;
			}
		}

		public EquipmentAsset():base(){}
		public EquipmentAsset(int id):base(id){}

		public override void OnSaveAsset (System.Xml.XmlWriter writer)
		{
			base.OnSaveAsset (writer);
			foreach(var mod in Mods){
				writer.WriteStartElement ("Mod");
				writer.SetAttr ("AssetType", mod.GetType ().Name);
				mod.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}

		public override void OnLoadAsset (System.Xml.XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Mod":
				{
					string modAssetType = reader.GetAttrString ("AssetType", "");
					var asset = RPGStatModifierUtility.CreateAsset (modAssetType);
					if (asset != null) {
						Mods.Add (asset);
						Mods[Mods.Count-1].OnLoadAsset(reader);
					}
				}
				break;
			}
		}
	}
}