using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class draganddrop : MonoBehaviour
{
    private Vector3 dragoffset;
    private Camera cam;
    public PhaseManger phaseManger;
    public bool canSell;
    private void Awake()
    {
        phaseManger = GameObject.FindGameObjectWithTag("PhaseManger").GetComponent<PhaseManger>();
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        if (phaseManger.canDrag == true)
        {
            dragoffset = transform.position - GetMousePos();
            
        }
        
    }

    private void OnMouseDrag()
    {
        if (phaseManger.canDrag == true)
        {
            transform.position = GetMousePos() + dragoffset;
            canSell = false;
        }
    }

    private void OnMouseUp()
    {
        if (phaseManger.canDrag == true)
        {
            
            canSell = true;
        }
    }

    
    Vector3 GetMousePos()
    {

        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
