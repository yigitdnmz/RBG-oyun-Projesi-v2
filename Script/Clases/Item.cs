using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Item : MonoBehaviour, Interactable
{
    public string itemName;
    public string itemStats;
    public Items itemType;
    public Sprite itemImage;
    public PlayerInteraction actualSender;

    public void Onclick(PlayerInteraction sender, Vector3 clickPos)
    {
        if (Vector3.Distance(sender.transform.position, transform.position) > 3)
        {
            sender.GetComponent<PlayerMovement>().walk(transform.position);
        }
        else
        {
            InventoryNew inv = sender.GetComponent<InventoryNew>();
            inv.AddItemTOInventory(gameObject);
            actualSender = sender;
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        
    }

    public void OnContinueClick(PlayerInteraction sender, Vector3 clickPos)
    {
        //yigit donmez
    }

    public void OnRaycastEnd(PlayerInteraction sender, Vector3 clickPos)
    {
        GetComponent<Outline>().enabled = false;
    }

    public void OnRaycastStart(PlayerInteraction sender, Vector3 clickPos)
    {
        GetComponent<Outline>().enabled = true;
    }

    private void OnDestroy()
    {
        if (actualSender != null) actualSender.lastInt = null; 
    }
}

