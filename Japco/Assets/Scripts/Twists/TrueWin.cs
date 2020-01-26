using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrueWin : MonoBehaviour
{

    public TextMeshProUGUI winText;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerMovement>().enabled = false;
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            foreach (var particle in other.GetComponentsInChildren<ParticleSystem>())
            {
                // particle.shape.spriteRenderer.color = new Color(255, 0, 255);
                particle.enableEmission = false;
            }
            other.GetComponentInChildren<Animator>().enabled = false;


            winText.text = "WE ARE DOOMED";
            winText.color = new Color(0x0, 0x81, 0xEE);
            var apple = GameObject.FindGameObjectWithTag("Apple");
            var pos = apple.transform.position;
            Destroy(GameObject.FindGameObjectWithTag("Apple"));
            for (int i = 0; i < 10; i++)
            {
                StartCoroutine(SpawnParticleAsync(particle1, pos, i));
                StartCoroutine(SpawnParticleAsync(particle2, pos, i));
            }

            StartCoroutine(SceneLoader.NextSceneAfterAsync(15f));
        }
    }

    public IEnumerator SpawnParticleAsync(ParticleSystem system, Vector3 pos, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(system, pos, Quaternion.identity);
    }
}
