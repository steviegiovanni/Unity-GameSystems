using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.GambitSystem{
	public class StatGambitConditionAsset : GambitConditionAsset {
		public float StatValue { get; set;}
		public int StatName{ get; set;}

		public override GambitCondition CreateInstance(){
			return new StatGambitCondition (this);
		}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Condition":
				{
					StatValue = reader.GetAttrFloat ("StatValue", 0.0f);
					StatName = reader.GetAttrInt ("StatName", 0);
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
			base.OnSaveAsset (writer);
			writer.SetAttr ("StatName", StatName);
			writer.SetAttr ("StatValue", StatValue);
		}

		#endregion


	}
}