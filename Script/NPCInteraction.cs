using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class NPCInteraction : MonoBehaviour, Interactable
{
    public float etkinlesmeMesafe = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick(PlayerInteraction sender, Vector3 clickPos)
    {
        if (Vector3.Distance(sender.transform.position, transform.position) > etkinlesmeMesafe)
        {
            sender.GetComponent<PlayerMovement>().walk(transform.position);
        }
        else
        {
            OnInteract();

        }

    }

    public abstract void OnInteract();

    public void OnContinueClick(PlayerInteraction sender, Vector3 clickPos)
    {

    }

    public void OnRaycastStart(PlayerInteraction sender, Vector3 clickPos)
    {
        GetComponent<Outline>().enabled = true;

    }


    public void OnRaycastEnd(PlayerInteraction sender, Vector3 clickPos)
    {
        GetComponent<Outline>().enabled = false;

    }
}
