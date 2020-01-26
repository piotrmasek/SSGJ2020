using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Outfrost;

public class FakeWin : CheckedMonoBehaviour
{
    [ExpectAttached] public TextMeshProUGUI winText;
    [ExpectAttached] public TextMeshPro tutorialText;
    [ExpectAttached] public Sprite whiteSprite;

    // Start is called before the first frame update
    void Start()
    {
        CheckReferences();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            winText.text = "YOU WIN!";
            FindObjectOfType<PlayerMovement>().enabled = false;
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            foreach (var particle in other.GetComponentsInChildren<ParticleSystem>())
            {
                // particle.shape.spriteRenderer.color = new Color(255, 0, 255);
                particle.enableEmission = false;
            }

            StartCoroutine(MakeSpriteMagentaAsync(GameObject.FindGameObjectsWithTag("Apple"), 1f));
            StartCoroutine(MakeSpriteMagentaAsync(GameObject.FindGameObjectsWithTag("Player"), 2f));
            StartCoroutine(MakeSpriteMagentaAsync(GameObject.FindGameObjectsWithTag("Platform"), 3f));
            StartCoroutine(MakeSpriteMagentaAsync(new[] { GameObject.Find("Bounds") }, 4f));
            StartCoroutine(MakeCameraBackgroundMagentaAsync(5f));

            StartCoroutine(SceneLoader.NextSceneAfterAsync(8f));
        }
    }

    public IEnumerator MakeCameraBackgroundMagentaAsync(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject.FindGameObjectWithTag("Background").SetActive(false);
        Camera.main.backgroundColor = new Color(255, 0, 255);
        winText.enabled = false;
        tutorialText.enabled = false;
    }

    public IEnumerator MakeSpriteMagentaAsync(GameObject[] objects, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        foreach (var obj in objects)
        {
            foreach (var particle in obj.GetComponentsInChildren<ParticleSystem>())
            {
                // particle.shape.spriteRenderer.color = new Color(255, 0, 255);
                particle.enableEmission = false;
            }

            foreach (var animator in obj.GetComponentsInChildren<Animator>())
            {
                animator.enabled = false;
            }

            foreach (var sprite in obj.GetComponentsInChildren<SpriteRenderer>())
            {
                //Replace sprites for player and apple, cos they have their own colors
                if (obj.tag == "Player" || obj.tag == "Apple")
                {
                    sprite.sprite = whiteSprite;
                }

                sprite.color = new Color(255, 0, 255);
            }
        }
    }
}
