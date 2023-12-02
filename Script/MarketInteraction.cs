using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class MarketInteraction : NPCInteraction
{
   
    public List<Sellable> sellingItem = new List<Sellable>();
    //public List<GameObject> marketSlots= new List<GameObject>();
    public GameObject[] marketSlots;
    public GameObject marketObj;
    public GameObject itemHolder;

    private void Start()
    {
        marketSlots = GameObject.FindGameObjectsWithTag("MarketSlot");
        marketObj.SetActive(false);
    }
    public override void OnInteract()
    {
        marketObj.SetActive(true);
        marketObj.GetComponent<MarketMaster>().master = gameObject;
        foreach (var item in sellingItem)
        {
            AddPrefabToMarket(item.item, item.usableCount);
        }
    }

    public GameObject FindEmptySpaceMarket()
    {
        for (int i = 0; i < marketSlots.Length; i++)
        {
            if (marketSlots[i].transform.childCount == 0)
            {
                return marketSlots[i];
            }
        }
        return null;
    }

    void AddPrefabToMarket(GameObject prefab, int usableCount)
    {
        GameObject emptyspace = FindEmptySpaceMarket();
        if (emptyspace != null)
        {
            GameObject newItemSlot = Instantiate(itemHolder, emptyspace.transform);
            newItemSlot.GetComponent<Holder>().holdingItem = prefab;
            //newItemSlot.GetComponent<Holder>().
        }
    }

    private void Update()
    {
        if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) >= etkinlesmeMesafe)
        {
            marketObj.SetActive(false);
        }
    }

}
[System.Serializable]
public class Sellable
{
    public GameObject item;
    public int price;
    public int usableCount =1;
}