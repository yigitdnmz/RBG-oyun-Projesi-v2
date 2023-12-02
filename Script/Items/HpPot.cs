using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using Unity.VisualScripting;
using UnityEngine;

public class HpPot : Item, IUsable
{
    public int healAmount = 5;

    public void OnUse(Holder sender)
    {
        PlayerFighter statsSc = sender.playerInvSc.GetComponent<PlayerFighter>();
        int hpDegeri = healAmount;
        statsSc.curHp += hpDegeri;
        if (statsSc.curHp > statsSc.hp) statsSc.curHp = statsSc.hp;
    }

    public void OnUse(InventoryNew sender)
    {
        PlayerFighter statsSc = sender.GetComponent<PlayerFighter>();
        int hpDegeri = healAmount;
        statsSc.curHp += hpDegeri;
        if (statsSc.curHp > statsSc.hp) statsSc.curHp = statsSc.hp;
    }
}
