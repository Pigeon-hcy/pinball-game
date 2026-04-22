using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Slime : MonoBehaviour,  IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public int turnEndMoney = 1;
    public int requiredBall = 3;
    public int currentBall = 0;
    public MMF_Player OnHitFeedBack;
    public MMF_Player OnUpgradeFeedBack;
    public MMF_Player OnTurnEndFeedBakck;
    public GameManagerNew GameManagerNew;
    public GameObject MParticle;
    public TextMeshPro TMP;

    public float turnLastTime = 10f;
    public float currentTurnLastTime = 0f;
    private void Awake()
    {
        GameManagerNew = FindFirstObjectByType<GameManagerNew>();
    }

    private void Update()
    {
        if (requiredBall <= currentBall) 
        {
            currentBall = 0;
            turnEndMoney += 2;
            OnUpgradeFeedBack.PlayFeedbacks();
        }

        currentTurnLastTime += Time.deltaTime;
        if (currentTurnLastTime >= turnLastTime)
        {
            currentTurnLastTime = 0f;
            addMoney();
        }

        TMP.text = currentBall.ToString() + "/3";
    }

    public void InteractWith()
    {
        currentBall += 1;
        OnHitFeedBack.PlayFeedbacks();
    }


    void addMoney()
    {
        OnTurnEndFeedBakck.PlayFeedbacks();
        GameManagerNew.money += turnEndMoney;
    }
}
