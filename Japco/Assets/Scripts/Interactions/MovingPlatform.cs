using System;
using UnityEngine;

namespace Interactions {

	public class MovingPlatform : MonoBehaviour {

		public bool HasHitSomething { get; private set; }

		private void OnTriggerEnter2D(Collider2D other) {
			HasHitSomething = true;
		}

	}

}
