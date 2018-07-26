using System;
using System.Collections.Generic;

namespace GameSystems.SaveSystem{
	[Serializable]
	public class PersistentData{
		public string name = "test";
		public int anotherIValue = 0;
		public List<string> stringList; 

		//[NonSerialized]
		//public int ivalue = 13;
		//[NonSerialized]
		//public float fvalue = 7.5f;

		public PersistentData(){
			stringList = new List<string> ();
			stringList.Add ("shalala");
			stringList.Add ("dududu");
		}
	}

	[Serializable]
	public class deriv1:PersistentData{
		public string derivName = "badam";
		public List<string> derivlist;

		public deriv1():base(){
			name = "deriv1";
			derivlist = new List<string> ();
			derivlist.Add ("qwerty");
			derivlist.Add ("asdf");
			derivlist.Add ("zxcvb");
		}
	}
}
