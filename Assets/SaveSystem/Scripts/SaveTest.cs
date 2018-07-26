using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GameSystems.SaveSystem;

public class SaveTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.S)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
			PersistentData pd = new PersistentData();
			bf.Serialize (file, pd);
			deriv1 der = new deriv1 ();
			bf.Serialize (file,der);
			file.Close ();
		}

		if (Input.GetKeyUp (KeyCode.L)) {
			Debug.Log ("loading=====");
			long position = 0;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			while (position < file.Length) {
				if (position < file.Length) {
					file.Seek (position, SeekOrigin.Begin);

					PersistentData pd = (PersistentData)bf.Deserialize (file);
					Debug.Log ("*********"+pd.GetType ().ToString());
					position = file.Position;
					if (pd == null)
						break;
					
					Debug.Log (pd.name);
					Debug.Log (pd.anotherIValue);
					if (pd.stringList != null) {
						foreach (var str in pd.stringList)
							Debug.Log (str);
					}

					deriv1 der = pd as deriv1;
					if (der != null) {
						if (der.derivlist != null) {
							foreach (var str2 in der.derivlist)
								Debug.Log (str2);
						}
					}
				}
			}
			file.Close ();
			Debug.Log ("end=====");
		}
	}
}
