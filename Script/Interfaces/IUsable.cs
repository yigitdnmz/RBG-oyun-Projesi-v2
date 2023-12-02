using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    void OnUse(Holder sender);
    void OnUse (InventoryNew sender);
}