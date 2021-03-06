﻿using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Effects{
	/// <summary>
	/// effect asset that affects stats of entities room wide
	/// </summary>
	public class StatGlobalEffectAsset : EffectAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}

		public float Modifier { get; set;}
		public int StatBase {get;set;}
		public int FlatValue{ get; set;}
		public int TargetStat{ get; set;}

		public override Effect CreateInstance(){
			return new StatGlobalEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);

			writer.SetAttr ("TargetType", TargetType);
			writer.SetAttr ("IncludeSelf", IncludeSelf);
			writer.SetAttr ("Modifier", Modifier);
			writer.SetAttr ("StatBase", StatBase);
			writer.SetAttr ("FlatValue", FlatValue);
			writer.SetAttr ("TargetStat", TargetStat);
		}

		#endregion

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Effect":
				{
					TargetType = reader.GetAttrInt ("TargetType", 0);
					IncludeSelf = reader.GetBoolAttribute ("IncludeSelf", true);
					Modifier = reader.GetAttrFloat("Modifier",0.0f);
					StatBase = reader.GetAttrInt ("StatBase", 0);
					FlatValue = reader.GetAttrInt ("FlatValue", 0);
					TargetStat = reader.GetAttrInt ("TargetStat", 0);
				}
				break;
			}
		}

		#endregion
	}
}
