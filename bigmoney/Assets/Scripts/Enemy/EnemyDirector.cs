using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    // List of enemies we can spawn :-)
    public List<Enemy> myArmy;

    // Spawning stats
    public float spawnDistance = 20f;

    // Rage stats
    public float targetRage;    // Once the rageMeter hits the targetRage, the director will spawn a horde
    public float rageMeter;     // A 'currency' the director will use to spawn enemies
    public float ragePerSecond = 1; // How much rage the director passively gains
    public float derivative = 0.05f;

    // Passively spawning stats
    public float curSpawnTimer, baseSpawnTimer = 2f;


    // Seeth stat
    public float seethMod;      // Used in seeth() to modify how more or less annoyed the director will be

    public bool canWeRage = false, canPassivelySpawn = false, paused = true;

    // Griddy variables
    public List<Farm> farmList;

    // What I've spawn
    public List<Enemy> mySpawnedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        mySpawnedEnemies = new List<Enemy>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        ragePerSecond += derivative * Time.deltaTime;
    }
    void Update()
    {
        if (!paused)
        {
            // Increase our rage
            this.rageMeter += ragePerSecond * Time.deltaTime;

            // Build to and summons hoards
            updateRage();

            // Will passively spawn in enemies, which spends rage
            spawnPassively();
        } 
    }

    public void spawnPassively()
    {
        if (canPassivelySpawn == false)
            // We aren't allowed to passively spawn enemies, drop out
            return;

        if (curSpawnTimer <= 0)
        {
            // Spawn an enemy
            float spawndirection = Random.Range(0f, 360f);
            spawndirection = Mathf.Deg2Rad * spawndirection;
            float y = spawnDistance * Mathf.Sin(spawndirection);
            float x = spawnDistance * Mathf.Cos(spawndirection);
            int spawnIDX = Random.Range(0, myArmy.Count);
            tryToSpawnEnemyByIdx(spawnIDX, new Vector2(x, y));
            curSpawnTimer = myArmy[spawnIDX].costInRage + baseSpawnTimer;
        }
        else
        {
            curSpawnTimer -= Time.deltaTime;
        }
    }

    public bool tryToSpawnEnemyByIdx(int idx, Vector2 spawncoords) // Checks if it can afford enemy, spawns enemy, deducts cost and returns true, otherwise returns false
    {
        if (myArmy[idx].costInRage <= rageMeter)
        {
            rageMeter -= myArmy[idx].costInRage;
            float x = Random.Range(-5, 5);
            float y = Random.Range(-5, 5);
            mySpawnedEnemies.Add(Instantiate(myArmy[idx], new Vector3(spawncoords.x + x, spawncoords.y + y), Quaternion.identity));
            //Debug.Log("Spawned" + myArmy[idx].name); 
            return true;
        }    
        return false;
    }

    public void updateRage()
    {
        if (canWeRage == false)
            // We aren't allowed to rage, drop out
            return;


        if (myArmy.Count > 0)
        {
            // Check if rage has met or exceeded target rage
            if (this.rageMeter >= this.targetRage)
            {                
                // Generate a random spawn point
                float spawndirection = Random.Range(0f, 360f);
                spawndirection = Mathf.Deg2Rad * spawndirection;
                float y = spawnDistance * Mathf.Sin(spawndirection);
                float x = spawnDistance * Mathf.Cos(spawndirection);

                int numberOfFails = 0;
                
                while (numberOfFails < 3) // Swap this to our lowest cost enemy
                {
                    // Spawn monsters until we run out of rage
                    // We'll need to figure out what our lowest cost enemy is
                    int spawnIDX = Random.Range(0, myArmy.Count);

                    if (tryToSpawnEnemyByIdx(spawnIDX, new Vector2(x, y)) == true)
                        continue;
                    numberOfFails++;
                }
            }
        }
        else
        {
            Debug.Log("Army too small");
        }
    }

    // Enum used for seeth(), so that you do not need to type in a rage increase amount
    public enum rageAmount
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    public void seeth(rageAmount amount)
    {
        // Whenever the player does something, it will 'annoy' the director 
        switch(amount)
        {
            case rageAmount.SMALL:
            {
                rageMeter += 5 * seethMod;
                break;
            }
            case rageAmount.MEDIUM:
            {
                rageMeter += 10 * seethMod;
                break;
            }
            case rageAmount.LARGE:
            {
                rageMeter += 20 * seethMod;
                break;
            }
        }
    }

    public List<Enemy> getEnemies()
    {
        return mySpawnedEnemies;
    }

    public void destroyEnemyFromList(Enemy inenemy)
    {
        mySpawnedEnemies.Remove(inenemy);
    }
}
