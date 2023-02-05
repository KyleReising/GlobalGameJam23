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
    }

    public void changeInteract(bool x)
    {
        btn.interactable = x;
    }

    public void updateVal(int mul)
    {
        val *= mul;
    }
}
