using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private PlayerFighter fighter;
    private NavMeshAgent agent;
    public bool isNavMeshing;

    private void Start()
    {
        fighter = GetComponent<PlayerFighter>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.remainingDistance <= 2 && !agent.pathPending)
        {
            isNavMeshing = false;
            agent.SetDestination(transform.position);
        }

        if (isNavMeshing)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void walk(Vector3 pos, bool stopFighting = true)
    {
        if (stopFighting)
        {
            fighter.isFighting = false;
            fighter.targetObj = null;
        }
        anim.SetBool("isFighting", false);
        
        agent.SetDestination(pos);
        isNavMeshing= true;
    }
}