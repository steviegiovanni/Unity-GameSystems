using System;
using UnityEngine;
using UnityEditor;
using UtilitySystems.XmlDatabase.Editor;
using RPGSystems.StatSystem.Database;
using RPGSystems.StatSystem.Editor;

namespace GameSystems.Effects.Editor{
	public class TargetStatEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetStatEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetStatEffectAsset effectAsset = asset as TargetStatEffectAsset;

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Source Stat Type", GUILayout.Width (150));
			//effectAsset.StatBase= EditorGUILayout.TextField (effectAsset.StatBase);

			var baseStatType = RPGStatTypeDatabase.Instance.Get(effectAsset.StatBase, true);
			if (GUILayout.Button(baseStatType == null ? "Assign Type" : baseStatType.Name, EditorStyles.toolbarButton, GUILayout.Width(100))) {
				XmlDatabaseEditorUtility.ShowContext(RPGStatTypeDatabase.Instance, (statTypeAsset) => {
					effectAsset.StatBase = statTypeAsset.Id;
				}, typeof(RPGStatTypeWindow));
			}

			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Stat Type", GUILayout.Width (150));
			//effectAsset.TargetStat= EditorGUILayout.TextField (effectAsset.TargetStat);

			var targetStatType = RPGStatTypeDatabase.Instance.Get(effectAsset.StatBase, true);
			if (GUILayout.Button(targetStatType == null ? "Assign Type" : targetStatType.Name, EditorStyles.toolbarButton, GUILayout.Width(100))) {
				XmlDatabaseEditorUtility.ShowContext(RPGStatTypeDatabase.Instance, (statTypeAsset) => {
					effectAsset.TargetStat = statTypeAsset.Id;
				}, typeof(RPGStatTypeWindow));
			}

			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Base Value", GUILayout.Width (150));
			effectAsset.FlatValue= EditorGUILayout.IntField (effectAsset.FlatValue);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Modifier", GUILayout.Width (150));
			effectAsset.Modifier= EditorGUILayout.FloatField (effectAsset.Modifier);
			GUILayout.EndHorizontal ();
		}

		#endregion
	}
}
