using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkiilbarHolder : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Holder refHolder;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (refHolder == null) Destroy(gameObject);
        Image img = transform.GetChild(0).GetComponent<Image>();
        if (refHolder.holdingItem != null)
        {
            img.enabled = true;
            img.sprite = refHolder.holdingItem.GetComponent<Item>().itemImage;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(refHolder.raycaster.transform);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnRightClick()
    {
        switch (refHolder.holdingItem.GetComponent<Item>().itemType)
        {
            case Items.Usable:
                IUsable comp = refHolder.holdingItem.GetComponent<IUsable>();
                if (comp != null) comp.OnUse(refHolder);
                Destroy(refHolder.gameObject);
                break;
            case Items.skill:
                //TODO be added 
                break;
        }
    }

}
