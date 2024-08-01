using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackBalls : MonoBehaviour, IInteractable
{
    public Rigidbody2D ballrb;
    public Transform ballT;

    public bool canInteract => throw new System.NotImplementedException();

    public void InteractWith()
    {
        
        change();
    }
    public void change()
    {
        ballT.position += new Vector3(Random.Range(-0.05f, 0.05f), 0.0f, Random.Range(-0.05f, 0.05f));
        ballrb.AddForce(new Vector2 (Random.Range(-0.25f,0.25f), Random.Range(-0.25f, 0.25f)),ForceMode2D.Impulse);
    }
}
