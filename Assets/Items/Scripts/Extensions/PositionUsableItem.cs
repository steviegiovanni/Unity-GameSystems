using UnityEngine;
using GameSystems.Effects;

namespace GameSystems.Items{
	public class PositionUsableItem : UsableItem, IHasPositionEffects
	{
		/// <summary>
		/// The position for the item
		/// </summary>
		private Vector3 _position;
		public Vector3 Position{
			get{ return _position;}
			set{ _position = value;}
		}

		#region IHasPositionEffects implementation

		public Vector3 GetPosition ()
		{
			return Position;
		}

		#endregion

		public PositionUsableItem(PositionUsableItemAsset asset):base(asset){}
	}
}

