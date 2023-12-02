using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public class QuesterInteraction : MonoBehaviour, Interactable
{
    public void Onclick(PlayerInteraction sender, Vector3 clickPos)
    {

    }

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

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
