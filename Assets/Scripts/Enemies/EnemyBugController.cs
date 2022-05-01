using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBugController : AbstractEnemy
{

    //Needs to change
    [SerializeField] GameObject player;
    public int speed = 2;
    private Rigidbody body;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.health = 100;
        this.damage = 100;
        body = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        walkTowardsPlayer();
    }

    private void walkTowardsPlayer()
    {
        Vector3 velocity = new Vector3(0, this.body.velocity.y, this.body.velocity.z);
        if(this.transform.position.x < player.transform.position.x + 2.5)
        {
            velocity.x = speed;
            animator.SetFloat("Speed", speed);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(this.transform.position.x > player.transform.position.x - 2.5)
        {
            velocity.x = -speed;
            animator.SetFloat("Speed", speed);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else
        {
            animator.SetFloat("Speed", 0);
        }
        this.body.velocity = velocity;
        float distanceToPlayer = Mathf.Abs(this.transform.position.x - player.transform.position.x);
        if(distanceToPlayer <= 2.5)
        {
            animator.SetBool("Attacking", true);
        } else
        {
            animator.SetBool("Attacking", false);
        }

    }

    public override void takeDamage(int damage)
    {

    }

    public override void die()
    {
        
    }
}
