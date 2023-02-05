using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Mound> mounds = new List<Mound>();
    public GameObject retrypanel;

    private void Update()
    {
        if (NumOfPlace() < 1)
        {
            retrypanel.SetActive(true);
            Debug.Log("Game over");
            Time.timeScale = 0.0f;

        }
    }

    void Start()
    {
        foreach(Mound m in GetComponentsInChildren<Mound>())
        {
            mounds.Add(m);
        }
    }

    public List<Placeable> SearchByTag(Organizer.Tag tag)
    {
        List<Placeable> p = new List<Placeable>();
        foreach (Mound m in mounds)
        {
            if (m.occupant != null)
            {
                foreach (Organizer.Tag h in m.occupant.myPreferenceTags)
                {
                    if (tag == h)
                    {
                        p.Add(m.occupant);
                    }
                }

            }
        }

        return p; //return list of all placeable objects that match the tags you give!
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
                    foreach (Organizer.Tag h in m.occupant.myPreferenceTags)
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

    public List<Placeable> GetAllPlaceables()
    {
        List<Placeable> p = new List<Placeable>();
        foreach (Mound m in mounds)
        {
            if (m.occupant != null)
            {
                p.Add(m.occupant);
            }
        }
        return p;
    }

    public int NumOfPlace()
    {
        int i = 0;
        foreach(Mound m in mounds)
        {
            if (m.occupant != null)
                i++;
        }
        return i;
    }
}
