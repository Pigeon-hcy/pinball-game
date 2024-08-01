using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public Animator animator;
    public MMF_Player onHitFeedBack;
    public MMF_Player onGrowFeedBack;
    public int pizzaLeft;
    public CircleCollider2D Ccollider2D;

    public GameObject PizzaSlice;
    public Transform FirePlace;
    public Transform FirePlace2;
    public float force;

   
    public void InteractWith()
    {
        onHitFeedBack.PlayFeedbacks();
        
            pizzaLeft -= 1;
            firePizza();
            
        
        
    }

    
    

    // Update is called once per frame
    void Update()
    {

        if (pizzaLeft == 0)
        {
            Ccollider2D.enabled = false;
        }
        else
        {
            Ccollider2D.enabled = true;
            animator.SetInteger("PizzaLeft", pizzaLeft);
        }
        
    }

    void OnEnable()
    {
        throwBall.OnTurnEnd += growPizza;
    }

    void OnDisable()
    {
        throwBall.OnTurnEnd -= growPizza;
    }

    void growPizza()
    {
        onGrowFeedBack.PlayFeedbacks();
        pizzaLeft = 8;
    }


    void firePizza()
    {
        if (pizzaLeft >= 4)
        {
            FirePlace.rotation = Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(-10f, -30f));
            GameObject ball = Instantiate(PizzaSlice, FirePlace.position, FirePlace.rotation);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePlace.up * force, ForceMode2D.Impulse);
        }
        else {
            FirePlace2.rotation = Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(10f, 30f));
            GameObject ball = Instantiate(PizzaSlice, FirePlace2.position, FirePlace2.rotation);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePlace2.up * force, ForceMode2D.Impulse);
        }
        

    }
}
