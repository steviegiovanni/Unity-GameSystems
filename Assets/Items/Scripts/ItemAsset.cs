using UtilitySystems.XmlDatabase;

namespace GameSystems.Items{
	public class ItemAsset : XmlDatabaseAsset{
		public bool Stackable { get; set;}

		public ItemAsset():base(){
			Stackable = false;
		}

		public ItemAsset(int id):base(id){
			Stackable = false;
		}

		public virtual Item CreateInstance(){
			return new Item (this);
		}
			
		public override void OnSaveAsset (System.Xml.XmlWriter writer)
		{
			writer.WriteElementString ("Stackable", Stackable.ToString ());
			/*writer.WriteStartElement("Stackable");
			writer.SetAttr("Value",Stackable);
			writer.WriteEndElement();*/
		}
			
		public override void OnLoadAsset (System.Xml.XmlReader reader)
		{
			
			switch (reader.Name) {
			case "Stackable":
				{
					bool val;
					bool.TryParse(reader.ReadElementContentAsString(),out val);
					Stackable = val;
					//Stackable = reader.GetBoolAttribute ("Value", false);
				}
				break;
			}
		}
	}
}