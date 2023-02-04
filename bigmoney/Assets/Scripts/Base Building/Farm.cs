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

    //Placeables are marked with given tags . . . give me a list of tags!
    public List<Placeable> SearchByTag(List<Organizer.Tag> tags)
    {
        List<Placeable> p = new List<Placeable>();
        foreach(Mound m in mounds)
        {
            if (m.occupant != null)
            {
                foreach (Organizer.Tag t in tags)
                {
                    foreach (Organizer.Tag h in m.occupant.myTags)
                    {
                        if (t == h)
                        {
                            p.Add(m.occupant);
                        }
                    }
                }
            }
        }

        return p; //return list of all placeable objects that match the tags you give!
    }
    //Search for all placeables that match the given example's ID value.
    public List<Placeable> SearchById(Placeable example)
    {
        List<Placeable> p = new List<Placeable>();
        foreach (Mound m in mounds)
        {
            if(m.occupant != null)
            {
                if(m.occupant.id == example.id)
                {
                    p.Add(m.occupant);
                }
            }
        }
        return p;
    }
    //Search for all placeables that match the hard coded ID value given.
    public List<Placeable> SearchById(int example)
    {
        List<Placeable> p = new List<Placeable>();
        foreach (Mound m in mounds)
        {
            if (m.occupant != null)
            {
                if (m.occupant.id == example)
                {
                    p.Add(m.occupant);
                }
            }
        }
        return p;
    }

    public Placeable GetPlaceable(Placeable example)
    {
        Placeable p = null;
        foreach (Mound m in mounds)
        {
            if (m.occupant == example)
            {
                return p;
            }
        }
        return p;
    }
}
