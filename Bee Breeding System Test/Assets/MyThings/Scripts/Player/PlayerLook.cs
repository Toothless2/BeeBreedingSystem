using UnityEngine;
using System.Collections;

namespace Snake
{
    [RequireComponent(typeof (PlayerMove))]
	public class PlayerLook : MonoBehaviour
    {
        /*Allows player to look around game world. 
        * Sensitivity variable allows controll over howfast the player looks.
        * It also has some looks dempening for a floaty feel what cam be optionaly dissabled.
        * Allows FOV of the camera to be changed.
        * Will clamp the degrees the player can look to 90deg.
        */

        public bool canLook = true;

        //will invert mouse input
        public bool invertMouse;
        
        //overall sensitivity to look around
        public float lookSensitivity = 5f;

        //Y rotation look clampig
        [Range(0.0f, 360.0f)]
        public float lookClamp;

        //x/y rotation a way to store a number so that we know how far the character has rotated
        private float xRot;
        private float yRot;

        //current x/y roatation of the player. will also be used for look dampening
        private float currentXRot;
        private float currentYRot;

        //velcity of rotation
        private float xRotV;
        private float yRotV;

        //optionaly dissables look smoothing
        public bool enableSmoothing;
        //defines how smooth looking is
        public float lookSmoothingX = 0.1f;
        public float lookSmoothingY = 0.1f;

        //allows a FOV range of 0.1deg to 200deg
        [Range(0.1f, 150.0f)]
        public float FOV = 85.0f;

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void CanLook(Vector3 foo)
        {
            canLook = !canLook;
        }

        void Update()
        {
            if(canLook)
            {
                if (Time.timeScale > 0)
                {
                    UpdateLook();
                }
            }
        }

        void UpdateLook()
        {
            if (!invertMouse)
            {
                yRot += Input.GetAxis("Mouse X") * lookSensitivity;
                xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;
            }
            else
            {
                yRot += Input.GetAxis("Mouse X") * lookSensitivity;
                xRot += Input.GetAxis("Mouse Y") * lookSensitivity;
            }

            if (!enableSmoothing)
            {
                lookSmoothingX = 0;
                lookSmoothingY = 0;
            }

            //prevents player from spinnig around contantly;
            xRot = Mathf.Clamp(xRot, -lookClamp, lookClamp);

            //assigns a value to currentXRot variable and applies whatever smoothing is set
            currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotV, lookSmoothingY);
            currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotV, lookSmoothingX);

            //takes the current x/y roations and applies them to the player
            transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
        }
	}
}