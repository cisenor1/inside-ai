using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using insideai.gamemanagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace insideai.camera
{
    public class CameraPanControls : MonoBehaviour
    {

        public float movementSpeed = 10f;
        public InputManager _input;
        public LayerMask ground;
        public Texture debugTex;
        private float _defaultCameraHeight;
        void Start()
        {
            _defaultCameraHeight = transform.position.y;
        }
        // Update is called once per frame
        void Update()
        {
            DebugDrawColoredRectangle(InputManager.instance.clickSection, Color.red);
            float xDirection = InputManager.instance.xDirection;
            float zDirection = InputManager.instance.zDirection;
            RaycastHit hit;
            float yDistance = 0f;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 600, ground))
            {
                yDistance = _defaultCameraHeight - hit.distance;
            }
            transform.Translate(xDirection * movementSpeed * Time.deltaTime, yDistance * Time.deltaTime * movementSpeed * 2, zDirection * movementSpeed * Time.deltaTime, Space.World);
        }

        void DebugDrawColoredRectangle(Rect rect, Color color)
        {
            Debug.DrawLine(rect.min, new Vector3(rect.xMax, rect.yMin, 0), color);
            Debug.DrawLine(rect.min, new Vector3(rect.xMin, rect.yMax, 0), color);
            Debug.DrawLine(rect.max, new Vector3(rect.xMin, rect.yMax, 0), color);
            Debug.DrawLine(rect.max, new Vector3(rect.xMax, rect.yMin, 0), color);
        }

        void OnGUI()
        {
            Rect r = InputManager.instance.clickSection;
            if (r == Rect.zero){
                return;
            }
            DebugDrawColoredRectangle(InputManager.instance.clickSection, Color.red);
        }
    }
}