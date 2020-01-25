using System;
using Outfrost;
using UnityEngine;

namespace Twists {

	public class CameraMovement : CheckedMonoBehaviour {

		[ExpectAttached] public new Camera camera;
		[ExpectAttached] public PlayerMovement playerMovement;
		public float minCameraX;
		public float maxCameraX;

		private void Start() {
			CheckReferences();
			playerMovement.enabled = false;
		}

		private void Update() {
			float horizontal = Input.GetAxis("Horizontal");
			Vector3 position = camera.transform.position;
			
			position.x += horizontal * playerMovement.movementSpeed * Time.deltaTime;
			position.x = Mathf.Clamp(position.x, minCameraX, maxCameraX);

			camera.transform.position = position;
		}

	}

}
