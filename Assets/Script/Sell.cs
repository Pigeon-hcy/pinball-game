using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sell : MonoBehaviour
{
    public ShopManger ShopManger;

    private void Awake()
    {
        ShopManger = GameObject.FindGameObjectWithTag("shopManger").GetComponent<ShopManger>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item" && collision.GetComponent<draganddrop>().canSell == true)
        { 
            Destroy(collision.gameObject);
            ShopManger.coins += 2;
            ShopManger.refreshCoin();
        }
    }

   
}
