using UnityEngine;

namespace Interactions
{
    public class Japco : MonoBehaviour
    {

        public bool Collided { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Collided = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Collided = false;
            }
        }

    }
}