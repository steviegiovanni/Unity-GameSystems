using System;
using UnityEngine;
using UnityEditor;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;
using RPGSystems.StatSystem.Database;
using RPGSystems.StatSystem.Editor;

namespace GameSystems.Effects.Editor{
	public class StatGlobalEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(StatGlobalEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			StatGlobalEffectAsset effectAsset = asset as StatGlobalEffectAsset;

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Source Stat Name", GUILayout.Width (150));
			var baseStatType = RPGStatTypeDatabase.Instance.Get(effectAsset.StatBase, true);
			if (GUILayout.Button(baseStatType == null ? "Assign Type" : baseStatType.Name, EditorStyles.toolbarButton, GUILayout.Width(100))) {
				XmlDatabaseEditorUtility.ShowContext(RPGStatTypeDatabase.Instance, (statTypeAsset) => {
					effectAsset.StatBase = statTypeAsset.Id;
				}, typeof(RPGStatTypeWindow));
			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Stat Name", GUILayout.Width (150));
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

			GUILayout.Space (10);

			GUILayout.BeginHorizontal ();
			effectAsset.IncludeSelf = GUILayout.Toggle (effectAsset.IncludeSelf, "Include self ", GUILayout.Width (150));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Type", GUILayout.Width (150));
			effectAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField((PerceptionTags)effectAsset.TargetType));
			GUILayout.EndHorizontal ();


		}

		#endregion


	}
}
