using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	public class SkillPrerequisiteAsset : IXmlOnSaveAsset, IXmlOnLoadAsset  {
		public int StatValue { get; set;}
		public int StatName{ get; set;}

		public virtual SkillPrerequisite CreateInstance(){
			return new SkillPrerequisite (this);
		}

		#region IXmlOnLoadAsset implementation

		public void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Prerequisite":
				{
					StatValue = reader.GetAttrInt ("StatValue", 0);
					StatName = reader.GetAttrInt ("StatName", 0);
				}
				break;
			}
		}

		#endregion

		#region IXmlOnSaveAsset implementation

		public void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("StatName", StatName);
			writer.SetAttr ("StatValue", StatValue);
		}

		#endregion
	}
}
