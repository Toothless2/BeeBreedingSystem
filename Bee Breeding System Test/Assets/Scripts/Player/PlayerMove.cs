using UnityEngine;
using System.Collections;

namespace Snake
{
    [RequireComponent(typeof (CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        /*
        * Moves the Player in the direction it is looking.
        * Speed can be changed eith by player input or in inspector
        */

        public bool canMove = true;

        //move speed is clamped
        [Range(0.0f, 10.0f)]
        public float moveSpeed;

        private Vector3 moveDirection;

        private PlayerSpeed speed;

        void Update()
        {
            if(canMove)
            {
                if (Time.timeScale > 0)
                {
                    MovePlayer();
                }
            }
        }

        void MovePlayer()
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 4 * Time.deltaTime;
            }
            else
            {
                moveSpeed = 2 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<CharacterController>().Move(new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed);
            }

            if(Input.GetKey(KeyCode.A))
            {
                GetComponent<CharacterController>().Move((new Vector3(transform.right.x, 0, transform.right.z) * -1) * moveSpeed);
            }

            if(Input.GetKey(KeyCode.D))
            {
                GetComponent<CharacterController>().Move(new Vector3(transform.right.x, 0, transform.right.z) * moveSpeed);
            }

            if(Input.GetKey(KeyCode.S))
            {
                GetComponent<CharacterController>().Move((new Vector3(transform.forward.x, 0, transform.forward.z) * -1) * moveSpeed);
            }
        }

        void CanMove(Vector3 foo)
        {
            canMove = false;
        }
    }
}