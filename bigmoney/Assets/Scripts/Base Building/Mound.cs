using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Mound : MonoBehaviour, IDropHandler
{
    public RectTransform RT;
    public Item i;
    public Placeable occupant;


    public void OnDrop(PointerEventData eventData)
    {
        
        //Debug.Log("YOU HAVE BEEN DROPPED ON.");
        //event data contains information about "what" was dragged onto us.
        //eventData.pointerDrag is the literal gameObject that's on top of us.  Let's get an item from the gameobject!
        i = eventData.pointerDrag.GetComponent<Item>();
        Debug.Log(i.amount);
        i.amount -= 1;
        GameObject G = Instantiate(i.PlaceableObject, RT);
        G.transform.localPosition = Vector3.zero;
        if (G.GetComponent<Placeable>() && occupant == null)
        {
            //I own the placeable as an occupant, and the placeable that knows i Own it
            occupant = G.GetComponent<Placeable>();
            G.GetComponent<Placeable>().myDirt = this;
        }
        else
        {
            //fail code here.
        }



    }
    // Start is called before the first frame update
    void Start()
    {
        RT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
