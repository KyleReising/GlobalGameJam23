using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // Start is called before the first frame update
    public GameObject PlaceableObject;
    public Vector2 oldPosition;
    [SerializeField] private Canvas canvas;
    public RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public int amount;
    private int oldAmount = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = BigBadCanvas.myCanvas;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("YOU ARE DRAGGING.");
        rectTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("YOU ARE ENDING DRAGGING AT POSITION:" + eventData.position);
        //Debug.Log("YOU ARE ENDING DRAGGING AT WORLD POSITION:" + Camera.main.ScreenToWorldPoint(eventData.position));
        Vector3 scren = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 v = new Vector3(scren.x, scren.y, 5); //THIS IS TO PUT IT IN FRONT.

        //GameObject g = Instantiate(PlaceableObject);
        //g.transform.position = v
        rectTransform.position = oldPosition;

        canvasGroup.blocksRaycasts = true;


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        //Debug.Log("YOU ARE BEGGINING DRAGGING.");
        oldPosition = transform.position;  //saving this for later...

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move the item off-screen if we have none
        if (amount > 0 && oldAmount != amount)
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        else if(amount <= 0)
            rectTransform.anchoredPosition = new Vector3(-200, 0, 0);

        oldAmount = amount;
    }

    public void increase()
    {
        amount += 1;
    }
}
