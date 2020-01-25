using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float jumpTime;
    public bool isJumping = false;
    public float gravity = 2f;
    public float movementSpeed = 5f;

    public Animator animator;
    public ParticleSystem particleJump;
    public ParticleSystem particleMoveRight;
    public ParticleSystem particleMoveLeft;

    private float jumpTimeCounter;

    Rigidbody2D rigidBody;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1.0f;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(horizontal * movementSpeed, rigidBody.velocity.y);


        if(horizontal != 0f)
        {
            animator.SetBool("IsMoving", true);
            if (!particleMoveRight.isPlaying && horizontal < 0) particleMoveRight.Play();
            if (!particleMoveLeft.isPlaying && horizontal > 0) particleMoveLeft.Play();
            if (particleMoveRight.isPlaying && isJumping)
            {
                particleMoveRight.Stop();
                var particles = new ParticleSystem.Particle[particleMoveRight.particleCount];
                particleMoveRight.GetParticles(particles);
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].remainingLifetime = 0f;
                }
                particleMoveRight.SetParticles(particles);
            }
            if (particleMoveLeft.isPlaying && isJumping)
            {
                particleMoveLeft.Stop();
                var particles = new ParticleSystem.Particle[particleMoveLeft.particleCount];
                particleMoveLeft.GetParticles(particles);
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].remainingLifetime = 0f;
                }
                particleMoveLeft.SetParticles(particles);
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
            if (particleMoveRight.isPlaying) particleMoveRight.Stop();
            if (particleMoveLeft.isPlaying) particleMoveLeft.Stop();
        }

        if (rigidBody.velocity.y == 0)
        {
            isJumping = false;
        }

        if (Input.GetAxis("Jump") != 0f && isGrounded)
        {
            Jump();
        }

        if (Input.GetAxis("Jump") != 0f && isGrounded == false && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                jumpTimeCounter -= Time.deltaTime;
                Debug.Log("In the AIR for: " + jumpTimeCounter);
            }
            else
            {
                Debug.Log("String to fall");
                isJumping = false;
                animator.SetBool("HasJumped", false);
            }
        }

        if (Input.GetKeyUp("space"))
        {
            Falling();
        }

    }

    private void Falling()
    {
        Debug.Log("Fallin");
        if (rigidBody.velocity.y < 0)
        {
            Debug.Log(rigidBody.velocity.x + ", " + rigidBody.velocity.y);
        }
        else
        {
            animator.SetBool("HasJumped", false);
        }
    }

    void Jump()
    {
        Debug.Log("Jump");
        animator.SetBool("HasJumped", true);
        rigidBody.velocity += new Vector2(rigidBody.velocity.x, jumpSpeed);
        ParticleSystem ps = Instantiate(particleJump, transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(ps.gameObject, ps.startLifetime);
        isGrounded = false;

        jumpTimeCounter = jumpTime;
        isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform")
        {
            // Check if any of the contact points are unerneath
            foreach (var contact in other.contacts)
            {
                Debug.Log(contact.normal, this);
                if (contact.normal.y > 0.8f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }
}
