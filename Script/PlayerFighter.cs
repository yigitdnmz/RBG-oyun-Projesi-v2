using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Security.Cryptography;
using System;


public class PlayerFighter : MonoBehaviour, IDamageable, IFighter
{
    public int baseAd { get; set; } = 5;
    public int ad { get; set; } = 5;
    public float attackSpd { get; set; } = 1;
    public float baseAttackSpd { get; set; } = 1;
    public float attackRadius = 4;
    public float attacAngle = 90;
    private float coolDown;
    private float curCoolDown;
    public float curHp { get; set; }
    public float hp { get; set; } = 200;
    public float baseHp { get; set; } = 200;
    public float dex { get; set; } = 5;
    public float baseDex { get; set; } = 5;
    public GameObject HpBarFront;
    public GameObject targetObj;
    private Animator anim;
    public bool isFighting;
    private void Start()
    {
        anim = GetComponent<Animator>();
        curHp = hp;

    }
    //draws a vertical line from above, then the bed draws a 90 degree line, takes the tenth of the character as an offset, then whoever is in this angle
    private void Update()
    {
        float x = curHp * 500 / hp;
        RectTransform tf = HpBarFront.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(x, tf.sizeDelta.y);

        if (curHp <= 0 )
        {
            //TODO oldun
            curHp= 0;
        }
        if (targetObj != null && !GetComponent<PlayerMovement>().isNavMeshing)
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
            transform.LookAt(targetObj.transform);
            curCoolDown -= Time.deltaTime;
            if (curCoolDown <= 0)
            {
                Collider[] objs = Physics.OverlapSphere(transform.position, attackRadius);
                foreach(Collider obj in objs)
                {
                    IDamageable damagable = obj.GetComponentInParent<IDamageable>();
                    if (damagable != null)
                    {
                        if(Vector3.Angle(transform.forward, obj.transform.position - transform.position) < attacAngle)
                        {
                            Damage(damagable);
                        }
                        
                    }
                }
                curCoolDown = coolDown;
                
                
            }
        }
    }

    public void Damage(IDamageable target)
    {
        target.gameObject.GetComponent<IDamageable>().TakeDamage(this, ad);
    }

    public void TakeDamage(IFighter sender, int damageAmount)
    {
        curHp -= damageAmount / dex;
        

        
    }
    

    public void SetTarget(GameObject target)
    {
        targetObj = target;
        GetComponent<PlayerMovement>().walk(target.transform.position, false);

        coolDown = 1 / attackSpd;
        curCoolDown = coolDown;
    }
}
