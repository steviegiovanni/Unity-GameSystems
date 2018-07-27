using System;
using UnityEngine;
using UnityEditor;
using UtilitySystems.XmlDatabase.Editor;
using RPGSystems.StatSystem.Database;
using RPGSystems.StatSystem.Editor;

namespace GameSystems.GambitSystem.Editor{
	public class StatGCEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(StatGambitConditionAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			StatGambitConditionAsset gambitConditionAsset = asset as StatGambitConditionAsset;
			GUILayout.Label ("Name ", EditorStyles.toolbarButton,GUILayout.Width(50));

			//gambitConditionAsset.StatName = GUILayout.TextField(gambitConditionAsset.StatName, GUILayout.Width (100));

			var baseStatType = RPGStatTypeDatabase.Instance.Get(gambitConditionAsset.StatName, true);
			if (GUILayout.Button(baseStatType == null ? "Assign Type" : baseStatType.Name, EditorStyles.toolbarButton, GUILayout.Width(100))) {
				XmlDatabaseEditorUtility.ShowContext(RPGStatTypeDatabase.Instance, (statTypeAsset) => {
					gambitConditionAsset.StatName = statTypeAsset.Id;
				}, typeof(RPGStatTypeWindow));
			}

			GUILayout.Label ("Value ", EditorStyles.toolbarButton,GUILayout.Width(50));
			gambitConditionAsset.StatValue = EditorGUILayout.FloatField(gambitConditionAsset.StatValue,GUILayout.Width(50));
		}

		#endregion


	}
}
