using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public int Emotion;
    public int startEmotion;
    public int Score;
    public TMP_Text goalText;
    public TMP_Text EmoText;
    public TMP_Text turnText;
    public int goal;
    public int tryTime;
    public int tryTimeMax;
    public bool ballExist;
    public bool noNearBlock;
    public int round;
    
    public GameObject[] ballList;

    private void Start()
    {
        tryTime = tryTimeMax;
        Emotion = startEmotion;
    }

    private void Update()
    {
        goalText.text = Score + "/" + goal;
        EmoText.text = Emotion.ToString();
        turnText.text = tryTime.ToString();

        ballList = GameObject.FindGameObjectsWithTag("Ball");

        if (ballList.Length == 0)
        {
            ballExist = false;
        }
        else if((ballList.Length != 0))
        { 
            ballExist = true;
        }
    }

    public void newGame()
    { 
        round = 0;
        Score = 0;
        Emotion = startEmotion;
        tryTime = tryTimeMax;
        goal = 10;
    }
    public void newRound()
    {
        round += 1;
        Score = 0;
        Emotion = startEmotion;
        tryTime = tryTimeMax;
        goal += 5 * round;
    }
}
