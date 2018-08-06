using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase.Editor;
using GameSystems.Items.Database;
using UnityEditor;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Items.Editor{
	public class ItemDatabaseWindow : XmlDatabaseWindowSimple<ItemAsset> {
		private ItemDatabase _database = null;

		[MenuItem("Window/Game Systems/Item Database")]
		static public void ShowWindow() {
			var wnd = GetWindow<ItemDatabaseWindow>();
			wnd.titleContent.text = "Item Database";
			wnd.Show();
		}

		protected override AbstractXmlDatabase<ItemAsset> GetDatabaseInstance() {
			if (_database == null) {
				_database = new ItemDatabase();
				_database.LoadDatabase();
			}
			return _database;
		}

		protected override void DisplayAssetGUI(ItemAsset asset) {
			GUILayout.BeginVertical("Box");

			GUILayout.BeginHorizontal();
			GUILayout.Label("Name", GUILayout.Width(80));
			asset.Name = EditorGUILayout.TextField(asset.Name);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Stackable", GUILayout.Width(80));
			asset.Stackable = EditorGUILayout.Toggle(asset.Stackable);
			GUILayout.EndHorizontal();

			GUILayout.BeginVertical (EditorStyles.helpBox);
			foreach (var extension in ItemEditorUtility.GetExtensions()) {
				if (extension.CanHandleType (asset.GetType()))
					extension.OnGUI (asset);
			}
			GUILayout.EndVertical ();

			GUILayout.EndVertical();
		}

		public override void DisplayGUIFooter() {
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Add New", EditorStyles.toolbarButton)) {
				XmlDatabaseEditorUtility.GetGenericMenu (ItemEditorUtility.GetNames (), (index) => {
					var itemAsset = ItemEditorUtility.CreateAsset (index);
					itemAsset.Id = GetDatabaseInstance().GetNextHighestId();
					GetDatabaseInstance().Add(itemAsset);
					EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
				}).ShowAsContext ();

				//OnAddNewAssetClick();
			}

			DisplayLoadButton();
			DisplaySaveButton();

			GUILayout.EndHorizontal();
		}
	}
}
