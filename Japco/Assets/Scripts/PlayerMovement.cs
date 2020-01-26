using System.Collections;
using System.Collections.Generic;
using Outfrost;
using UnityEngine;

public class PlayerMovement : CheckedMonoBehaviour
{
    public float jumpSpeed = 10f;
    public float jumpTime;
    public bool isJumping = false;
    public float gravity = 2f;
    public float movementSpeed = 5f;
    public bool JumpEnabled = true;

    [ExpectAttached] public Animator animator;
    [ExpectAttached] public ParticleSystem particleJump;
    [ExpectAttached] public ParticleSystem particleMoveRight;
    [ExpectAttached] public ParticleSystem particleMoveLeft;

    private float jumpTimeCounter;

    public Rigidbody2D rigidBody { get; private set; }
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        CheckReferences();
        Time.timeScale = 1.0f;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(horizontal * movementSpeed, rigidBody.velocity.y);


        if (horizontal != 0f)
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

        if (JumpEnabled && Input.GetAxis("Jump") != 0f)
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                    animator.SetBool("HasJumped", false);
                }
            }
        }
        else
        {
            Falling();
        }

    }

    private void Falling()
    {
        if (rigidBody.velocity.y >= 0)
        {
            animator.SetBool("HasJumped", false);
        }
    }

    void Jump()
    {
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
