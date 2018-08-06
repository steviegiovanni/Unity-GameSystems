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
				Debug.Log (itemAsset.GetType ().ToString());
				if (typeof(PositionUsableItemAsset).IsAssignableFrom (itemAsset.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetPositionEffectNames (), (index) => {
						var itemEffectAsset = EffectEditorUtility.CreatePositionEffectAsset (index);
						itemAsset.Effects.Add (itemEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
					}).ShowAsContext ();
				} else if (typeof(TargetUsableItemAsset).IsAssignableFrom (itemAsset.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetTargetEffectNames (), (index) => {
						var itemEffectAsset = EffectEditorUtility.CreateTargetEffectAsset (index);
						itemAsset.Effects.Add (itemEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
					}).ShowAsContext ();
				} else {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreateAsset (index);
						itemAsset.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
					}).ShowAsContext ();
				}
			}

			GUILayout.EndVertical ();
		}

		public void OnGUI (object asset, ItemDatabaseWindow itemDBWindow)
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

				bool clicked = GUILayout.Toggle (i == itemDBWindow.SelectedEffectIndex, effect.GetType ().Name, ToggleButtonStyle, GUILayout.Width(150));
				if (clicked != (i == itemDBWindow.SelectedEffectIndex)) {
					if (clicked) {
						itemDBWindow.SelectedEffectIndex = i;
						GUI.FocusControl (null);
					} else {
						itemDBWindow.SelectedEffectIndex = -1;
					}
				}
				GUILayout.EndHorizontal ();

				if (itemDBWindow.SelectedEffectIndex == i) {
					GUILayout.BeginVertical ();
					foreach (var extension in EffectEditorUtility.GetExtensions()) {
						if (extension.CanHandleType (effect.GetType ())) {
							extension.OnGUI (effect);
						}
					}
					GUILayout.EndVertical ();
				}

			}

			if(GUILayout.Button("Add Effect", EditorStyles.toolbarButton)){
				Debug.Log (itemAsset.GetType ().ToString());
				if (typeof(PositionUsableItemAsset).IsAssignableFrom (itemAsset.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetPositionEffectNames (), (index) => {
						var itemEffectAsset = EffectEditorUtility.CreatePositionEffectAsset (index);
						itemAsset.Effects.Add (itemEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
					}).ShowAsContext ();
				} else if (typeof(TargetUsableItemAsset).IsAssignableFrom (itemAsset.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetTargetEffectNames (), (index) => {
						var itemEffectAsset = EffectEditorUtility.CreateTargetEffectAsset (index);
						itemAsset.Effects.Add (itemEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
					}).ShowAsContext ();
				} else {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreateAsset (index);
						itemAsset.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<ItemDatabaseWindow> ();
					}).ShowAsContext ();
				}
			}

			GUILayout.EndVertical ();
		}
	}
}
