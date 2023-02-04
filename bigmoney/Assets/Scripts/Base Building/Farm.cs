using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Mound> mounds = new List<Mound>();
    void Start()
    {
        foreach(Mound m in GetComponentsInChildren<Mound>())
        {
            mounds.Add(m);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
