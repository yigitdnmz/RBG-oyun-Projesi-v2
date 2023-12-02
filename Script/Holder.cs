using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Holder : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject itemDetails;
    private GameObject itemDetailsObj;

    public Text CountText;

    [HideInInspector]
    public GraphicRaycaster raycaster;
    [HideInInspector]
    public GameObject holdingItem;
    [HideInInspector]
    public GameObject parentObject;
    [HideInInspector]
    public InventoryNew playerInvSc;
    public GameObject SkillbarHolder;
    [HideInInspector]
    public GameObject refHolder;
    public int sellPunish;

    public int StactCount = 1; 
    private void Start()
    {
        playerInvSc = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryNew>();
        raycaster = GetComponentInParent<GraphicRaycaster>();
        parentObject = transform.parent.gameObject;
    }

    private void Update()
    {
        //if (holdingItem.GetComponent<Item>().itemType == Items.Usable)
        //{
          //  CountText.enabled = true;
            //CountText.text = StactCount.ToString();
       // }
        //else
        //{
         //   CountText.enabled = false;
        //}


        Image img = transform.GetChild(0).GetComponent<Image>();
        if (holdingItem != null)
        {
            img.enabled = true;
            img.sprite = holdingItem.GetComponent<Item>().itemImage;
        }
        else
        {
            Destroy(gameObject);
        }

        if(itemDetailsObj != null)
        {
            itemDetailsObj.transform.position = Input.mousePosition;
        }
    }
    public List<RaycastResult> CheckGraphRaycasts()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(raycaster.transform);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = CheckGraphRaycasts();
        if (results.Count > 1)
        {
            foreach (RaycastResult result in results)
            {
                Button hld = result.gameObject.GetComponent<Button>();

                if (hld != null )
                {

                    if (hld.transform.childCount == 0)
                    {
                        Item hldItem = holdingItem.GetComponent<Item>();
                        /*if (hld.gameObject.CompareTag("MarketSlot")) //markete biraktiysam itemi 
                        {
                            if (parentObject.CompareTag("InvSlot")) //envantere biraktiysam itemi
                            {
                                GameObject master = parentObject.GetComponentInParent<MarketMaster>().master;
                                List<Sellable> sellingItems = master.GetComponent<MarketInteraction>().sellingItem;
                                int price = 0;
                                foreach (var sellingItem in sellingItems)
                                {
                                    var img = sellingItem.item.GetComponent<Item>().itemImage;
                                    if (img == holdingItem.GetComponent<Item>().itemImage)
                                    {
                                        price = sellingItem.price;
                                    }
                                }

                                price = (int)(price / sellPunish);
          
                                    playerInvSc.gold += price;
                                  
                            }
                        }
                        */

                        if (hld.gameObject.CompareTag("MarketSlot"))
                        {
                            if (parentObject.CompareTag("InvSlot")) //envantere biraktiysam itemi
                            {
                                GameObject master = hld.GetComponentInParent<MarketMaster>().master;
                                List<Sellable> sellingItems = master.GetComponent<MarketInteraction>().sellingItem;
                                int price = -1;
                                foreach (var sellingItem in sellingItems)
                                {
                                    var img = sellingItem.item.GetComponent<Item>().itemImage;
                                    if (img == holdingItem.GetComponent<Item>().itemImage)
                                    {
                                        price = sellingItem.price;
                                    }
                                }
                                if (price != -1) { 
                                   
                                    playerInvSc.gold += price /3 ;
                                    SetUIParent(hld);
                                }
                                SetUIParent(parentObject.GetComponent<Button>());
                                
                            }
                        }


                        if (hld.gameObject.CompareTag("InvSlot"))
                        {

                            if (parentObject.CompareTag("SilahSlot"))
                            {
                                playerInvSc.TakeAwaySword(hldItem);
                            }
                            if (parentObject.CompareTag("GoguslukSlot"))
                            {
                                playerInvSc.TakeAwayArmor(hldItem);
                            }
                            if (parentObject.CompareTag("MarketSlot"))
                            {
                                GameObject master = parentObject.GetComponentInParent<MarketMaster>().master;
                                List<Sellable> sellingItems = master.GetComponent<MarketInteraction>().sellingItem;
                                int price = 100000;
                                foreach(var sellingItem in sellingItems)
                                {
                                    var img = sellingItem.item.GetComponent<Item>().itemImage;
                                    if (img == holdingItem.GetComponent<Item>().itemImage)
                                    {
                                        price = sellingItem.price; 
                                    }
                                }
                                if(playerInvSc.gold < price)
                                {
                                    break;
                                }
                                else
                                {
                                    playerInvSc.gold -= price;
                                }

                            }
                            transform.SetParent(hld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        }
                        if (hld.gameObject.CompareTag("SilahSlot") && hldItem.itemType == Items.Sword && parentObject.CompareTag("InvSlot"))
                        {
                            transform.SetParent(hld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            playerInvSc.PutOnSword(holdingItem.GetComponent<Item>());
                        }
                        if (hld.gameObject.CompareTag("GoguslukSlot") && hldItem.itemType == Items.Breastplate && parentObject.CompareTag("InvSlot"))
                        {
                            transform.SetParent(hld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            playerInvSc.PutOnArmor(holdingItem.GetComponent<Item>());
                        }
                        if (hld.gameObject.CompareTag("SkillBarSlot") && hldItem.itemType == Items.Usable && parentObject.CompareTag("InvSlot"))
                        {
                            if(refHolder != null)Destroy(refHolder);
                            refHolder = Instantiate(SkillbarHolder);
                            refHolder.transform.SetParent(hld.transform);
                            refHolder.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                            refHolder.GetComponent<SkiilbarHolder>().refHolder = this;
                        }
                    }
                }
                
            }

        }

        else
        {
            GameObject slotItem = holdingItem;
            GameObject newObj = Instantiate(slotItem, playerInvSc.transform.position + playerInvSc.transform.forward * 2 + playerInvSc.transform.up, UnityEngine.Quaternion.identity);
            newObj.GetComponent<Rigidbody>().isKinematic = false;
            newObj.GetComponent<Collider>().enabled = true;
            Destroy(gameObject);
        }

        if (transform.parent == raycaster.transform) { 
            transform.SetParent(parentObject.transform);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
    }


    public void SetUIParent(Button target)
    {
        transform.SetParent(target.transform);
        parentObject = transform.parent.gameObject;
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if(RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(),Input.mousePosition))
        {
            //OnPointerEnter(null);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (parentObject.CompareTag("MarketSlot"))
            {
                return;
            }
            switch (holdingItem.GetComponent<Item>().itemType)
            {
                case Items.Usable:
                    IUsable comp = holdingItem.GetComponent<IUsable>();
                     if (comp != null) comp.OnUse(this);
                    /* if(StactCount > 1)
                     {
                         StactCount -= 1;
                     }
                     else
                     {

                     }
                     */
                    Destroy(gameObject);
                    break;
                case Items.skill:
                    //TODO be added 
                    break;
                case Items.Sword:
                    if (parentObject.CompareTag("InvSlot")) 
                    { 
                    GameObject shld = GameObject.FindGameObjectWithTag("SilahSlot");
                        if (shld.transform.childCount == 0)
                        {
                            transform.SetParent(shld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            playerInvSc.PutOnSword(holdingItem.GetComponent<Item>());
                        }
                    }
                    else
                    {
                        GameObject hld = playerInvSc.FindEmptySpaceInv();
                        if (hld != null)
                        {
                            transform.SetParent(hld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            playerInvSc.TakeAwaySword(holdingItem.GetComponent<Item>());
                        }
                    }
                    break;
                case Items.Breastplate:
                    if (parentObject.CompareTag("InvSlot")) { 
                    GameObject ghld = GameObject.FindGameObjectWithTag("GoguslukSlot");
                        if (ghld.transform.childCount == 0)
                        {
                            transform.SetParent(ghld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            playerInvSc.PutOnArmor(holdingItem.GetComponent<Item>());
                        }
                    }
                    else
                    {
                        GameObject hld = playerInvSc.FindEmptySpaceInv();
                        if (hld != null)
                        {
                            transform.SetParent(hld.transform);
                            parentObject = transform.parent.gameObject;
                            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            playerInvSc.TakeAwayArmor(holdingItem.GetComponent<Item>());
                            
                        }
                    }
                    break;
            }
        }    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemDetailsObj = Instantiate(itemDetails, raycaster.transform);
        itemDetailsObj.transform.position = Input.mousePosition;
        itemDetailsObj.GetComponent<ItemDetailsController>().SetHoldingItem(holdingItem.GetComponent<Item>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(itemDetailsObj);
    }
}
