using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantMovement : MonoBehaviour
{
    private Animator anim;
    private MutantFighter fighter;
    private NavMeshAgent agent;
    public bool isNavMeshing;

    private void Start()
    {
        fighter = GetComponent<MutantFighter>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.remainingDistance <= 1.5f && !agent.pathPending)
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
        anim.SetBool("isFighting", false);
        agent.SetDestination(pos);
        isNavMeshing = true;
    }
}