using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEater : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameObject ball;
    public GameObject pointA;
    public GameObject pointB;
    public void InteractWith()
    {
        Debug.Log("spawn!");
        Instantiate(ball, pointA.transform.position, Quaternion.identity);
        Instantiate(ball, pointB.transform.position, Quaternion.identity);
    }
}
