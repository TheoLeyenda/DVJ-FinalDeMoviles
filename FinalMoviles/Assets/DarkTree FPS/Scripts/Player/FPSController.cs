﻿/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

namespace DarkTreeFPS
{
    public class FPSController : MonoBehaviour
    {
        public GameObject prefabPlayerPC;
        public GameObject prefabPlayerAndroid;
        public Transform posCamara;
        public Image imageButtonRuning;
        public Sprite spriteRunningStickMan;
        public Sprite spriteWalkingStickMan;
        public Construction currentConstruction;
        public GameObject floorObject;
        private string auxFloorObjectTag;
        private GameData gd;

        [Header("Movement Settings")]
        public float moveSpeed = 1f;
        public float crouchSpeed = 0.4f;
        public float runSpeedMultiplier = 2f;
        public float jumpForce = 4f;
        public float height;
        public float crouchHeight = 0.5f;
        private bool crouch = false;
        private bool isRunning = false;

        [Header("MouseLook Settings")]
        private Vector2 clampInDegrees = new Vector2(360, 180);
        public bool lockCursor;
        public Vector2 sensitivity = new Vector2(0.5f, 0.5f);
        public Vector2 smoothing = new Vector2(3, 3);

        [Header("CameraShake Settings")]
        public float durationCameraShake;
        public float magnitudeCameraShake;
        public CameraShake cameraShake;

        [HideInInspector]
        public Vector2 targetDirection;

        [HideInInspector]
        public Rigidbody controllerRigidbody;

        private CapsuleCollider controllerCollider;
        public Transform camHolder;
        private float moveSpeedLocal;

        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;

        private float distanceToGround;

        private Animator weaponHolderAnimator;

        public bool isClimbing = false;

        private float inAirTime;

        [HideInInspector]
        public bool mouseLookEnabled = true;

        //Velocity calculation variable
        private Vector3 previousPos = new Vector3();

        Vector3 dirVector;

        InputManager inputManager;
        private void Awake()
        {
#if UNITY_ANDROID
            prefabPlayerPC.SetActive(false);
#else
            prefabPlayerAndroid.SetActive(false);
#endif
            auxFloorObjectTag = floorObject.tag;
            floorObject.tag = "Untagged";
        }
        private void Start()
        {
            gd = GameData.instaceGameData;
            controllerRigidbody = GetComponent<Rigidbody>();
            controllerCollider = GetComponent<CapsuleCollider>();

            distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
            targetDirection = camHolder.transform.forward;
            weaponHolderAnimator = GameObject.Find("Weapon holder").GetComponent<Animator>();

            inputManager = FindObjectOfType<InputManager>();
            crouch = false;
            sensitivity = gd.sensivility;

            lockCursor = true;

        }

