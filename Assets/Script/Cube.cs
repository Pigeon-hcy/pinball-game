using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Cube : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameManger GameManger;
    public AudioSource AudioSource;
    public MMF_Player OnHitFeedBack;
    public int cooldown = 10;
    public int cooldownRemaining;
    public bool canIntercat;
    public GameObject EParticle;
    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }

    public void InteractWith()
    {
        
            AudioSource.Play();
            OnHitFeedBack.PlayFeedbacks();
            
            
        if (canIntercat)
        {
            canIntercat = false;
            cooldownRemaining = cooldown;
            StartCoroutine(AddEmo());
        }
        
    }

    private void Update()
    {
        if (!canIntercat) 
        {
            cooldownRemaining--;
        }

        if (cooldownRemaining < 0 && !canIntercat) 
        {
            canIntercat = true;
        }
    }


    IEnumerator AddEmo()
    {
        for (int i = 0; i < 1; i++)
        {
            Instantiate(EParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Emotion += 1;
    }
}
