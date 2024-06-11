using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPannel : MonoBehaviour
{
    public GameObject winTitle;
    public GameObject loseTitle;
    public GameObject winButton;
    public GameObject loseButton;
    public GameManger gameManger;

    private void Awake()
    {
        gameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();

    }
    private void Update()
    {
        if (gameManger.Score >= gameManger.goal)
        {
            loseButton.SetActive(false);
            loseTitle.SetActive(false);
            winButton.SetActive(true);
            winTitle.SetActive(true);
        }
        else
        {
            loseButton.SetActive(true);
            loseTitle.SetActive(true);
            winButton.SetActive(false);
            winTitle.SetActive(false);
        }
    }
}
