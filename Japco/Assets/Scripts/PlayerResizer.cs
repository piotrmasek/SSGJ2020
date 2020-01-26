using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResizer : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = player.GetComponent<Rigidbody2D>().velocity;

        if (player.transform.localScale.x > 60f)
        {
            SceneLoader.LoadNextScene();
        }

        if (velocity.x != 0f)
        {
            float scale = Mathf.Clamp(player.transform.localScale.x * 1.1f * (1f + Time.deltaTime), 0f, 300f);
            player.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
