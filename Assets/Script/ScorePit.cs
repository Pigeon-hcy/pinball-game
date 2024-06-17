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

    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        ShopManger = GameObject.FindGameObjectWithTag("shopManger").GetComponent<ShopManger>();
    }

    public void InteractWith()
    {
        GameManger.Emotion += Emo;
        GameManger.Score += Score;

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
    //public void Update()
    //{
        

    //}
}
