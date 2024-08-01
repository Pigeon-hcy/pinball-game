using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_interactLogic : MonoBehaviour
{
    public ShopManger ShopManger;
   
    private void Awake()
    {
        ShopManger = GameObject.FindGameObjectWithTag("shopManger").GetComponent<ShopManger>();
    }
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
            ShopManger.coins += 1;
        }

    }

 
}
