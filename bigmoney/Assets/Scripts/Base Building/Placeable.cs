using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Placeable : MonoBehaviour
{
    public float hp = 10, mhp = 10;  //hit points, maximum hit points.
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

    public virtual bool takeDamage(float damage)
    {
        this.hp -= damage;

        if (this.hp <= 0)
        {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }
}
