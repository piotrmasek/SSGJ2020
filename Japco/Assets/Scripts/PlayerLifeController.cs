using System.Collections;
using System.Collections.Generic;
using Outfrost;
using UnityEngine;

public class PlayerLifeController : CheckedMonoBehaviour
{

    [ExpectAttached] public Animator animator;
    [ExpectAttached] public SpriteRenderer PlayerSprite;

    public void Die()
    {
        PlayerSprite.enabled = false;
        Debug.LogWarning("Implement dying animation!"); //TODO: dying animation
        StartCoroutine(SceneLoader.NextSceneAfterAsync(2f));
    }
}
