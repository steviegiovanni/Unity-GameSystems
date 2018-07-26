namespace GameSystems.Items{
	public class Item{
		private string _name;
		public string Name{
			get{ return _name;}
			set{ _name = value;}
		}

		private bool _stackable = false;
		public bool Stackable{
			get{ return _stackable;}
			set{ _stackable = value;}
		}
	}
}
