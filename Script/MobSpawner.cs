using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Rendering;

public class MobSpawner : MonoBehaviour
{
    private System.Random rnd;
    public List<MobSpawnType> mobsToSpawn = new List<MobSpawnType>();
    public int[] spawnedMobs;

    private GameObject terrain;
    // Start is called before the first frame update
    void Start()
    {

        spawnedMobs = new int[mobsToSpawn.Count]; 

        terrain = GameObject.FindGameObjectWithTag("Terrain");
        rnd = new System.Random();

        foreach (var mob in mobsToSpawn)
        {
            mob.curCooldown = mob.coolDown;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<mobsToSpawn.Count; i++)
        {
            MobSpawnType mob = mobsToSpawn[i];
            mob.curCooldown -= Time.deltaTime;
            if(mob.curCooldown <= 0)
            {
                if (spawnedMobs[i] >= mob.maxAmount) continue;
                Vector2 posRelativeOrigin = UnityEngine.Random.insideUnitCircle;
                posRelativeOrigin *= mob.circleRadius;
                posRelativeOrigin += mob.circleCenter;
                Vector3 actualPos = new Vector3(posRelativeOrigin.x, 40, posRelativeOrigin.y);
                RaycastHit hit;
                Physics.Raycast(actualPos, -Vector3.up, out hit);
                Vector3 spawnPoint = hit.point;
                spawnPoint += Vector3.up * mob.offsetY;
                GameObject newMob = Instantiate(mob.mob, spawnPoint, Quaternion.identity);
                newMob.gameObject.name = i.ToString();
                spawnedMobs[i] += 1;
                mob.curCooldown = mob.coolDown;
            }
        }
    }
    public void IAmDied(int idx)
    {
        spawnedMobs[idx] -= 1; 
            
    }
}
[System.Serializable]
public class MobSpawnType
{
    public int maxAmount = 30;
    public float coolDown =10;

    [HideInInspector]
    public float curCooldown;
    public GameObject mob;
    public Vector2 circleCenter = new Vector2 (126, 337);
    public float circleRadius= 20;
    public float offsetY = 0; 

}
