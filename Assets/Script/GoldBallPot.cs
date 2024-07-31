using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GoldBallPot : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameObject GoldBall;
    public Transform FirePlace;
    public float force;
    public int ballNeed;
    public int currentBall;
    public MMF_Player onHitFeedBack;
    public MMF_Player onBrustFeedBack;
    public TextMeshPro TMP;

    private void Awake()
    {
        currentBall = 0;
    }

    private void Update()
    {
        TMP.text = currentBall.ToString() + "/" + "2";
    }

    public void InteractWith()
    {
        currentBall += 1;
        onHitFeedBack.PlayFeedbacks();
        if (currentBall == ballNeed) 
        {
            fireGoldBall();
            currentBall = 0;
        }
    }

    void fireGoldBall()
    {
        onBrustFeedBack.PlayFeedbacks();
        FirePlace.rotation = Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(-30f, 30f));
        GameObject ball = Instantiate(GoldBall, FirePlace.position, FirePlace.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePlace.up * force, ForceMode2D.Impulse);
    }


}
