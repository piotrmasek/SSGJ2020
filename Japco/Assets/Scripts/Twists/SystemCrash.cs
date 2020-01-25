using UnityEngine;
using Outfrost;

namespace Twists {

	public class SystemCrash : MonoBehaviour {

		public ClickBox clickBox;

		private bool triggered = false;

		private void Start() {
			if (!Util.IsPrefab(gameObject)) {
				if (! clickBox) {
					Debug.LogError("No ClickBox reference attached");
					enabled = false;
				}
			}
		}

		private void Update() {
			if (!Util.IsPrefab(gameObject)) {
				if (! triggered && clickBox.Clicked) {
					triggered = true;
					// Here is where we do the crash visuals lol
				}
			}
		}

	}

}
