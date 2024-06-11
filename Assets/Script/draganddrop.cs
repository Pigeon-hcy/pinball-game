using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draganddrop : MonoBehaviour
{
    private Vector3 dragoffset;
    private Camera cam;
    public PhaseManger phaseManger;

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
        }
    }

    Vector3 GetMousePos()
    {

        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
