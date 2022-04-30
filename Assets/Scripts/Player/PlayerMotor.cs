using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        
        private Rigidbody body;
        [SerializeField] private Transform[] groundCheck;
        
        private Vector2 moveInput;
        
        [SerializeField] private LayerMask whatIsGround;
        private bool isGrounded;
        private float groundDistance = 0.1f;

        [SerializeField] private float moveSpeed, jumpForce;
        private bool jump = false;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody>();
            moveInput = new Vector2();
        }

        // Update is called once per frame
        void Update()
        {
           
            // Read the movement
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            
            RaycastHit hit;
            bool foundGround = false;
            foreach (var g in groundCheck)
            {
                //Debug.DrawLine(g.position, g.position + Vector3.down * 1f, Color.red);
                Debug.DrawLine(g.position, g.position + Vector3.down * groundDistance, Color.green);
                if(Physics.CheckSphere(g.position, groundDistance, whatIsGround)) {
                    isGrounded = true;
                    foundGround = true;
                    break;
                }
            }

            if (!foundGround) isGrounded = false;

            if (Input.GetButtonDown("Jump") && isGrounded) jump = true;
        }
        
        private void FixedUpdate()
        {
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            body.velocity = new Vector3(move.x * moveSpeed, body.velocity.y,
                move.y * moveSpeed);
            
            if (jump) 
            {   
                isGrounded = false;
                Jump();
                
                jump = false;
            }
        }

        void Jump() => body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
    }
}
