using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public int money;
    public Farm farm;
    public Text txt;
    public List<ButtonValues> btnList;

    private void Start()
    {
        txt.text = "0";
    }

    private void Update()
    {

        //button visibility
        foreach (ButtonValues btn in btnList)
        {
            if (money < btn.val)
                btn.changeInteract(false);
            else
                btn.changeInteract(true);
        }

        //money numbr
        txt.text = money.ToString();
    }

    //remove money on purchase
    public void purchase(ButtonValues btn)
    {
        money -= btn.val;
    }


    //update money on harvest
    public void harvest()
    {
        List<Placeable> a = farm.SearchByTag(Organizer.Tag.Vegtable);
        foreach (Placeable veg in a)
        {
            Debug.Log(veg);
            money += veg.gameObject.GetComponent<Vegetable>().harvest();

        }
        
    }
}
