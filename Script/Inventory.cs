using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Numerics;

public class Inventory : MonoBehaviour
{

    public List<GameObject> items = new List<GameObject>();
    private Holder[] invButtons;
    private Holder[] charButtons;
    private Holder[] skillButtons;


    public GameObject inventory;
    public GameObject swordHolder;
    public GameObject armorHolder;


    public GameObject itemBtnParent;
    public GameObject charBtnParent;
    public GameObject skillBtnParent;

    public Text goldText;
    public int gold = 0;
    private PlayerFighter statsSc;
    public GraphicRaycaster raycaster;
    // charButtons 0: silah1: zirh2:
    // Start is called before the first frame update
    void Start()
    {

        statsSc = GetComponent<PlayerFighter>();

        skillButtons = skillBtnParent.GetComponentsInChildren<Holder>();

        invButtons = itemBtnParent.GetComponentsInChildren<Holder>();
        charButtons = charBtnParent.GetComponentsInChildren<Holder>();
        for (int i = 0; i < invButtons.Length; i++)
        {
            var i1 = i;
            invButtons[i].GetComponent<Button>().onClick.AddListener(() => { OnInvSlotClick(i1); });
        }


        for (int i = 0; i < charButtons.Length; i++)
        {
            var i1 = i;
            charButtons[i].GetComponent<Button>().onClick.AddListener(() => { OnCharSlotClick(i1); });
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            var i1 = i;
            skillButtons[i].GetComponent<Button>().onClick.AddListener(() => { OnSkillSlotClick(i1); });
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



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            var results = CheckGraphRaycasts();
            if (results.Count != 0)
            {
                foreach (RaycastResult result in results)
                {
                    Holder hld = result.gameObject.GetComponent<Holder>();

                    if (hld != null)

                    {
                        if (hld.transform.parent.name == "character") continue;
                        if (hld.transform.parent.name == "SkillBar") continue;
                        GameObject slotItem = hld.holdingItem;
                        GameObject newObj = Instantiate(slotItem, transform.position + transform.forward, UnityEngine.Quaternion.identity);
                        newObj.GetComponent<Rigidbody>().isKinematic = false;
                        newObj.GetComponent<Collider>().enabled = true;
                        hld.holdingItem = null;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            var results = CheckGraphRaycasts();
            if (results.Count != 0)
            {
                foreach (RaycastResult result in results)
                {
                    Holder hld = result.gameObject.GetComponent<Holder>();
                    

                    if (hld != null)

                    {
                        if (hld.holdingItem.GetComponent<Item>().itemType == Items.Breastplate) continue;
                        if (hld.holdingItem.GetComponent<Item>().itemType == Items.Sword) continue;
                        if (hld.transform.parent.name == "Items")
                        {
                            AddPrefabToSkillBar(hld.holdingItem);
                            hld.holdingItem = null;
                        }
                        if(hld.transform.parent.name == "SkillBar")
                        {
                            AddPrefabToInventorty(hld.holdingItem);
                            hld.holdingItem = null;
                        }
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) OnSkillSlotClick(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) OnSkillSlotClick(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) OnSkillSlotClick(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) OnSkillSlotClick(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) OnSkillSlotClick(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) OnSkillSlotClick(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) OnSkillSlotClick(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) OnSkillSlotClick(7);

        if (gold <= 0) gold = 0;
        goldText.text = gold.ToString();

        UpdateImage(invButtons);
        UpdateImage(charButtons);
        UpdateImage(skillButtons);

    }

     void UpdateImage(Holder[] btnList)
    {
        foreach (var btn in btnList)
        {
            Image img = btn.transform.GetChild(0).GetComponent<Image>();
            if (btn.holdingItem != null)
            {
                img.enabled = true;
                img.sprite = btn.holdingItem.GetComponent<Item>().itemImage;
            }
            else
            {
                img.enabled = false;
            }
        }
    }

    void InstWeapon(GameObject weapon)
    {
        GameObject _ = Instantiate(weapon, swordHolder.transform.position, swordHolder.transform.rotation);
        _.transform.parent = swordHolder.transform;
    }

    void InstArmor(GameObject armor)
    {
        GameObject _ = Instantiate(armor, armorHolder.transform.position, armorHolder.transform.rotation);
        _.transform.parent = armorHolder.transform;
    }

    void DestroyGeneric(GameObject weapon)
    {
        Destroy(weapon);
    }

    void DrinkPot(Item holdingItem)
    {
        int hpDegeri = ((HpPot)holdingItem).healAmount;
        statsSc.curHp += hpDegeri;
        if (statsSc.curHp > statsSc.hp) statsSc.curHp = statsSc.hp;
    }

    void OnSkillSlotClick (int slot) // 1 2 3 4 5 6 7 8
    {
        Button curSlot = skillButtons[slot].GetComponent<Button>();

        GameObject holding = curSlot.GetComponent<Holder>().holdingItem;
        Image slotImage = curSlot.transform.GetChild(0).GetComponent<Image>();
        if (holding != null)
        {
            Item holdingItem = holding.GetComponent<Item>();
            switch (holdingItem.itemType)
            {
                case Items.Usable:
                    DrinkPot(holdingItem);
                    curSlot.GetComponent<Holder>().holdingItem = null;
                    break;
                case Items.skill:
                    break;
            }
        }
    }

    void OnInvSlotClick(int slot)
    {
        Button curSlot = invButtons[slot].GetComponent<Button>();
        GameObject holding = curSlot.GetComponent<Holder>().holdingItem;
        Image slotImage = curSlot.transform.GetChild(0).GetComponent<Image>();
        if (holding != null)
        {
            Item holdingItem = holding.GetComponent<Item>();
            switch (holdingItem.itemType)
            {
                case Items.Sword:
                    if (charButtons[0].holdingItem == null)
                    {
                        charButtons[0].holdingItem = holdingItem.gameObject;
                      //  curSlot.GetComponent<Holder>().holdingItem = null;
                        InstWeapon(holdingItem.gameObject);

                        int hasarDegeri = ((Sword) holdingItem).damage;
                        statsSc.ad += hasarDegeri;
                    }
                    break;
                case Items.Breastplate:
                    if (charButtons[1].holdingItem == null)
                    {
                        charButtons[1].holdingItem = holdingItem.gameObject;
                       // curSlot.GetComponent<Holder>().holdingItem = null;
                        InstArmor(holdingItem.gameObject);

                        int zirhDegeri = ((Armor)holdingItem).armor;
                        statsSc.dex += zirhDegeri;
                        
                    }
                    break;
                case Items.Usable:
                    // potu ic
                    DrinkPot(holdingItem);
                    
                    break;
            }
            curSlot.GetComponent<Holder>().holdingItem = null;
        }
    }

    void AddPrefabToInventorty(GameObject prefab )
    {
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (invButtons[i].holdingItem == null)
            {
                invButtons[i].holdingItem = prefab ;
                break;
            }
        }
    }

    void AddPrefabToSkillBar(GameObject prefab)
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].holdingItem == null)
            {
                skillButtons[i].holdingItem = prefab;
                break;
            }
        }
    }
    public void AddItemTOInventory(GameObject instance)
    {
        GameObject prefab = items.Find(x => x.GetComponent<Item>().itemImage == instance.GetComponent<Item>().itemImage);
        AddPrefabToInventorty(prefab);
    }
    void OnCharSlotClick(int slot)
    {
        Button curSlot = charButtons[slot].GetComponent<Button>();
        GameObject holding = curSlot.GetComponent<Holder>().holdingItem;
        Image slotImage = curSlot.transform.GetChild(0).GetComponent<Image>();
        if (holding != null)
        {
            Item holdingItem = holding.GetComponent<Item>();
            AddPrefabToInventorty(holdingItem.gameObject);
            if (slot == 0) // silah
            {
                DestroyGeneric(swordHolder.transform.GetChild(0).gameObject);

                int hasarDegeri = ((Sword)holdingItem).damage;
                statsSc.ad -= hasarDegeri;
                if (statsSc.ad <= statsSc.baseAd) statsSc.ad = statsSc.baseAd;
            }
            if (slot == 1)  //zirh
            {
                DestroyGeneric(armorHolder.transform.GetChild(0).gameObject);
                int zirhDegeri = ((Armor)holdingItem).armor;
                statsSc.dex -= zirhDegeri;
                if (statsSc.dex <= statsSc.baseDex) statsSc.dex = statsSc.baseDex; 
            }
            curSlot.GetComponent<Holder>().holdingItem = null;
        }
    }
}
