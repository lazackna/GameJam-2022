using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private GameObject playerModel;
        [SerializeField] public GameObject hearth0;
        [SerializeField] public GameObject hearth1;
        [SerializeField] public GameObject hearth2;

        private Rigidbody body;
        [SerializeField] private Transform[] groundCheck;

        private Vector2 moveInput;

        [SerializeField] private LayerMask whatIsGround;
        private bool isGrounded;
        private float groundDistance = 0.1f;

        [SerializeField] private float moveSpeed, jumpForce;
        private bool jump = false;
        private bool active = true;
        //public GameObject mesh;
        public int health = 300;

        [SerializeField] private ConsoleHandler consoleHandler;

        float hitTimer = 1;
        bool isHit = false;

        [SerializeField] public Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody>();
            moveInput = new Vector2();
        }

        // Update is called once per frame
        void Update()
        {
            if (isHit)
            {
                hitTimer -= Time.deltaTime;
                //mesh.SetActive(!active);
                active = !active;
                if (hitTimer <= 0)
                {
                    active = true;
                    //mesh.SetActive(active);
                    isHit = false;
                    hitTimer = 2;
                }
            }
            // Read the movement
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            bool foundGround = false;
            foreach (var g in groundCheck)
            {
                //Debug.DrawLine(g.position, g.position + Vector3.down * 1f, Color.red);
                Debug.DrawLine(g.position, g.position + Vector3.down * groundDistance, Color.green);
                if (Physics.CheckSphere(g.position, groundDistance, whatIsGround))
                {
                    isGrounded = true;
                    foundGround = true;
                    animator.SetBool("Jump", false);
                    break;

                }
            }

            if (!foundGround) isGrounded = false;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jump = true;
                animator.SetBool("Jump", true);
            }
            animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));

        }


        private void FixedUpdate()
        {
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            if (consoleHandler.is2d)
            {
                if (moveInput.x < 0)
                {
                    playerModel.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (moveInput.x > 0)
                {
                    playerModel.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                // Player is not allowed to move along the z axis when in 2d mode.
                body.velocity = new Vector3(move.x * moveSpeed, body.velocity.y,
                    0);
            }
            else
            {
                body.velocity = new Vector3(move.x * moveSpeed, body.velocity.y,
                    move.z * moveSpeed);
            }


            if (jump)
            {
                isGrounded = false;
                Jump();

                jump = false;
            }
        }

        void Jump() => body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy" && !isHit)
            {
                if (collision.contacts[0].point.y < this.transform.position.y)
                {
                    
                }
                else
                {
                    health -= collision.gameObject.GetComponent<AbstractEnemy>().getDamage();
                    checkHealth();
                }
            }
        }

        private void checkHealth()
        {
            isHit = true;
            if (health == 0)
            {
                die();
            } else
            {
                if(health < 300)
                {
                    hearth2.SetActive(false);
                }
                if(health < 200)
                {
                    hearth1.SetActive(false);
                }
            }
        }

        private void die()
        {

        }
    }
}
