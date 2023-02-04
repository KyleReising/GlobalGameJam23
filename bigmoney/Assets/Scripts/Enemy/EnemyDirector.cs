using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    // Stats
    public float targetRage;    // Once the rageMeter hits the targetRage, the director will spawn a horde
    public float rageMeter;     // A 'currency' the director will use to buy enemies
    public float ragePerSecond; // How much rage the director passively gains

    public float seethMod;      // Used in seeth() to modify how more or less annoyed the director will be

    // Griddy variables
    public List<Farm> farmList; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRage()
    {
        // Increase our rage
        this.rageMeter += ragePerSecond * Time.deltaTime;
        
        // Check if rage has met or exceeded target rage
        if (this.rageMeter >= this.targetRage)
        {
            while (rageMeter >= 0)
            {
                rageMeter = 0;

                // Spawn monsters until we run out of rage
                // We can spend rage that we don't have
                // For example, if we have 40 rage, but a unit costs 50, we can still spawn it

                // Generate a random spawn point
            }
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
}
