using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBugController : AbstractEnemy
{

    //Needs to change
    GameObject player;
    public int speed = 2;
    private Rigidbody body;
    private Animator animator;
    ConsoleHandler consoleHandler;
    bool isAttacking = false;
    bool dead = false;
    bool grounded = false;
    bool inProx = false;

    // Start is called before the first frame update
    void Start()
    {
        this.health = 100;
        this.damage = 100;
        body = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        player = FindObjectOfType<PlayerMotor>().gameObject;
        consoleHandler = FindObjectOfType<ConsoleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        if(distanceToPlayer <= 12)
        {
            inProx = true;
        } else
        {
            inProx = false;
        }

        if(!dead) walkTowardsPlayer();
    }

    private void walkTowardsPlayer()
    {
        Vector3 velocity = new Vector3(0, this.body.velocity.y, this.body.velocity.z);
        if (!isAttacking && inProx)
        {
            if (!consoleHandler.is2d)
            {
                if (this.transform.position.z < player.transform.position.z)
                {
                    if (this.transform.position.x < player.transform.position.x)
                    {
                        velocity.x = speed;
                        animator.SetFloat("Speed", speed);
                    }
                    else if (this.transform.position.x > player.transform.position.x)
                    {
                        velocity.x = -speed;
                        animator.SetFloat("Speed", speed);
                    }
                    velocity.z = speed;
                    animator.SetFloat("Speed", speed);
                    float rotation = Mathf.Rad2Deg * Mathf.Atan2(player.transform.position.z - this.transform.position.z, player.transform.position.x - this.transform.position.x);
                    this.transform.rotation = Quaternion.Euler(0, -rotation, 0);
                }
                else if (this.transform.position.z > player.transform.position.z)
                {
                    if (this.transform.position.x < player.transform.position.x)
                    {
                        velocity.x = speed;
                        animator.SetFloat("Speed", speed);
                    }
                    else if (this.transform.position.x > player.transform.position.x)
                    {
                        velocity.x = -speed;
                        animator.SetFloat("Speed", speed);
                    }
                    velocity.z = -speed;
                    animator.SetFloat("Speed", speed);
                    float rotation = Mathf.Rad2Deg * Mathf.Atan2(player.transform.position.z - this.transform.position.z, player.transform.position.x - this.transform.position.x);
                    this.transform.rotation = Quaternion.Euler(0, -rotation, 0);
                }
                else
                {
                    animator.SetFloat("Speed", 0);
                }
            }
            else
            {
                if (this.transform.position.z != 0)
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                }
                if (this.transform.position.x < player.transform.position.x + 1.5)
                {
                    velocity.x = speed;
                    animator.SetFloat("Speed", speed);
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (this.transform.position.x > player.transform.position.x - 1.5)
                {
                    velocity.x = -speed;
                    animator.SetFloat("Speed", speed);
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    animator.SetFloat("Speed", 0);
                }
            }
        }
        this.body.velocity = velocity;

    }

    public override void takeDamage(int damage)
    {

    }

    public override IEnumerator die()
    {
        animator.Play("DeathAnimation");
        dead = true;
        this.body.velocity = new Vector3(0, 0, 0);
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(player))
        {
            
            if (collision.contacts[0].point.y > this.transform.position.y && collision.contacts[0].point.x > this.transform.position.x - 2.2 && collision.contacts[0].point.x < this.transform.position.x + 2.2)
            {
                StartCoroutine(die());
                
            }
            else
            {
                animator.SetBool("Attacking", true);
                isAttacking = true;
            }
        } else if(collision.gameObject.layer == 6)
        {
            Debug.Log(string.Format("Ground: {0} / Enemy: {1}", collision.contacts[0].point, this.transform.position));
            if(collision.contacts[0].point.y < this.transform.position.y)
            {
                grounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6 && grounded)
        {
            if (collision.contacts[0].point.y > this.transform.position.y)
                this.body.velocity = new Vector3(this.body.velocity.x + 3, 5, this.body.velocity.y);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.Equals(player))
        {
            animator.SetBool("Attacking", false);
            isAttacking = false;
        } else if(collision.gameObject.layer == 6)
        {
            grounded = false;
        }
    }
}
