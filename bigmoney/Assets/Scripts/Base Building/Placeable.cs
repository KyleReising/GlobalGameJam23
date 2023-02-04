using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Placeable : MonoBehaviour
{
    public int hp, mhp;  //hit points, maximum hit points.
    public Slider hpSlider;
    public List<Organizer.Tag> myTags;
    public int id;
    public Mound myDirt;
    
    
    

    void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

    }



    public void UpdateUI()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = mhp;
            hpSlider.value = hp;

        }
        
    }

    public List<Organizer.Tag> GetTags()
    {
        return myTags;
    }



}
