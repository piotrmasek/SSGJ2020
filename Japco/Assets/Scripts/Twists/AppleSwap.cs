using UnityEngine;
using Outfrost;

namespace Twists
{
    public class AppleSwap : CheckedMonoBehaviour
    {
        [ExpectAttached] public PlayerMovement playerMovement;
        [ExpectAttached] public PlayerMovement appleMovement;

        Vector2 originalApplePosition;

        bool firstSwapDone = false;
        private void Start()
        {
            originalApplePosition = appleMovement.transform.position;

            //Stop all player particle systems
            foreach (var playerParticleSystem in playerMovement.GetComponentsInChildren<ParticleSystem>())
            {
                playerParticleSystem.enableEmission = false;
            }
        }

        private void Update()
        {

        }

        public void PerformSwap(PlayerMovement movementToBecome, PlayerMovement movementToLeave)
        {
            movementToLeave.transform.position = movementToBecome.transform.position;
            movementToBecome.transform.position = originalApplePosition;

            movementToLeave.enabled = false;
            movementToBecome.enabled = true;

            movementToLeave.GetComponent<Rigidbody2D>().constraints &= RigidbodyConstraints2D.FreezePositionX;
            movementToBecome.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;

            movementToLeave.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            movementToBecome.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            // Moving apple approaches static player
            if (other.gameObject.tag == "Apple" && !firstSwapDone)
            {
                PerformSwap(playerMovement, appleMovement);
                firstSwapDone = true;

                playerMovement.GetComponentInChildren<Animator>().enabled = true;

                //Enable all particle systems
                foreach (var playerParticleSystem in playerMovement.GetComponentsInChildren<ParticleSystem>())
                {
                    playerParticleSystem.enableEmission = true;
                }
            }

            // Moving player approaches static apple
            if (other.gameObject.tag == "Player" && firstSwapDone)
            {
                PerformSwap(appleMovement, playerMovement);
                appleMovement.enabled = false;
                playerMovement.enabled = false;
                SceneLoader.LoadNextScene();
            }
        }
    }
}
