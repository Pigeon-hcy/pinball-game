using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BlackHole : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameObject EParticle;
    public GameManger GameManger;
    public MMF_Player OnHitFeedBack;
    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }

    public void InteractWith()
    {
        OnHitFeedBack.PlayFeedbacks();
        StartCoroutine(AddEmo());
    }

    IEnumerator AddEmo()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(EParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Emotion += 2;
    }
}
