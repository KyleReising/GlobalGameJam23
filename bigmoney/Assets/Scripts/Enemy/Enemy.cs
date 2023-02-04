using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Base stats
    public float movementSpeed;
    public float health;
    public float damage;

    // Attack stats
    public float curAttackWindup, baseAttackWindup;     // How long the enemy needs to remain near to it's target to attack 
    public float curAttackCooldown, baseAttackCooldown; // How long the enemy needs to wait to do anything after it has attacked
    public float attackRange;                           // How close the enemy needs to be to attack it's target

    // Movement stats
    public Vector2 targetLocation;
    public Rigidbody2D enemyRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine a target if we don't have one

        // See if we are close enough to the target to attack --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- 
        if (Vector2.Distance((Vector2)transform.position, targetLocation) > attackRange)
        {
            curAttackWindup = baseAttackWindup; // Reset the windup
            curAttackCooldown -= Time.deltaTime; // Tick down the attack cooldown
            return; // We are too far from our target to attack, so we don't need to do anything more here
        }

        // We are close enough to attack
        // Check to see if the cooldown is less than or equal to zero
        if (curAttackCooldown <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            // We are close enough and not on cooldown
            // Check to see if  wind up is less than or equal to zero
            if (curAttackWindup <= 0)
            {
                // Attack goes here
                GetComponent<SpriteRenderer>().color = Color.red; // Setting color to red while we are cooling down, once cooled down the enemy will at least turn back to white
                curAttackCooldown = baseAttackCooldown;
                curAttackWindup = baseAttackWindup;
            }
            else
            {
                // Tick down the wind up
                curAttackWindup -= Time.deltaTime;
                GetComponent<SpriteRenderer>().color = Color.yellow;

            }
        }
        else
        {
            // Tick down cooldown
            curAttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Move towards target, don't if we are close enough to attack
        // Also don't move if we are on attack cooldown
        if (Vector2.Distance((Vector2)transform.position, targetLocation) > attackRange && curAttackCooldown <= 0)
        {
            Vector2 direction = (targetLocation - (Vector2)transform.position).normalized;
            Vector2 force = direction * movementSpeed * Time.fixedDeltaTime;
            this.enemyRigidbody.velocity = force;
        }
        else
        {
            // Stop the enemies movement since we are now close to its target
            this.enemyRigidbody.velocity = new Vector2(0,0);
        }
    }


    public void takeDamage(float damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
