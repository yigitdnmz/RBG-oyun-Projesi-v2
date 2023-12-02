using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface Interactable 
{
    GameObject gameObject { get; }
    void Onclick(PlayerInteraction sender, Vector3 clickPos);
    void OnRaycastStart(PlayerInteraction sender, Vector3 clickPos);
    void OnRaycastEnd(PlayerInteraction sender, Vector3 clickPos);
    void OnContinueClick(PlayerInteraction sender, Vector3 clickPos);
}
