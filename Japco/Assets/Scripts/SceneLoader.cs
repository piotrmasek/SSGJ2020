using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SceneLoader
{
    public static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public static void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static IEnumerator NextSceneAfterAsync(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneLoader.LoadNextScene();
    }
}
