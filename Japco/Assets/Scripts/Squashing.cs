using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squashing : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ceiling")
        {
            GetComponent<PlayerLifeController>().Die();
        }
    }
}
