using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase.Editor;
using System;
using UnityEditor;
using RPGSystems.StatSystem;
using RPGSystems.StatSystem.Editor;
using RPGSystems.StatSystem.Database;


namespace GameSystems.Items.Editor{
	public class EquipmentEditorExtension : EditorExtension {
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
			return typeof(EquipmentAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
		}

		public void OnGUI (object asset, ItemDatabaseWindow itemDBWindow){
			GUILayout.BeginVertical ();

			EquipmentAsset itemAsset = asset as EquipmentAsset;

			GUILayout.BeginHorizontal (EditorStyles.toolbar);

			GUILayout.Label (" ", EditorStyles.toolbarButton, GUILayout.Width (30));
			GUILayout.Label("Type", EditorStyles.toolbarButton);
			GUILayout.Button("StatId",EditorStyles.toolbarButton);
			GUILayout.Label("Value",EditorStyles.toolbarButton, GUILayout.Width (50));
			GUILayout.Label("Stacks",EditorStyles.toolbarButton, GUILayout.Width (50));

			/*bool clicked = GUILayout.Toggle (i == itemDBWindow.SelectedEffectIndex, modifier.GetType ().Name, ToggleButtonStyle, GUILayout.Width(150));
				if (clicked != (i == itemDBWindow.SelectedEffectIndex)) {
					if (clicked) {
						itemDBWindow.SelectedEffectIndex = i;
						GUI.FocusControl (null);
					} else {
						itemDBWindow.SelectedEffectIndex = -1;
					}
				}*/
			GUILayout.EndHorizontal ();

			for(int i = 0; i < itemAsset.Mods.Count; i++){
				RPGStatModifierAsset modifier = itemAsset.Mods[i];
				GUILayout.BeginHorizontal (EditorStyles.toolbar);

				if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
					&& EditorUtility.DisplayDialog ("Remove modifier", "Are you sure you want to delete the modifier?", "Delete", "Cancel")) {
					itemAsset.Mods.RemoveAt (i);
				}

				GUILayout.Label(modifier.GetType ().Name,EditorStyles.toolbarButton);
				//modifier.AssignedStatId = EditorGUILayout.IntField (modifier.AssignedStatId);

				var statType = RPGStatTypeDatabase.Instance.Get(modifier.AssignedStatId, true);
				if (GUILayout.Button(statType == null ? "Assign Stat" : statType.Name, EditorStyles.toolbarButton)) {
					XmlDatabaseEditorUtility.ShowContext(RPGStatTypeDatabase.Instance, (statTypeAsset) => {
						modifier.AssignedStatId = statTypeAsset.Id;
						Debug.Log("Assigning stat to stat type " + statTypeAsset.Id);
					}, typeof(RPGStatTypeWindow));
				}


				modifier.Value = EditorGUILayout.FloatField (modifier.Value,GUILayout.Width(50));
				modifier.Stacks = EditorGUILayout.Toggle (modifier.Stacks,GUILayout.Width(50));

				/*bool clicked = GUILayout.Toggle (i == itemDBWindow.SelectedEffectIndex, modifier.GetType ().Name, ToggleButtonStyle, GUILayout.Width(150));
				if (clicked != (i == itemDBWindow.SelectedEffectIndex)) {
					if (clicked) {
						itemDBWindow.SelectedEffectIndex = i;
						GUI.FocusControl (null);
					} else {
						itemDBWindow.SelectedEffectIndex = -1;
					}
				}*/
				GUILayout.EndHorizontal ();
			}

			if(GUILayout.Button("Add Modifier", EditorStyles.toolbarButton)){
				XmlDatabaseEditorUtility.GetGenericMenu(RPGStatModifierEditorUtility.GetNames(), (selectedIndex) => {
					itemAsset.Mods.Add(RPGStatModifierEditorUtility.CreateAsset(selectedIndex));
				}).ShowAsContext();
			}

			GUILayout.EndVertical ();
		}
	}
}
