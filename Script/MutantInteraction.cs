using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public class MutantInteraction : MonoBehaviour, Interactable
{
    private PlayerInteraction actualSender;
    public void Onclick(PlayerInteraction sender, Vector3 clickPos)
    {
        sender.GetComponent<PlayerFighter>().SetTarget(gameObject);
        actualSender = sender;
    }

    public void OnContinueClick(PlayerInteraction sender, Vector3 clickPos)
    {
        actualSender = sender;
    }

    public void OnRaycastStart(PlayerInteraction sender, Vector3 clickPos)
    {
        GetComponent<Outline>().enabled = true;
        actualSender = sender;
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

    private void OnDestroy()
    {
        if (actualSender != null)
        {
            actualSender.lastInt = null;
        }
    }
}
