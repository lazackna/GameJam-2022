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
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = dsCamera.nearClipPlane;
            lastPoint = dsCamera.ScreenToWorldPoint(mousePos);
            lastPoint.z = transform.position.z - 0.01f;
            points.Add(lastPoint);
        }

        private List<Vector3> points = new List<Vector3>();

        private void OnMouseUp()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = dsCamera.nearClipPlane;
            lastPoint = dsCamera.ScreenToWorldPoint(mousePos);
            lastPoint.z = transform.position.z - 0.01f;
            points.Add(lastPoint);

            Debug.Log("Adding line");
            lineController.SetupLine(points.ToArray());
            Vector3[] worldPoints = new Vector3[2];
            for (int i = 0; i < worldPoints.Length; i++)
            {
                Vector3 relative = GetRelativePosition(points[i]);
                Vector3 relativeToPlayer = dsTextureCamera.ViewportToWorldPoint(
                    new Vector3(
                        relative.x,
                        relative.y,
                        -dsTextureCamera.transform.position.z));
                worldPoints[i] = relativeToPlayer;
            }
            
            // Instantiate(cube, relativeToPlayer, Quaternion.identity);
            PlaceBridge(worldPoints);
            points.Clear();
            points.Clear();
        }

        private void Update()
        {
            if (points.Count != 1) return;
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = dsCamera.nearClipPlane;
            mousePos = dsCamera.ScreenToWorldPoint(mousePos);
            mousePos.z = transform.position.z - 0.01f;
            
            lineController.SetupLine(new[] {points[0], mousePos});
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

        private GameObject gameBridge;

        public void PlaceBridge(Vector3[] points)
        {
            Vector3 point1 = points[0];
            Vector3 point2 = points[1];
            point1.z = 0;
            point2.z = 0;

            float deltaX = point2.x - point1.x;
            float deltaY = point2.y - point1.y;

            double angle = Math.Atan2((double)deltaY, (double)deltaX);
            double degree = angle * Mathf.Rad2Deg;

            float dist = Vector3.Distance(point1, point2);
            if (gameBridge != null)
            {
                Destroy(gameBridge);
                gameBridge = null;
            }

            gameBridge = Instantiate(bridge, point1, Quaternion.identity);
            gameBridge.transform.localScale = new Vector3(dist, gameBridge.transform.localScale.y, 5);
            gameBridge.transform.rotation = Quaternion.Euler(0f, 0f, (float)degree);
            gameBridge.transform.Translate(new Vector3(dist / 2, 0, 0));
            gameBridge.layer = LayerMask.NameToLayer("Ground");
            PhysicMaterial material = new PhysicMaterial();
            material.dynamicFriction = 0;
            material.staticFriction = 0;
            material.bounciness = 0;
            material.frictionCombine = PhysicMaterialCombine.Minimum;
            material.bounceCombine = PhysicMaterialCombine.Minimum;
            gameBridge.AddComponent<BoxCollider>();
            gameBridge.GetComponent<BoxCollider>().material = material;
        }

        public Vector3 RelativeToPlayerPosition(Vector3 relative)
        {
            float height = 2 * dsTextureCamera.orthographicSize;
            float width = height * dsTextureCamera.aspect;
            float x = relative.x * width;
            float y = relative.y * height;

            Vector3 playerPos = dsTextureCamera.transform.parent.position;
            Vector3 worldPos = dsTextureCamera.ScreenToWorldPoint(new Vector3(x, y, 0));
            worldPos += playerPos;
            worldPos.z = 0;
            Vector3 test = playerPos + relative * 2;
            test.z = 0;
            //dsTextureCamera.ScreenToWorldPoint()
            Debug.Log("world " + worldPos.x + ", " + worldPos.y);
            return worldPos;
        }

        /**
         * Return value for top-left is 0,0
         * Bottom right 1,1
         */
        public Vector3 GetRelativePosition(Vector3 point)
        {
            // on scale 1 a plane is 10 units long.
            Vector3 centerPoint = transform.position;
            Vector3 diff = point - centerPoint;
            Vector3 relative = diff / (transform.localScale.x * 10);

            relative = new Vector3(relative.x * -1 + 0.5f, (relative.y - 0.5f) * -1, 0);

            Debug.Log(relative.x + ", " + relative.y);
            return relative;
        }
    }
}