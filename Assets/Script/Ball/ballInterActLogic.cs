using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballInterActLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().InteractWith();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            collision.gameObject.GetComponent<IInteractable>().InteractWith();
        }
    }
}
