using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofChecker : MonoBehaviour
{
    public GameObject roofImage;
    public GameObject player;
    public Animation anim;

    private bool rekt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Vector3 roofPos = roofImage.GetComponent<Transform>().transform.position;
        Vector3 playerPos = player.GetComponent<Transform>().transform.position;
        if (!rekt) roofImage.transform.position = new Vector3(playerPos.x, roofPos.y, roofPos.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        roofImage.GetComponent<SpriteRenderer>().enabled = true;
        var playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        playerSprite.enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        rekt = true;
        StartCoroutine(SceneLoader.NextSceneAfterAsync(2.0f));
    }
}
