using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Outfrost;

public static class SceneLoader
{
    public static void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex < SceneManager.sceneCountInBuildSettings
                               ? nextSceneIndex
                               : 0);
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
