using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.Effects;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Items{
	public class UsableItemAsset : ItemAsset {
		/// <summary>
		/// The effects.
		/// </summary>
		private List<EffectAsset> _effects;
		public List<EffectAsset> Effects{
			get{ 
				if (_effects == null)
					_effects = new List<EffectAsset> ();
				return _effects;
			}
		}

		public UsableItemAsset():base(){}
		public UsableItemAsset(int id): base(id){}

		public override void OnSaveAsset (System.Xml.XmlWriter writer)
		{
			base.OnSaveAsset (writer);
			foreach(var effect in Effects){
				writer.WriteStartElement ("Effect");
				writer.SetAttr ("AssetType", effect.GetType ().Name);
				effect.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}

		public override void OnLoadAsset (System.Xml.XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Effect":
				{
					string skillEffectAssetType = reader.GetAttrString ("AssetType", "");
					var asset = EffectUtility.CreateAssetOfType(skillEffectAssetType);
					if (asset != null) {
						Effects.Add (asset);
						Effects[Effects.Count-1].OnLoadAsset(reader);
					}
				}
				break;
			}
		}
	}
}
