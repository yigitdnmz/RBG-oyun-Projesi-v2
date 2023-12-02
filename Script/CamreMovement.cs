using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreMovement : MonoBehaviour
{
    //Yigit Donmez
    //mouse sag tusuyla hareket ettigimizde en fazla yorungesel olarak alinabilecek degerler. 
    float minX = -30;
    float maxX = 30;

    float curX = 0;


    // mouse orta tusuyla ileri geri kamera hareketi 
    float minDist = -30;
    float maxDist = -2;
    float curDist = -5;
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //mouse ile kamera hareket ettirme.
            curX -= Input.GetAxis("Mouse Y");
            curX = Mathf.Clamp(curX, minX, maxX);
            transform.parent.eulerAngles = new Vector3(curX,transform.parent.eulerAngles.y + Input.GetAxis("Mouse X") * 2,0);
        }
        //scroll ile ileri geri hareket etmeyi saglanmasi.
        curDist += Input.mouseScrollDelta.y/2;
        curDist = Mathf.Clamp(curDist, minDist, maxDist);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, curDist);

        transform.parent.position = player.transform.position - 2 * Vector3.up;
    }
}
