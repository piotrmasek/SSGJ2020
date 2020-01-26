using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChecker : MonoBehaviour
{

    public GameObject player;
    public GameObject floor;
    public GameObject camera;

    int jcount;
    bool shakedOnce = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        jcount = player.GetComponent<PlayerMovement>().jumpCount;
        if (jcount == 1 && shakedOnce == true)
        {
            floor.transform.Rotate(new Vector3(-15, 0, 0));
            floor.transform.position = new Vector3(floor.transform.position.x, floor.transform.position.y - 15, floor.transform.position.z);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 15, camera.transform.position.z);

           //Camera Shake
            camera.transform.Rotate(new Vector3(5, 0, 0));
            shakedOnce = false;
        }
        else if (jcount > 1)
        {
            floor.transform.position = new Vector3(floor.transform.position.x, -300, floor.transform.position.z);
        }
    }
}
