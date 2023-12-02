using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    GameObject gameObject { get; }
    float curHp { get; set; }
    // Start is called before the first frame update
    float hp { get; set; }
    float baseHp { get; set; }
    float dex { get; set; }
    float baseDex { get; set; }

    void TakeDamage(IFighter sender,  int damageAmount);
}
