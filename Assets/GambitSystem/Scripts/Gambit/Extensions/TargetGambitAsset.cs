﻿using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// gambit asset for gambits that requires target
	/// </summary>
	public class TargetGambitAsset : GambitAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}

		public override Gambit CreateInstance(){
			return new TargetGambit (this);
		}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Gambit":
				{
					TargetType = reader.GetAttrInt ("TargetType", 0);
					IncludeSelf = reader.GetBoolAttribute("IncludeSelf", false);
				}
				break;
			default:
				{
				}
				break;
			}
		}

		#endregion

		#region IXmlOnSaveAsset implementation
		public override void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("TargetType", TargetType);
			writer.SetAttr ("IncludeSelf", IncludeSelf);

			base.OnSaveAsset (writer);
		}
		#endregion
	}
}
