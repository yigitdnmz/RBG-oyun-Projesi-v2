using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInteraction : MonoBehaviour , Interactable
{
    

    public void Onclick(PlayerInteraction sender, Vector3 clickPos)
    {
        sender.GetComponent<PlayerMovement>().walk(clickPos);
    }

    public void OnContinueClick(PlayerInteraction sender, Vector3 clickPos)
    {
        sender.GetComponent<PlayerMovement>().walk(clickPos);
    }

    public void OnRaycastEnd(PlayerInteraction sender, Vector3 clickPos)
    {
        
    }

    public void OnRaycastStart(PlayerInteraction sender, Vector3 clickPos)
    {
        
    }

}
