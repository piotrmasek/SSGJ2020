using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

	public static void LoadNextScene() {
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex + 1);
	}

	public static void LoadStartScene() {
		SceneManager.LoadScene(0);
	}

	public static void QuitGame() {
		Application.Quit();
	}

}
