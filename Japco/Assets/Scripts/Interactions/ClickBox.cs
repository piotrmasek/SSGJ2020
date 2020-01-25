using UnityEngine;

namespace Interactions {

	public class ClickBox : MonoBehaviour {

		public bool Clicked { get; private set; }

		private void OnMouseDown() {
			Clicked = true;
		}

	}

}