using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consoles
{
    public class DS : MonoBehaviour
    {

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera dsCamera;

        private Vector3 screenPoint;
        private Vector3 offset;
        private Vector3 lastPoint;

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

     
    }
}
