using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : Placeable
{
    public int value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual int harvest()
    {
        return value;
    }
}
