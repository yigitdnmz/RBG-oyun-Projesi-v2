using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    public Interactable lastInt;
    public GameObject ClickedObj;
    // Start is called before the first frame update

    void LastIntEnd(Vector3 point)
    {
        if (lastInt != null)
        {
            if (lastInt.gameObject != null) 
            { 
            lastInt.OnRaycastEnd(this, point);
            }

            lastInt = null;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            //interactable fonksiyonlarindan objeleri null gelmiyosa cagirabilir 
            Interactable hitInt = hit.collider.gameObject.GetComponentInParent<Interactable>();
            if (hitInt != null)
            {
                if (lastInt != hitInt)
                {
                    hitInt.OnRaycastStart(this, hit.point);

                    LastIntEnd(hit.point);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    hitInt.Onclick(this, hit.point);
                    ClickedObj = hitInt.gameObject;
                }
                if (Input.GetMouseButton(0))
                {
                    if (ClickedObj == hitInt.gameObject)
                    {
                        hitInt.OnContinueClick(this, hit.point);
                    }
                }
                lastInt = hitInt;
            }
            else
            {
                if (lastInt != null) lastInt.OnRaycastEnd(this, hit.point);
                
            }
        }
        else
        {
            if (lastInt != null) lastInt.OnRaycastEnd(this, Vector3.zero);
        }
        
    }
}
