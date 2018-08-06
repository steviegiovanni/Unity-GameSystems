namespace GameSystems.Items{
	/// <summary>
	/// base class for item
	/// </summary>
	public class Item{
		/// <summary>
		/// name of the item
		/// </summary>
		private string _name;
		public string Name{
			get{ return _name;}
			set{ _name = value;}
		}

		/// <summary>
		/// whether the item is stackable or not
		/// </summary>
		private bool _stackable = false;
		public bool Stackable{
			get{ return _stackable;}
			set{ _stackable = value;}
		}

		public Item(ItemAsset asset){
			Name = asset.Name;
			Stackable = asset.Stackable;
		}

		public Item(){}
	}
}
