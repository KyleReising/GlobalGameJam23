using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValues : MonoBehaviour
{
    public int val;
    public Button btn;
    public Text cost;

    public void Start()
    {
        btn = GetComponent<Button>();
        updateVal(1.0f);
    }

    public void changeInteract(bool x)
    {
        btn.interactable = x;
    }

    public void updateVal(float mul)
    {
        float temp = val;
        temp *= mul;
        val = (int)temp;

        if (val >= 1000000000)
            cost.text = "$" + (((float)val+1) / 1000000000).ToString("F1") + "B";
        else if (val >= 1000000)
            cost.text = "$" + (((float)val + 1) / 1000000).ToString("F1") + "M";
        else if (val >= 1000)
            cost.text = "$" + (((float)val + 1) / 1000).ToString("F1") + "k";
        else
            cost.text = "$" + ((float)val).ToString();
    }
}
