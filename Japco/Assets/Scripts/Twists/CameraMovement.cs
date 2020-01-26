using Outfrost;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			Vector3 position = camera.transform.position;
			if (Mathf.Abs(maxCameraX - position.x) < Mathf.Epsilon) {
				SceneLoader.LoadNextScene();
			}
			
			float horizontal = Input.GetAxis("Horizontal");
			
			position.x += horizontal * playerMovement.movementSpeed * Time.deltaTime;
			position.x = Mathf.Clamp(position.x, minCameraX, maxCameraX);

			camera.transform.position = position;
		}

	}

}