        private void Update()
        {

            if (mouseLookEnabled && !InventoryManager.showInventory)
                MouseLook();

            StandaloneMovement();

#if UNITY_STANDALONE
            if (lockCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
#endif
#if UNITY_ANDROID
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
#endif
            Crouch();
            Landing();
        }
        public void SetIsRunning()
        {
            isRunning = !isRunning;
#if UNITY_ANDROID
            if (isRunning)
            {
                imageButtonRuning.sprite = spriteWalkingStickMan;
            }
            else
            {
                imageButtonRuning.sprite = spriteRunningStickMan;
            }
#endif
        }
        void StandaloneMovement()
        {
            if (isGrounded())
            {
                if (CheckMovement())
                {
                    weaponHolderAnimator.SetBool("Walk", true);
                    moveSpeedLocal = moveSpeed;
                }
                else
                    weaponHolderAnimator.SetBool("Walk", false);

                if ((Input.GetKey(inputManager.Run) || isRunning) && !isClimbing && !crouch && weaponHolderAnimator.GetBool("Walk") == true)
                {
                    moveSpeedLocal = runSpeedMultiplier * moveSpeed;
                    weaponHolderAnimator.SetBool("Run", true);
                }
                else
                    weaponHolderAnimator.SetBool("Run", false);
            }
            else
            {
                weaponHolderAnimator.SetBool("Walk", false);
                weaponHolderAnimator.SetBool("Run", false);
            }

            /*if (crouch)
            {
                moveSpeedLocal = crouchSpeed;
                weaponHolderAnimator.SetBool("Walk", false);
                weaponHolderAnimator.SetBool("Run", false);
                if (CheckMovement())
                {
                    weaponHolderAnimator.SetBool("Crouch", true);
                }
                else
                    weaponHolderAnimator.SetBool("Crouch", false);
            }
            else
                weaponHolderAnimator.SetBool("Crouch", false);*/

            /*if (Input.GetKeyDown(inputManager.Crouch))
            {
                crouch = !crouch;
            }*/
            if (Input.GetKeyDown(inputManager.Jump))
            {
                Jump();
                crouch = false;
            }
        }

        public void MobileMovement()
        {
            if (isGrounded())
            {
                if (CheckMovement())
                {
                    weaponHolderAnimator.SetBool("Walk", true);
                    moveSpeedLocal = moveSpeed;
                }
                else
                    weaponHolderAnimator.SetBool("Walk", false);

                if (InputManager.joystickInputVector.y > 0.5f && !isClimbing && !crouch && weaponHolderAnimator.GetBool("Walk") == true)
                {
                    moveSpeedLocal = runSpeedMultiplier * moveSpeed;
                    weaponHolderAnimator.SetBool("Run", true);
                }
                else
                    weaponHolderAnimator.SetBool("Run", false);
            }
            else
            {
                weaponHolderAnimator.SetBool("Walk", false);
                weaponHolderAnimator.SetBool("Run", false);
            }

            /*if (crouch)
            {
                moveSpeedLocal = crouchSpeed;
                weaponHolderAnimator.SetBool("Walk", false);
                weaponHolderAnimator.SetBool("Run", false);
                if (CheckMovement())
                {
                    weaponHolderAnimator.SetBool("Crouch", true);
                }
                else
                    weaponHolderAnimator.SetBool("Crouch", false);
            }
            else
                weaponHolderAnimator.SetBool("Crouch", false);*/

            /*if (Input.GetKeyDown(inputManager.Crouch))
            {
                crouch = !crouch;
            }*/
            if (Input.GetKeyDown(inputManager.Jump))
            {
                Jump();
                crouch = false;
            }

        }

        void FixedUpdate()
        {
            CharacterMovement();
        }

        void CharacterMovement()
        {
            var camForward = camHolder.transform.forward;
            var camRight = camHolder.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            if (isClimbing)
            {
                crouch = false;

                weaponHolderAnimator.SetBool("HideWeapon", true);
                controllerRigidbody.useGravity = false;

                dirVector = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical")).normalized;


                Vector3 verticalDirection = transform.up;
                Vector3 moveDirection = (verticalDirection) * dirVector.z + camRight * dirVector.x;

                controllerRigidbody.MovePosition(transform.position + moveDirection * moveSpeedLocal * Time.deltaTime);
            }
            else
            {
                weaponHolderAnimator.SetBool("HideWeapon", false);
                controllerRigidbody.useGravity = true;

                dirVector = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical")).normalized;

                Vector3 moveDirection = camForward * dirVector.z + camRight * dirVector.x;

                controllerRigidbody.MovePosition(transform.position + moveDirection * moveSpeedLocal * Time.deltaTime);
            }
        }

        bool CheckMovement()
        {
            if (CrossPlatformInputManager.GetAxis("Vertical") > 0 || CrossPlatformInputManager.GetAxis("Vertical") < 0 || CrossPlatformInputManager.GetAxis("Horizontal") > 0 || CrossPlatformInputManager.GetAxis("Horizontal") < 0)
            {
                return true;
            }


            return false;
        }

        void MouseLook()
        {
            Quaternion targetOrientation = Quaternion.Euler(targetDirection);

            Vector2 mouseDelta = new Vector2();


            mouseDelta = new Vector2(CrossPlatformInputManager.GetAxisRaw("Mouse X"), CrossPlatformInputManager.GetAxisRaw("Mouse Y"));
            //mouseDelta = new Vector2(CrossPlatformInputManager.GetAxis("Mouse X"), CrossPlatformInputManager.GetAxis("Mouse Y"));
            //mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            // Debug.Log(Input.GetAxis("Mouse X") + "," + Input.GetAxis("Mouse X"));

            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
                
                _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
                _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

                _mouseAbsolute += _smoothMouse;

                if (clampInDegrees.x < 360)
                    _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

                var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
            camHolder.transform.localRotation = xRotation;

                if (clampInDegrees.y < 360)
                    _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

                var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, camHolder.transform.InverseTransformDirection(Vector3.up));
            camHolder.transform.localRotation *= yRotation;
            camHolder.transform.rotation *= targetOrientation;
        }
        
        void Crouch()
        {
            if (crouch == true)
            {
                controllerCollider.height = crouchHeight;
                camHolder.transform.localPosition = new Vector3(0, posCamara.localPosition.y/2, 0);
            }
            else
            {
                //Ray ray = new Ray();
                //RaycastHit hit;
                //ray.origin = transform.position;
                //ray.direction = transform.up;
                //if (!Physics.Raycast(ray, out hit, 1))
                //{
                    camHolder.transform.localPosition = new Vector3(0, posCamara.localPosition.y,0); 
                    controllerCollider.height = height;
                    crouch = false;
                //}
                //else
                    //crouch = true;
            }
        }
        
        public float GetVelocityMagnitude()
        {
            var velocity = ((transform.position - previousPos).magnitude) / Time.deltaTime;
            previousPos = transform.position;
            return velocity;
        }

        public void Jump()
        {
            if (isGrounded() && !crouch)
                controllerRigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }

        public void CrouchMobile()
        {
            crouch = !crouch;
        }

        bool isGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
        }

        void Landing()
        {
            if(!isGrounded())
            {
                inAirTime += Time.deltaTime;
            }
            else
            {
                if (inAirTime > 0.5f)
                    weaponHolderAnimator.Play("Landing");

                inAirTime = 0;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Inside" || other.tag == "MeleTarget")
            {
                floorObject.tag = auxFloorObjectTag;
                currentConstruction = other.GetComponent<Construction>();
            }
            if (other.tag == "Piso" && currentConstruction != null)
            {
                transform.position = currentConstruction.teleportPosition.transform.position;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Piso" && currentConstruction != null)
            {
                transform.position = currentConstruction.teleportPosition.transform.position;
            }
        }
    }
}