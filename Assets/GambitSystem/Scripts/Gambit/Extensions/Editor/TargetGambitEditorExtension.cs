﻿using System;
using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class TargetGambitEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetGambitAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetGambitAsset gambitAsset = asset as TargetGambitAsset;
			GUILayout.BeginHorizontal ();
			gambitAsset.IncludeSelf = GUILayout.Toggle (gambitAsset.IncludeSelf, "Include self ", GUILayout.Width (150));
			gambitAsset.TargetType = (int)(GambitTags)(EditorGUILayout.EnumFlagsField("Target Type ", (GambitTags)gambitAsset.TargetType));
			GUILayout.EndHorizontal ();
		}

		#endregion


	}
}