using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManger : MonoBehaviour
{
    public bool turnStart;
    public bool turnEnd;
    public bool roundOver;
    public bool shop;
    public bool alreadychangeToPhase3;
    public GameManger manger;
    public throwBall thrower;
    public GameObject roundOverPannel;
    public GameObject shopPannel;
    public bool canDrag;
    public ShopManger shopManger;
    private void Awake()
    {
        thrower = GameObject.FindGameObjectWithTag("Thrower").GetComponent<throwBall>();
        manger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }

    private void Update()
    {
        if (thrower.throwDone == true && manger.ballExist == false && manger.tryTime != 0)
        {
            changePhase(1);
        }

        if (manger.tryTime == 0 && manger.ballExist == false && thrower.throwDone == true && alreadychangeToPhase3 == false)
        {
            alreadychangeToPhase3 = true;
            changePhase(3);
            roundOverPannel.SetActive(true);
        }

        if (shop)
        {
            roundOverPannel.SetActive(false);
            shopPannel.SetActive(true);
        }


    }

    public void changePhase(int index)
    {
        if (index == 1)
        {
            turnStart = true;
            turnEnd = false;
            roundOver = false;
            shop = false;
            canDrag = true;
        }

        else if (index == 2)
        {
            turnStart = false;
            turnEnd = true;
            roundOver = false;
            shop = false;
           
            canDrag = false;
        }
        else if (index == 3)
        {
            turnStart = false;
            turnEnd = false;
            roundOver = true;
            shop = false;
            canDrag = false;
        }
        else if (index == 4)
        {
            turnStart = false;
            turnEnd = false;
            roundOver = false;
            shop = true;
            canDrag = true;
        }
        else
        {

            return;

        }

    }

    public void switchToShop() 
    {
        shopManger.AddCoins(5);
        //debug

        Debug.Log("changetoshop");
        changePhase(4);
        
    }

    public void switchToNextRound()
    {
        alreadychangeToPhase3 = false ;
        shopPannel.SetActive(false);
        manger.newRound();
        changePhase(1);
    }

    public void restart()
    {
        //alreadychangeToPhase3 = false;
        //roundOverPannel.SetActive(false);
        //manger.newGame();
        //changePhase(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

