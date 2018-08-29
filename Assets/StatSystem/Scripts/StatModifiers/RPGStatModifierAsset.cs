using UnityEngine;
using System.Collections;
using UtilitySystems.XmlDatabase;
using System;
using System.Xml;

namespace RPGSystems.StatSystem {
    [System.Serializable]
    public abstract class RPGStatModifierAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
        public int AssignedStatId;

        public float Value;
        public bool Stacks;

        public abstract RPGStatModifier CreateInstance();

		protected virtual T Internal_CreateInstance<T>() where T : RPGStatModifier {
            var mod = System.Activator.CreateInstance<T>() as RPGStatModifier;
            mod.Value = Value;
            mod.Stacks = Stacks;
            return mod as T;
        }

        public void OnSaveAsset(XmlWriter writer) {
            writer.WriteAttributeString("Value", Value.ToString());
            writer.WriteAttributeString("Stacks", Stacks.ToString());
        }

        public void OnLoadAsset(XmlReader reader) {
            reader.GetAttrFloat("Value", 0f);
            reader.GetBoolAttribute("Stacks", true);
        }
    }
}