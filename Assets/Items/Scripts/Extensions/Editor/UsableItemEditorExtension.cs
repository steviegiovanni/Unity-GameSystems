using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase.Editor;
using System;
using GameSystems.Effects;
using UnityEditor;
using GameSystems.Effects.Editor;

namespace GameSystems.Items.Editor{
	public class UsableItemEditorExtension : EditorExtension {
		private GUIStyle toggleButtonStyle;
		public virtual GUIStyle ToggleButtonStyle {
			get {
				if (toggleButtonStyle == null) {
					// Create custom style for stat buttons
					toggleButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
					toggleButtonStyle.alignment = TextAnchor.MiddleLeft;
				}
				return toggleButtonStyle;
			}
		}

		public override bool CanHandleType (Type type)
		{
			return typeof(UsableItemAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			GUILayout.BeginVertical ();

			UsableItemAsset itemAsset = asset as UsableItemAsset;
			for(int i = 0; i < itemAsset.Effects.Count; i++){
				EffectAsset effect = itemAsset.Effects[i];
				GUILayout.BeginHorizontal (EditorStyles.toolbar);

				if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
					&& EditorUtility.DisplayDialog ("Remove effect", "Are you sure you want to delete the effect?", "Delete", "Cancel")) {
					itemAsset.Effects.RemoveAt (i);
				}

				GUILayout.Label (effect.GetType ().Name, ToggleButtonStyle);
				GUILayout.EndHorizontal ();


				GUILayout.BeginVertical ();
				foreach (var extension in EffectEditorUtility.GetExtensions()) {
					Debug.Log (effect.GetType ().ToString ());
					if (extension.CanHandleType (effect.GetType ())) {
						extension.OnGUI (effect);
					} else {
						Debug.Log (extension.GetType ().ToString ());
					}
				}
				GUILayout.EndVertical ();

			}

			if(GUILayout.Button("Add Effect", EditorStyles.toolbarButton)){
				/*if (typeof(PositionSkillAsset).IsAssignableFrom (skill.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetPositionEffectNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreatePositionEffectAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				} else if (typeof(TargetSkillAsset).IsAssignableFrom (skill.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetTargetEffectNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreateTargetEffectAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				} else {*/
				XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetNames (), (index) => {
					var skillEffectAsset = EffectEditorUtility.CreateAsset (index);
					itemAsset.Effects.Add (skillEffectAsset);
					EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
				}).ShowAsContext ();
				//}
			}

			GUILayout.EndVertical ();
		}

		public override void OnGUI (object asset, ItemDatabaseWindow itemDBWindow)
		{
			GUILayout.BeginVertical ();

			UsableItemAsset itemAsset = asset as UsableItemAsset;
			for(int i = 0; i < itemAsset.Effects.Count; i++){
				EffectAsset effect = itemAsset.Effects[i];
				GUILayout.BeginHorizontal (EditorStyles.toolbar);

				if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
					&& EditorUtility.DisplayDialog ("Remove effect", "Are you sure you want to delete the effect?", "Delete", "Cancel")) {
					itemAsset.Effects.RemoveAt (i);
				}

				GUILayout.Label (effect.GetType ().Name, ToggleButtonStyle);
				GUILayout.EndHorizontal ();


				GUILayout.BeginVertical ();
				foreach (var extension in EffectEditorUtility.GetExtensions()) {
					Debug.Log (effect.GetType ().ToString ());
					if (extension.CanHandleType (effect.GetType ())) {
						extension.OnGUI (effect);
					} else {
						Debug.Log (extension.GetType ().ToString ());
					}
				}
				GUILayout.EndVertical ();

			}

			if(GUILayout.Button("Add Effect", EditorStyles.toolbarButton)){
				/*if (typeof(PositionSkillAsset).IsAssignableFrom (skill.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetPositionEffectNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreatePositionEffectAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				} else if (typeof(TargetSkillAsset).IsAssignableFrom (skill.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetTargetEffectNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreateTargetEffectAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				} else {*/
				XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetNames (), (index) => {
					var skillEffectAsset = EffectEditorUtility.CreateAsset (index);
					itemAsset.Effects.Add (skillEffectAsset);
					EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
				}).ShowAsContext ();
				//}
			}

			GUILayout.EndVertical ();
		}
	}
}
