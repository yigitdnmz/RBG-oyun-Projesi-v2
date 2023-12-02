using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFighter
{
    GameObject gameObject { get; }
    int baseAd { get; set; }
    int ad { get; set; }
    float attackSpd { get; set; }
    float baseAttackSpd { get; set; }

    void Damage(IDamageable target);
}
