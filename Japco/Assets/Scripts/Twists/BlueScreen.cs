using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueScreen : MonoBehaviour
{
    public Image blueScreenImage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            blueScreenImage.enabled = true;
            StartCoroutine(SceneLoader.NextSceneAfterAsync(5f));
        }
    }
}
