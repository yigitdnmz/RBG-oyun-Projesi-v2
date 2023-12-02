using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pumpkinFighter : EnemyFighter
{
    private IDamageable lastTarget;
    public override void Damage(IDamageable target)
    {
        lastTarget= target;
        anim.SetBool("isFighting", true);
        Invoke("GiveDamage", .3f);
    }

    public void GiveDamage()
    {
        base.Damage(lastTarget);
        anim.SetBool("isFighting", false);
    }
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
            //anim.SetBool("isFighting", true);
            isFighting = true;
        }
        else
        {
            //anim.SetBool("isFighting", false);
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
}
