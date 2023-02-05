using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harvester : Placeable
{
    // Start is called before the first frame update
    public float rate;
    public float waited;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waited += Time.deltaTime;

        if(waited > rate)
        {
            Market.me.harvest(false);
            waited -= rate;
        }
    }
}
