using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MutantFighter : EnemyFighter, IFighter, IDamageable
{
    /*
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
    private Animator anim; 
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
    */
    private void Update()
    {
        if (curHp <= 0)
        {
            //TODO oldun
            curHp = 0;
            GetComponent<MutantDie>().Die();
        }
        if (targetObj != null && !GetComponent<MutantMovement>().isNavMeshing)
        {
            anim.SetBool("isFighting", true);
            isFighting = true;
        }
        else
        {
            anim.SetBool("isFighting", false);
            isFighting = false;
        }

        if (isFighting)
        {
            Vector3 lookTransform = new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z);
            transform.LookAt(targetObj.transform);
            if (Vector3.Distance(targetObj.transform.position, transform.position) > 3)
            {
                movement.walk(targetObj.transform.position); 
            }
            curCoolDown -= Time.deltaTime;
            if (curCoolDown <= 0)
            {
                curCoolDown = coolDown;
                Damage(targetObj.GetComponent<IDamageable>());

            }
        }
    }
    /*
    public void Damage(IDamageable target)
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

        curHp -= damageAmount/ dex;
        float x = curHp * 2 / hp;
        RectTransform tf = hpBarFront.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(x, tf.sizeDelta.y);
        
    }
    
    */
}
