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
        private void OnMouseDown()
        {
            screenPoint = dsCamera.WorldToScreenPoint(gameObject.transform.position);
            
            offset = gameObject.transform.position - dsCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, screenPoint.z));
            lastPoint = offset;
        }
        
        void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
            Vector3 curPosition = dsCamera.ScreenToWorldPoint(curScreenPoint) + offset;
            Debug.DrawLine(lastPoint, curPosition, Color.red);
            //transform.position = curPosition;
            Debug.Log("Mouse Drag");
        }

     
    }
}
