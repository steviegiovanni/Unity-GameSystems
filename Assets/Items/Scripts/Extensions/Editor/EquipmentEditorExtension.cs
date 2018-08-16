using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase.Editor;
using System;
using UnityEditor;


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

		public override void OnGUI (object asset){
		}
	}
}
