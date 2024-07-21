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
    public GameManger GameManger;
    public GameObject MParticle;
    public TextMeshPro TMP;
    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }

    private void Update()
    {
        if (requiredBall == currentBall) 
        {
            currentBall = 0;
            turnEndMoney += 2;
            OnUpgradeFeedBack.PlayFeedbacks();
        }

        TMP.text = currentBall.ToString() + "/3";
    }

    public void InteractWith()
    {
        currentBall += 1;
        OnHitFeedBack.PlayFeedbacks();
    }

    void OnEnable() 
    {
        throwBall.OnTurnEnd += addMoney;
    }

    void OnDisable()
    { 
        throwBall.OnTurnEnd -= addMoney;
    }

    void addMoney()
    {
        OnTurnEndFeedBakck.PlayFeedbacks();
        StartCoroutine(AddEmo(turnEndMoney));
    }

    IEnumerator AddEmo(int turnEndMoney)
    {
        for (int i = 0; i < turnEndMoney; i++)
        {
            Instantiate(MParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Score += turnEndMoney;
    }
}
