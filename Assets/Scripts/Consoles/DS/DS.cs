using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Consoles
{
    public class DS : MonoBehaviour
    {

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera dsCamera;
        [SerializeField] private Camera dsTextureCamera;
        [SerializeField] private GameObject cube;

        private Vector3 screenPoint;
        private Vector3 offset;
        private Vector3 lastPoint;

        public GameObject bridge;

        [SerializeField] private LineController lineController;

        private void OnMouseDown()
        {
            // screenPoint = dsCamera.WorldToScreenPoint(gameObject.transform.position);
            //
            // offset = gameObject.transform.position - dsCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            //     Input.mousePosition.y, screenPoint.z));
            // //lastPoint = offset;
            // Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //
            // Vector3 curPosition = dsCamera.ScreenToWorldPoint(curScreenPoint) + offset;
            // Debug.DrawLine(lastPoint, curPosition, Color.red);
            // Vector3 drawPosition = new Vector3(curPosition.x, curPosition.y, transform.position.z - 1);
            // lastPoint = curPosition;
            // //transform.position = curPosition;
            // Debug.Log("Mouse down");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = dsCamera.nearClipPlane;
            lastPoint = dsCamera.ScreenToWorldPoint(mousePos);
            lastPoint.z = transform.position.z - 0.01f;

        }

        private List<Vector3> points = new List<Vector3>();
        private void OnMouseUp()
        {
            points.Add(lastPoint);
            if (points.Count >= 2)
            {
                Debug.Log("Adding line");
                lineController.SetupLine(points.ToArray());
                Vector3 relative = GetRelativePosition(lastPoint);
                Vector3 relativeToPlayer = RelativeToPlayerPosition(relative);
                Instantiate(cube, relativeToPlayer, Quaternion.identity);
                
                points.Clear();
            }
        }

        void OnMouseDrag()
        {
            // Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //
            // Vector3 curPosition = dsCamera.ScreenToWorldPoint(curScreenPoint) + offset;
            // Debug.DrawLine(lastPoint, curPosition, Color.red);
            // Vector3 drawPosition = new Vector3(curPosition.x, curPosition.y, transform.position.z - 1);
            // lastPoint = drawPosition;
            // //transform.position = curPosition;
            // Debug.Log("Mouse Drag");
        }

        public void PlaceBridge(Vector3[] points)
        {
            Vector3 point1 = points[0];
            Vector3 point2 = points[1];
            
            
            
        }
        
        public Vector3 RelativeToPlayerPosition(Vector3 relative)
        {

            float height = 2 * dsTextureCamera.orthographicSize;
            float width = height * dsTextureCamera.aspect;
            float x = relative.x * width;
            float y = relative.y * height;
           // x *= 2;
           // y *= 2;
            Vector3 playerPos = dsTextureCamera.transform.parent.position;
            Vector3 worldPos = dsTextureCamera.ScreenToWorldPoint(new Vector3(x, y, 0));
            worldPos += playerPos;
            worldPos.z = 0;
            Vector3 test = playerPos + relative * 2;
            test.z = 0;
            //dsTextureCamera.ScreenToWorldPoint()
            return test;
        }
        
        public Vector3 GetRelativePosition(Vector3 point)
        {
            // on scale 1 a plane is 10 units long.
            Vector3 centerPoint = transform.position;
            float width = transform.localScale.x * 10;
            float heigth = transform.localScale.z * 10;
            Vector3 diff = point - centerPoint;
            Vector3 relative = diff / transform.localScale.x;
            // relative.x += 5;
            // relative.y -= 5;
           // relative.y 
           // float height = 
           return relative;
        }
     
    }
}
