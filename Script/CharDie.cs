using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDie : MonoBehaviour
{
    private bool isDied = false; 

    public void Die()
    {
        GetComponentInChildren<Collider>().enabled = false; 
        GetComponent<Rigidbody>().isKinematic= false;

        GetComponent<Animator>().SetTrigger("die");
        GetComponent<Animator>().SetBool("isDead", true);

        Destroy(GetComponent<PlayerInteraction>());
        Destroy(GetComponent<PlayerFighter>());
        Destroy(GetComponent<MutantFighter>());
    }

    public bool GetDied()
    {
        return isDied;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
