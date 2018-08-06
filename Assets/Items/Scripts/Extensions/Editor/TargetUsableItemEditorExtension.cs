using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase.Editor;
using System;
using UnityEditor;
using GameSystems.PerceptionSystem;

namespace GameSystems.Items.Editor{
	public class TargetUsableItemEditorExtension : EditorExtension {
		public override bool CanHandleType (Type type)
		{
			return typeof(TargetUsableItemAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			GUILayout.BeginVertical ();

			TargetUsableItemAsset itemAsset = asset as TargetUsableItemAsset;

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Type", GUILayout.Width (80));
			itemAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField((PerceptionTags)itemAsset.TargetType));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Include Self", GUILayout.Width(80));
			itemAsset.IncludeSelf = GUILayout.Toggle(itemAsset.IncludeSelf,"");
			GUILayout.EndHorizontal();

			GUILayout.EndVertical ();
		}
	}
}
