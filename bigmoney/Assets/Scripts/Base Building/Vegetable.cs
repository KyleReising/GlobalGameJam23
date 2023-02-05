using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        int a = (int)Mathf.Pow(2, (myDirt.fertility)-1);
        return value * a;
    }


}
