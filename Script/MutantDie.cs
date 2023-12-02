using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Security.Cryptography;


public class MutantDie : MonoBehaviour
{
    private float destroytimer = 5f;
    public float lootTimer = 1f;
    private bool died = false;
    System.Random rand = new System.Random();
    public List<Loot> loots = new List<Loot>();

    public int expWorth = 5; 
    public int goldWorth =10;

    public GameObject hpBar; 

    public void Die()
    {
        GameObject.FindGameObjectWithTag("MobSpawner").GetComponent<MobSpawner>().IAmDied(int.Parse (gameObject.name));

        //exp ve altin
        GameObject player = GetComponent<MutantFighter>().lastHit;
        player.GetComponent<InventoryNew>().gold += goldWorth;
        player.GetComponent<PlayerStats>().AddExp(expWorth);

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic= false;
        GetComponent<Animator>().SetBool("died", true);
        GetComponent<EnemyFighter>().targetObj.GetComponent<PlayerFighter>().targetObj = null;
        Destroy(GetComponent<MutantInteraction>());
        Destroy(GetComponent<MutantMovement>());
        Destroy(GetComponent<EnemyFighter>());
        GetComponent<Outline>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>().lastInt = null;
        Destroy(hpBar);
        died= true;
    }
    private void Update()
    {
        if (died)
        {
            destroytimer -= Time.deltaTime;
            lootTimer -= Time.deltaTime;
            if(destroytimer <= 0)
            {
                destroytimer = 0;
                Destroy(gameObject);
            }
            if (lootTimer <= 0 && lootTimer >= -5)
            {
                lootTimer= -10;
                DropLoots();
            }
        }
    }

    private void DropLoots()
    {
        
        foreach(var loot in loots)
        {
            for (int i = 0; i < loot.maxAmount; i++)
            {
                int val = UnityEngine.Random.Range(0, 100);
                if (val > loot.probability)
                {
                    int layermask = 1 << 3;
                    Vector3 randPos = transform.position + transform.forward * UnityEngine.Random.Range(0, 3) + transform.right * UnityEngine.Random.Range(0, 3) + transform.up * 5;
                    GameObject newObj = Instantiate(loot.loot, randPos, Quaternion.identity);
                    newObj.GetComponent<Rigidbody>().isKinematic = false;
                    newObj.GetComponent<Collider>().enabled = true;
                    RaycastHit hit;
                    Physics.Raycast(newObj.transform.position, -transform.up, out hit, 100, layermask);
                    Vector3 colPoint = newObj.GetComponent<Collider>().ClosestPoint(hit.point);
                    float diff = colPoint.y - hit.point.y;
                    newObj.transform.position -= Vector3.up * diff;
                    //newObj.transform.position += Vector3.up * diff;



                }

            }
        }
    }

}

[Serializable] 
public class Loot
{
    public GameObject loot;
    public int probability;  //100 uzerinden
    public int maxAmount;
}
