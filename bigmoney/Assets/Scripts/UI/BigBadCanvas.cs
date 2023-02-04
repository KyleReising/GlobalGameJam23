using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBadCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public static Canvas myCanvas;
    void Awake()
    {
        myCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
