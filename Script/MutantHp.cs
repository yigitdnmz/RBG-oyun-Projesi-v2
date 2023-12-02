using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHp : MonoBehaviour
{
    public int maxCan = 100;
    private int currentCan;

    void Start()
    {
        currentCan = maxCan;
    }

    public void HasarVer(int hasarMiktari)
    {
        currentCan -= hasarMiktari; 

        if (currentCan <= 0)
        {
            Olum(); 
        }
    }

    private void Olum()
    {

        Debug.Log("Mutant öldü!");

    }
}