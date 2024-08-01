using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopManger : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemsSO;
    public ShopTemp[] shopPannels;
    public GameObject[] item;
    public int[] prize;
    public bool[] alreadyBuy;
    public UnityEngine.UI.Button[] buyButton;
    public int refreshPrize;
    public GameObject point;
    
    private void Start()
    {
        
        coinUI.text = "Coins: " + coins.ToString();
        LoadPannels();
    }

    private void Update()
    {
        coinUI.text = "Coins: " + coins.ToString();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopPannels.Length; i++)
        {
            if (coins >= prize[i] && alreadyBuy[i] == false)
            {
                buyButton[i].interactable = true;
                Debug.Log("able");
            }
            else
            {
                buyButton[i].interactable = false;
                Debug.Log("disable");
            }
        }
    }

    public void refreshCoin()
    {
        coinUI.text = "Coins: " + coins.ToString();
        CheckPurchaseable();
    }

    public void AddCoins(int mount)
    {
        coins += mount;
        coinUI.text = "Coins: " + coins.ToString();
        CheckPurchaseable();
    }

    public void buyItem(int buttonNo)
    {
        if (coins >= prize[buttonNo])
        { 
            coins -= prize[buttonNo];
            coinUI.text = "Coins: " + coins.ToString();
            alreadyBuy[buttonNo] = true;
            CheckPurchaseable();
            Instantiate(item[buttonNo], new Vector3(-10,0,0), item[buttonNo].transform.rotation);
        }
    }

    public void Refresh()
    {
        if (coins >= refreshPrize)
        {
            coins -= refreshPrize;
            coinUI.text = "Coins: " + coins.ToString();
            CheckPurchaseable();
            LoadPannels();
            refreshPrize += 1;
        }
    }

    public void LoadPannels()
    {
        for (int i = 0; i < shopPannels.Length; i++)
        {
            
            int randomNum;
            randomNum = UnityEngine.Random.Range(0, shopItemsSO.Length);
            shopPannels[i].title.text = shopItemsSO[randomNum].title;
            shopPannels[i].effect.text = shopItemsSO[randomNum].description;
            shopPannels[i].cost.text = "Coins: " + shopItemsSO[randomNum].cost.ToString();
            prize[i] = shopItemsSO[randomNum].cost;
            item[i] = shopItemsSO[randomNum].prefab;
            alreadyBuy[i] = false;

        }
        CheckPurchaseable();
    }
}
