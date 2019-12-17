using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkTreeFPS
{

    public class Lean : MonoBehaviour
    {
        private bool left;
        private bool right;
        InputManager inputManager;

        [Header("Lean Settings")]
        public float leanRotationSpeed = 80f;
        public float leanPositionSpeed = 3f;
        public float maxAngle = 30f;
        public float leanPositionShift = 0.1f;

        private float leanCurrentAngle = 0f;
        private float leanCurrentPosition;
        Quaternion leanRotation;

        Vector3 velocity = Vector3.zero;

        public float checkCollisionDistance = 0.1f;

        private void Start()
        {
            inputManager = FindObjectOfType<InputManager>();
        }
        public void LeanLeft()
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, -transform.right, out hit, checkCollisionDistance))
            {
                Debug.Log("TREMENDO FASO");
                var temp_leanPositionShift = leanPositionShift;
                leanCurrentAngle = Mathf.MoveTowardsAngle(leanCurrentAngle, maxAngle, leanRotationSpeed * Time.smoothDeltaTime);
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(-temp_leanPositionShift, 0, 0), ref velocity, leanPositionSpeed * Time.smoothDeltaTime);
            }
            else
            {
                var temp_leanPositionShift = Vector3.Distance(transform.position, hit.point) / 1.5f;
                leanCurrentAngle = Mathf.MoveTowardsAngle(leanCurrentAngle, maxAngle / 3, leanRotationSpeed * Time.smoothDeltaTime);
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(-temp_leanPositionShift, 0, 0), ref velocity, leanPositionSpeed * Time.smoothDeltaTime);
            }
        }
        public void LeanRight()
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, transform.right, out hit, checkCollisionDistance))
            {
                var temp_leanPositionShift = leanPositionShift;
                leanCurrentAngle = Mathf.MoveTowardsAngle(leanCurrentAngle, -maxAngle, leanRotationSpeed * Time.smoothDeltaTime);
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(temp_leanPositionShift, 0, 0), ref velocity, leanPositionSpeed * Time.smoothDeltaTime);
            }
            else
            {
                var temp_leanPositionShift = Vector3.Distance(transform.position, hit.point) / 1.5f;
                leanCurrentAngle = Mathf.MoveTowardsAngle(leanCurrentAngle, -maxAngle / 3, leanRotationSpeed * Time.smoothDeltaTime);
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(temp_leanPositionShift, 0, 0), ref velocity, leanPositionSpeed * Time.smoothDeltaTime);
            }

        }
        public void LeanCenter()
        {
            leanCurrentAngle = Mathf.MoveTowardsAngle(leanCurrentAngle, 0f, leanRotationSpeed * Time.deltaTime);
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref velocity, leanPositionSpeed * Time.smoothDeltaTime);
            left = false;
            right = false;
        }
        public void SetLeft(bool _left)
        {
            left = _left;

        }
        public void SetRight(bool _right)
        {
            right = _right;
        }
        void Update()
        {
#if !UNITY_ANDROID
            if (Input.GetKey(inputManager.LeanLeft))
            {
                LeanLeft();
            }
            else if (Input.GetKey(inputManager.LeanRight))
            {
                LeanRight();
            }
            else
            {
                LeanCenter();
            }

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, leanCurrentAngle));
#else 
            if (left)
            {
                LeanLeft();
            }
            else if (right && !left)
            {
                LeanRight();
            }
            else
            {
                LeanCenter();
            }
#endif 
        }
    }
}
