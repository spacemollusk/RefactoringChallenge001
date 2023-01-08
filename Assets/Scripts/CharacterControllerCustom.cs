using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LongMethod
{
    public class CharacterControllerCustom : MonoBehaviour
    {
        /// <summary>
        /// Controls player movement (default to keyboard) and camera rotation using the mouse
        /// </summary>
        
        [Header("Camera Control")]
        [SerializeField]
        private float upperLimit = 10;
        [SerializeField]
        private float lowerLimit = 35;
        [SerializeField]
        private float cameraSpeed = 45;


        [Header("Character Movement")]
        [SerializeField]
        private float movementSpeed = 7;
        [SerializeField]
        private float jumpPower  = 10;

        private Rigidbody characterRb;

        void Awake()
        {
            // keeps cursor locked in game window
            Cursor.lockState = CursorLockMode.Locked;

            characterRb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            CharacterMovement();
            CameraRotation();
        }

        private void CameraRotation()
        {
            // camera view rotation on Y axis
            transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (cameraSpeed * Input.GetAxis("Mouse X") * Time.deltaTime), 0);

            // camera view rotation on X Axis
            var cameraTransform = transform.Find("Main Camera").transform;
            var xAxisRotation = cameraTransform.localRotation.eulerAngles.x - (cameraSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);

                if (xAxisRotation > lowerLimit)
                {
                    xAxisRotation = lowerLimit;
                }
                else if (xAxisRotation < upperLimit)
                {
                    xAxisRotation = upperLimit;
                }

            cameraTransform.localRotation = Quaternion.Euler(xAxisRotation, 0, 0);
        }

        void CharacterMovement()
        {
            var movement = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward).normalized;
            transform.position += (movement * Time.deltaTime * movementSpeed);

                if (Input.GetButtonDown("Jump"))
                {
                    characterRb.velocity = Vector3.up * jumpPower;

                }
        }
    }
}
