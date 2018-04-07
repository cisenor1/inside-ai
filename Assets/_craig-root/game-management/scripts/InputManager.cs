using UnityEngine;
using System.Collections;

namespace insideai.gamemanagement
{
    public class InputManager : MonoBehaviour
    {
        public KeyCode cameraForward = KeyCode.W;
        public KeyCode cameraBackward = KeyCode.S;
        public KeyCode cameraLeft = KeyCode.A;
        public KeyCode cameraRight = KeyCode.D;
        public LayerMask selectable;
        public Rect clickSection {
            get {
                return new Rect(mouseP1, mouseP2 - mouseP1);
            }   
        }

        [HideInInspector]
        public float zDirection = 0.0f;
        [HideInInspector]
        public float xDirection = 0.0f;

        public static InputManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("inputManager").AddComponent<InputManager>();
                }
                return _instance.GetComponent<InputManager>();
            }
        }

        private static InputManager _instance;
        private bool _dragging = false;
        private Vector3 mouseP1 = Vector3.zero;
        private Vector3 mouseP2 = Vector3.zero;

        public void Update()
        {
            _CheckCameraInputs();
            _CheckClicks();
        }

        private

        void _CheckClicks()
        {
            if (Input.GetMouseButtonDown(0) && !_dragging)
            {
                mouseP1 = Input.mousePosition;
                _dragging = true;
            }

            if (Input.GetMouseButton(0) && _dragging){
                mouseP2 =  Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0) && _dragging)
            {
                _dragging = false;
                mouseP2 = Input.mousePosition;
            }
        }

        void _CheckCameraInputs()
        {
            float up = Input.GetKey(cameraForward) ? 1.0f : 0.0f;
            float down = Input.GetKey(cameraBackward) ? -1.0f : 0.0f;
            float left = Input.GetKey(cameraLeft) ? -1.0f : 0.0f;
            float right = Input.GetKey(cameraRight) ? 1.0f : 0.0f;
            zDirection = up + down;
            xDirection = right + left;
        }
    }
}