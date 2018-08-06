using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Items.Database{
	public class ItemDatabase : AbstractXmlDatabase<ItemAsset> {
		static private ItemDatabase _instance = null;
		static public ItemDatabase Instance {
			get {
				if (_instance == null) {
					_instance = new ItemDatabase();
					_instance.LoadDatabase();
				}
				return _instance;
			}
		}

		public override string DatabaseName { get { return @"ItemDatabase.xml"; } }
		public override string DatabasePath { get { return @"Databases/SkillSystem/"; } }

		public override ItemAsset CreateAssetOfType(string type) {
			if (type == typeof(ItemAsset).Name) {
				return new ItemAsset (this.GetNextHighestId ());
			} else if (type == typeof(UsableItemAsset).Name) {
				return new UsableItemAsset (this.GetNextHighestId ());
			} else if (type == typeof(PositionUsableItemAsset).Name) {
				return new PositionUsableItemAsset (this.GetNextHighestId ());
			}	else if (type == typeof(TargetUsableItemAsset).Name) {
				return new TargetUsableItemAsset (this.GetNextHighestId ());
			}
			return null;
		}
	}
}
