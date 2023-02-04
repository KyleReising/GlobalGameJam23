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
        return listOfFarms[0].GetAllPlaceables();
    }
}
