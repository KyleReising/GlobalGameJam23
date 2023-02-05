using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class SprinklerMachine : Placeable
{


    public void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Mound>().fertility += 1;
    }

}
