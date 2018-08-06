using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.Items.Editor{
	static public class ItemEditorUtility{
		/// <summary>
		/// Gets an array containing all extension that can apply to a skill
		/// </summary>
		static public IEditorExtension[] GetExtensions(){
			return new IEditorExtension[] {
				new UsableItemEditorExtension()
			};
		}


		/// <summary>
		/// creates an instance of the item Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public ItemAsset CreateAsset(int index){
			switch (index) {
			case 0:
				return new ItemAsset();
			case 1:
				return new UsableItemAsset ();
			case 2:
				return new PositionUsableItemAsset ();
			case 3:
				return new TargetUsableItemAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// Gets an array of all the names of item types
		/// </summary>
		static public string[] GetNames(){
			return new string[] {
				"Item",
				"UsableItem",
				"PositionUsableItem",
				"TargetUsableItem"
			};
		}
	}
}
