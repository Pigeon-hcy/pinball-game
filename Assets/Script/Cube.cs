using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameManger GameManger;
    public AudioSource AudioSource;
    public MMF_Player OnHitFeedBack;

    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }

    public void InteractWith()
    {
        AudioSource.Play();
        OnHitFeedBack.PlayFeedbacks();
        GameManger.Emotion += 1;
    }

    
}
