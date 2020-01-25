using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float JumpSpeed = 10f;
    public float MovementSpeed = 5f;

    Rigidbody2D rigidBody;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(horizontal * MovementSpeed, rigidBody.velocity.y);
        
        if(Input.GetButton("Jump") && isGrounded)
        {
            Jump();    
        }

        if(!isGrounded)
        {
            ProcessGravity();
        }
    }

    void Jump()
    {
        Debug.Log("Jump");
        rigidBody.velocity += new Vector2(0f, JumpSpeed);


    }

    void ProcessGravity()
    {
        Debug.Log(Physics2D.gravity);
        rigidBody.velocity -= Physics2D.gravity * Time.deltaTime; //TODO: doesnt work, lol
    }
}
