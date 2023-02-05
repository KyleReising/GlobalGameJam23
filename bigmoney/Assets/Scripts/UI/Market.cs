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
    public static Market me;
    public AudioSource audioSource;
    public GameObject winPanel;
    public GameObject retPanel;

    private void Start()
    {
        if(me == null)
        {
            me = this;
            me.audioSource = GetComponent<AudioSource>();
        }
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
        if (money >= 1000000000)
            txt.text = (((float)money + 1) / 1000000000).ToString("F1") + "B";
        else if (money >= 1000000)
            txt.text = (((float)money + 1) / 1000000).ToString("F1") + "M";
        else if (money >= 1000)
            txt.text =  (((float)money + 1) / 1000).ToString("F1") + "k";
        else
            txt.text =  ((float)money).ToString();


    }

    //remove money on purchase
    public void purchase(ButtonValues btn)
    {
        money -= btn.val;
    }


    //update money on harvest
    public void harvest(bool shouldSound)
    {
        List<Placeable> a = farm.SearchByTag(Organizer.Tag.Vegtable);
        foreach (Placeable veg in a)
        {
            money += veg.gameObject.GetComponent<Vegetable>().harvest();

        }
        if(!audioSource.isPlaying && shouldSound)
            audioSource.Play();


        if(money < 0)
        {
            Time.timeScale = 0.0f;
            winPanel.SetActive(true);
            retPanel.SetActive(true);
        }
        
    }
}
