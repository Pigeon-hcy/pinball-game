using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScorePit : MonoBehaviour, IInteractable
{
    public ShopManger ShopManger;
    public GameManger GameManger;
    public int Score;
    public int Emo;
    public int Level;
    public int Cost;
    public bool canInteract => throw new System.NotImplementedException();
    public GameObject MParticle;
    public GameObject EParticle;

    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        ShopManger = GameObject.FindGameObjectWithTag("shopManger").GetComponent<ShopManger>();
    }

    public void InteractWith()
    {
        StartCoroutine(AddEmo());
        StartCoroutine(AddScore());

    }

    public void levelUp()
    {
        Level += 1;
        Cost += Level + 1;
        if (Score != 0)
        {
            Score += Convert.ToInt32(Score * 1.3f);
        }
        else if (Emo != 0)
        {
            Emo += Convert.ToInt32(Emo * 1.3f);
        }

    }

    public void buyUpgrade()
    {
        if (Cost <= ShopManger.coins)
        {
            ShopManger.coins -= Cost;
            levelUp();
            
            ShopManger.refreshCoin();
        }
        else
        {
            return;
        }
    }


    IEnumerator AddEmo()
    {
        for (int i = 0; i < Emo; i++)
        {
            Instantiate(EParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Emotion += Emo;
    }

    IEnumerator AddScore()
    {
        for (int i = 0; i < Score; i++)
        {
            Instantiate(MParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Score += Score;
    }
}
