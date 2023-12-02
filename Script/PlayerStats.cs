using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameObject statPencere;
    private PlayerFighter playerFighter;

    //gorsellik
    public Text str;
    public Text toStr;

    public Text dex;
    public Text toDex;

    public Text vit;
    public Text toVit;

    public Text intelligence;
    public Text totInt;

    public Text levelText;
    public Text exp;
    public Text statPointText; 
    public RectTransform expLevel;

    public GameObject buttonsParent;

    //asil degerler
    public int statPoints = 0;
    public int level = 1;
    public float expExponential = 1.2f;
    public float curExp = 0;
    public float reqExp = 10;
    public int reqFirstExp = 10;
     
    // Start is called before the first frame update
    void Start()
    {
        playerFighter = GetComponent<PlayerFighter>();
        statPencere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            statPencere.SetActive(!statPencere.activeSelf);
        }

        if (statPoints <= 0)
        {
            buttonsParent.SetActive(false);
        }
        else
        {
            buttonsParent.SetActive(true);
        }


        if(curExp >= reqExp)
        {
            level += 1;
            reqExp *= expExponential;
            reqExp = (int) reqExp;
            curExp = 0;
            statPoints += 1;
        }


        str.text = playerFighter.baseAd.ToString();
        toStr.text = playerFighter.ad.ToString();
        dex.text = playerFighter.baseDex.ToString();
        toDex.text = playerFighter.dex.ToString();
        vit.text = playerFighter.baseHp.ToString();
        toVit.text = playerFighter.hp.ToString();
        statPointText.text = statPoints.ToString();
        intelligence.text = "0";
        totInt.text = "0";
        levelText.text = level.ToString();
        exp.text = curExp + "/" + reqExp;
        expLevel.sizeDelta = new Vector2(400 * (curExp / reqExp), expLevel.sizeDelta.y);
    }

    public void IncStr()
    {
        playerFighter.baseAd += 1;
        playerFighter.ad += 1;
        statPoints -= 1;
    }


    public void IncDex()
    {
        playerFighter.baseDex += 1;
        playerFighter.dex += 1;
        statPoints -= 1;
    }

    public void IncVit()
    {
        float curYuzde = playerFighter.curHp / playerFighter.hp;
        playerFighter.baseHp += 1;
        playerFighter.hp += 1;
        playerFighter.curHp = curYuzde * playerFighter.hp;
        statPoints -= 1;
    }

    public void IncInt()
    {
            //TODO
    }

    public void AddExp(int amount)
    {
        curExp += amount; 
    }


}
