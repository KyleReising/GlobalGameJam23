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
    public Placeable myPlaceableTarget = null;

    // Director stats
    public float costInRage = 10;
    public List<Organizer.Tag> myPreferenceTags;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyRigidbody = GetComponent<Rigidbody2D>();
        //this.myPreferenceTags = new List<Organizer.Tag>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine a target if we don't have one
        this.updateTarget();

        // See if we are close enough to the target to attack --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- 
        this.updateAttack();
    }

    // Called a fixed number of times per second
    private void FixedUpdate()
    {
        this.updateMovement();
    }

    public virtual void updateTarget()
    {
        if (myPlaceableTarget != null)
        {
            // we do not need to continue if we already have a placeable
            return;
        }

        GameManager gm = FindObjectOfType<GameManager>();

        // Get all our placeables
        List<Placeable> genericlist = gm.getFarmPlaceable();
        List<Placeable> preferedlist = gm.getFarmPlaceableByTags(myPreferenceTags);


        // If we do not have placeables, drop out
        if (genericlist.Count == 0)
        {
            return;
        }
        
        // Check if we have 
        if (preferedlist.Count != 0)
        {
            // What is the closest prefered placeable
            Placeable closestp = null;
            foreach (Placeable p in preferedlist)
            {
                // if this is our first p, immediatly save it away
                if (closestp == null)
                {
                    closestp = p;
                    continue;
                }
                // Calculate distance to closest p and p
                float distp = Vector2.Distance(p.gameObject.transform.position, this.gameObject.transform.position);
                float distclosep = Vector2.Distance(closestp.gameObject.transform.position, this.gameObject.transform.position);


                // Check to see if p is closer than closestp
                if (distclosep > distp)
                {
                    closestp = p;

                }
            }
            myPlaceableTarget = closestp;
            targetLocation = closestp.transform.position;
            return;
        }
        else
        {
            // What is the closest non prefered placeable
            Placeable closestp = null;
            foreach (Placeable p in genericlist)
            {
                // if this is our first p, immediatly save it away
                if (closestp == null)
                {
                    closestp = p;
                    continue;
                }
                // Calculate distance to closest p and p
                float distp = Vector2.Distance(p.gameObject.transform.position, this.gameObject.transform.position);
                float distclosep = Vector2.Distance(closestp.gameObject.transform.position, this.gameObject.transform.position);


                // Check to see if p is closer than closestp
                if (distclosep > distp)
                {
                    closestp = p;

                }
            }
            myPlaceableTarget = closestp;
            targetLocation = closestp.transform.position;
            return;
        }
    }

    public virtual void updateAttack()
    {
        if (curAttackCooldown <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (myPlaceableTarget == null)
        {
            curAttackWindup = baseAttackWindup;     // Reset the windup
            curAttackCooldown -= Time.deltaTime;    // Tick down the attack cooldown
            return;                                 // We don't have a target
        }

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
                if (myPlaceableTarget.takeDamage(damage))
                {
                    myPlaceableTarget = null;
                }
                
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

    public virtual void updateMovement()
    {
        if (myPlaceableTarget == null)
        { 
            this.enemyRigidbody.velocity = new Vector2(0, 0);
            return;
        }

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
            this.enemyRigidbody.velocity = new Vector2(0, 0);
        }
    }

    public virtual void takeDamage(float damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            FindObjectOfType<EnemyDirector>().destroyEnemyFromList(this);
            Destroy(this.gameObject);
        }
    }
}
