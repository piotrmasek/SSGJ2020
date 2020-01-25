using UnityEngine;

using Interactions;
using Outfrost;
using TMPro;

namespace Twists {

	public class SystemCrash : CheckedMonoBehaviour {

		[ExpectAttached]
		public ClickBox clickBox;
		[ExpectAttached]
		public TextMeshProUGUI textObject;
		[ExpectAttached]
		public TextAsset segfault;

		private bool triggered = false;

		private void Start() {
			CheckReferences();
		}

		private void Update() {
			if (!Util.IsPrefab(gameObject)) {
				if (! triggered && clickBox.Clicked) {
					triggered = true;
					// TODO make game freeze
					textObject.text = segfault.text;
				}
			}
		}

	}

}
