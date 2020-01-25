using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Interactions;
using Outfrost;
using TMPro;
using UnityEngine.Assertions;

namespace Twists
{
    public class ExtrudeJump : CheckedMonoBehaviour
    {
        [ExpectAttached] public PlayerMovement Movement;
        public float ErectionSpeed = 1f;

        private void Start()
        {
            CheckReferences();
            Movement.JumpEnabled = false;
            Physics2D.gravity = Vector2.zero;
        }

        private void Update()
        {
            if (Movement == null)
            {
                return;
            }

            if (Input.GetAxis("Jump") != 0f)
            {
                Movement.rigidBody.velocity = Movement.rigidBody.velocity + new Vector2(0f, ErectionSpeed);
            }
            else
            {
                Movement.rigidBody.velocity = new Vector2(Movement.rigidBody.velocity.x, 0f);
            }
        }
    }
}
