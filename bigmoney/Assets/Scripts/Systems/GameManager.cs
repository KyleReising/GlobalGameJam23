using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public int startingCash;
    public int cash;

    public List<Farm> listOfFarms;


    // Start is called before the first frame update
    void Awake()
    {
        if(GM == null)
        {
            GM = this;
        }
        GM.cash = startingCash;

        listOfFarms = new List<Farm>(GameObject.FindObjectsOfType<Farm>());
    }

    public bool Spend(int t) //Returns true if you actually CAN spend money.
    {
        if(GM.cash - t > 0) //Can you afford it?
        {
            GM.cash -= t;
            //Play SFX here...
            return true;
        }
        else//Id not, send error...
        {
            //Play ERROR sfx here...
            return false;
        }
    }

    public void Earn(int t)
    {
        GM.cash += t;
        //play cha-ching sound??
    }

    public int PonderMyHoardOfCash()
    {
        return GM.cash;
    }

    // Update is called once per frame

    public List<Placeable> getFarmPlaceable()
    {
        // When we have more farms, we can swap it to do a for each
        List<Placeable> templist = new List<Placeable>();

        foreach(Farm f in listOfFarms)
        {
            templist.AddRange(f.GetAllPlaceables());
        }

        return templist;
    }

    public List<Placeable> getFarmPlaceableByTags(List<Organizer.Tag> tags)
    {
        // When we have more farms, we can swap it to do a for each
        List<Placeable> templist = new List<Placeable>();

        foreach (Farm f in listOfFarms)
        {
            templist.AddRange(f.SearchByTag(tags));
        }

        return templist;
    }

    public void spawnAttackLine(Vector2 startpos, Vector2 endpos)
    {
        LineRenderer CurrentLR = new GameObject().AddComponent<LineRenderer>() as LineRenderer;
        CurrentLR.SetPosition(0, new Vector3(startpos.x, startpos.y, -4));
        CurrentLR.SetPosition(1, new Vector3(endpos.x, endpos.y, -4));

        CurrentLR.startWidth = 0.25f;
        CurrentLR.endWidth = 0.25f;
        CurrentLR.material = new Material(Shader.Find("Sprites/Default"));
        CurrentLR.startColor = Color.red;
        CurrentLR.endColor = Color.red;

        Destroy(CurrentLR, 0.5f);
    }
}
