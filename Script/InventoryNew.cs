using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNew : MonoBehaviour
{

    public List<GameObject> items = new List<GameObject>();

    private GameObject[] invButtons;
    private GameObject[] skillButtons;


    public GameObject itemHolder;
    private PlayerFighter statsSc;

    public GameObject swordHolder;
    public GameObject armorHolder;
    // charButtons 0: silah1: zirh2:

    public Text goldText;
    public int gold = 100;

    public GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        skillButtons = GameObject.FindGameObjectsWithTag("SkillBarSlot");
        statsSc = GetComponent<PlayerFighter>();
        invButtons = GameObject.FindGameObjectsWithTag("InvSlot");
        goldText = GameObject.FindGameObjectWithTag("Gold").GetComponent<Text>();

        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = gold.ToString();

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) OnSkillSlotClick(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) OnSkillSlotClick(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) OnSkillSlotClick(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) OnSkillSlotClick(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) OnSkillSlotClick(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) OnSkillSlotClick(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) OnSkillSlotClick(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) OnSkillSlotClick(7);
    }

    public void AddItemTOInventory(GameObject instance)
    {
        GameObject prefab = items.Find(x => x.GetComponent<Item>().itemImage == instance.GetComponent<Item>().itemImage);
        AddPrefabToInventorty(prefab);
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

    public void PutOnSword(Item swordPrefab)
    {
        InstWeapon(swordPrefab.gameObject);

        int hasarDegeri = ((Sword)swordPrefab).damage;
        statsSc.ad += hasarDegeri;


    }



    public void  TakeAwaySword(Item swordPrefab)
    {
        int hasarDegeri = ((Sword) swordPrefab).damage;
        statsSc.ad -= hasarDegeri;

        if (statsSc.ad <= statsSc.baseAd) statsSc.ad = statsSc.baseAd;
        Destroy(swordHolder.transform.GetChild(0).gameObject);
    }

    public void TakeAwayArmor(Item armorPrefab)
    {
        int zirhDegeri = ((Armor)armorPrefab).armor;
        statsSc.dex -= zirhDegeri;
        if (statsSc.dex <= statsSc.baseDex) statsSc.dex = statsSc.baseDex;
        Destroy(armorHolder.transform.GetChild(0).gameObject);
    }



    public void PutOnArmor(Item armorPrefab)
    {
        InstArmor(armorPrefab.gameObject);

        int zirhDegeri = ((Armor) armorPrefab).armor;
        statsSc.dex += zirhDegeri;


    }

    /* void AddPrefabToInventorty(GameObject prefab)
     {
         if (prefab.GetComponent<Item>().itemType != Items.Usable)
         {
             GameObject emptySpace = FindEmptySpaceInv();
             if(emptySpace != null)
             { 
             GameObject newItemSlot = Instantiate(itemHolder, emptySpace.transform);
             newItemSlot.GetComponent<Holder>().holdingItem = prefab;
             }
          }

     }*/

    void AddPrefabToInventorty(GameObject prefab, GameObject emptyace = null)
    {
        //if (prefab.GetComponent<Item>().itemType != Items.Usable)
        //{
        if (emptyace == null)
        {
            emptyace = FindEmptySpaceInv();
        }
        if (emptyace != null)
        {
            GameObject newItemSlot = Instantiate(itemHolder, emptyace.transform);
            newItemSlot.GetComponent<Holder>().holdingItem = prefab;
        }
        //}
    }
    /*
        else
        {
            if(emptyace == null)
            {
                Holder matchingUsable = MatchingUsable(prefab.GetComponent<Item>().itemImage); 
                if (matchingUsable != null)
                {
                    matchingUsable.StactCount += 1;
                }
                else
                {
                    emptyace = FindEmptySpaceInv();
                    GameObject newItemSlot = Instantiate(itemHolder, emptyace.transform);
                    newItemSlot.GetComponent<Holder>().holdingItem = prefab;
                }
            }
        }
    }*/
   // ITEM STACKLEME DENEME
   /* public Holder MatchingUsable(Sprite img)
    {
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (invButtons[i].transform.childCount != 0)
            {
                Holder curChild = invButtons[i].transform.GetChild(0).GetComponent<Holder>();
                if (curChild.holdingItem.GetComponent<Item>().itemImage == img) 
                { 
                     return curChild;
                }
            }
        }
        return null;
    }*/

    public  GameObject FindEmptySpaceInv()
    {
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (invButtons[i].transform.childCount == 0)
            {
                return invButtons[i];
            }
        }
        return null;
    }


    void OnSkillSlotClick(int slot) // 1 2 3 4 5 6 7 8
    {
        Button curSlot = skillButtons[slot].GetComponent<Button>();

        if (curSlot.transform.childCount != 0)
        {
            SkiilbarHolder refHolder = curSlot.transform.GetChild(0).GetComponent<SkiilbarHolder>();
            if (refHolder != null)
            {
                refHolder.OnRightClick();
            }


        }
    }

}
