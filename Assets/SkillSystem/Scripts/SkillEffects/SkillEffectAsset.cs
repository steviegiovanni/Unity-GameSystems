﻿using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Skill effect asset. Represents an xml entry on the skill collection database
	/// </summary>
	public class SkillEffectAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public float Delay{ get; set;}

		public virtual SkillEffect CreateInstance(){
			return new SkillEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("Delay", Delay);
		}

		#endregion

		#region IXmlOnLoadAsset implementation

		public void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "SkillEffect":
				{
					Delay = reader.GetAttrFloat ("Delay", 0.0f);
				}
				break;
			}
		}

		#endregion
	}
}