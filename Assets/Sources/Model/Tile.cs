using System;
using UnityEngine;

namespace TRPG {
	[Serializable]
	public class Tile {
		[SerializeField]
		private IntVector3 position;

		public IntVector2 Position {get {return new IntVector2(position.x, position.z);}}
		public int Height {get{return position.y;}}

		public GameObject View;

		public void UpdateView() {
			if(View!=null)
				View.transform.localPosition = position;
		}
	}
}