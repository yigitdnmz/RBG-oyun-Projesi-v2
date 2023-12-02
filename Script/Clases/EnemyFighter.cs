using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour, IFighter, IDamageable
{
    public int baseAd { get; set; } = 1;
    public int ad { get; set; } = 1;
    public float attackSpd { get; set; } = .5f;
    public float baseAttackSpd { get; set; } = .5f;
    public float curHp { get; set; }
    public float hp { get; set; } = 20;
    public float baseHp { get; set; } = 20;
    public float dex { get; set; } = 3;
    public float baseDex { get; set; } = 3;
    public GameObject hpBarFront;
    public Animator anim;
    public GameObject targetObj;
    public MutantMovement movement;
    public bool isFighting;
    public float curCoolDown;
    public float coolDown;

    public GameObject lastHit;

    private void Start()
    {
        movement = GetComponent<MutantMovement>();
        anim = GetComponent<Animator>();
        curHp = hp;
    }

    public virtual void Damage(IDamageable target)
    {
        target.gameObject.GetComponent<IDamageable>().TakeDamage(this, ad);
    }

    public void TakeDamage(IFighter sender, int damageAmount)
    {
        lastHit = sender.gameObject;

        if (targetObj == null)
        {
            targetObj = sender.gameObject;
            coolDown = 1 / attackSpd;
            curCoolDown = coolDown;
        }

        curHp -= damageAmount / dex;
        float x = curHp * 2 / hp;
        RectTransform tf = hpBarFront.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(x, tf.sizeDelta.y);

    }

}
