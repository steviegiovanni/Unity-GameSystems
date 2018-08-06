using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Items{
	public class TargetUsableItemAsset : UsableItemAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}

		public TargetUsableItemAsset():base(){}
		public TargetUsableItemAsset(int id): base(id){}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "TargetType":
				{
					int targetType;
					int.TryParse(reader.ReadElementContentAsString (), out targetType);
					TargetType = targetType;
				}
				break;
			case "IncludeSelf":
				{
					bool includeSelf;
					bool.TryParse (reader.ReadElementContentAsString (), out includeSelf);
					IncludeSelf = includeSelf;
				}
				break;
			}
		}

		#endregion

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);
			writer.WriteElementString ("TargetType", TargetType.ToString());
			writer.WriteElementString ("IncludeSelf", IncludeSelf.ToString ());
		}

		#endregion
	}
}
