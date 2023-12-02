using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDetailsController : MonoBehaviour
{
    public Text itemNameText;
    public Text itemStatText;

    private Item holdingItem;

    public void SetHoldingItem (Item item)
    {
        switch (item.itemType)
        {
            case Items.Sword:
                Sword a = (Sword)item;
                item.itemStats = "Saldýrý Gücü: " + a.damage;
                break;
            case Items.Breastplate: 
                Armor b = (Armor)item;
                item.itemStats = "Savunma: " + b.armor;
                break;
            case Items.Usable:
                item.itemStats = "";
                break;
                
        }


        holdingItem = item;
        itemNameText.text = item.itemName;
        itemStatText.text = item.itemStats;
    }

}
