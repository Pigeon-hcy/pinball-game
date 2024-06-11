using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePit : MonoBehaviour, IInteractable
{
    public GameManger GameManger;
    public int Score;
    public int Emo;
    public bool canInteract => throw new System.NotImplementedException();

    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }

    public void InteractWith()
    {
        GameManger.Emotion += Emo;
        GameManger.Score += Score;
        
    }

    
}
