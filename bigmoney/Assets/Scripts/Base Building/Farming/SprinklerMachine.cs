using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerMachine : Placeable
{

    public void Start()
    {
    }

    //if we hit mound, upgrade fertility
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Mound>() != null)
        {
            collision.gameObject.GetComponent<Mound>().fertility += 1;
        }
            
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Mound>() != null)
        {
            collision.gameObject.GetComponent<Mound>().fertility -= 1;
        }
    }

    

}
