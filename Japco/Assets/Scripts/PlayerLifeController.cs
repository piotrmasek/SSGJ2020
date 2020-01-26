using System.Collections;
using System.Collections.Generic;
using Outfrost;
using UnityEngine;

public class PlayerLifeController : CheckedMonoBehaviour
{

    [ExpectAttached] public Animator animator;
    [ExpectAttached] public SpriteRenderer PlayerSprite;
    [ExpectAttached] public ParticleSystem dyingAnimation;

    public bool IsDead = false;

    public void Die()
    {
        IsDead = true;
        PlayerSprite.enabled = false;
        var particles = GetComponentsInChildren<ParticleSystem>();
        for (int i=0; i<particles.Length; i++)
        {
            particles[i].Stop();
        }
        Instantiate(dyingAnimation, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        GetComponent<PlayerMovement>().enabled = false;
        Debug.LogWarning("Implement dying animation!");
        StartCoroutine(SceneLoader.NextSceneAfterAsync(2f));
    }
}
