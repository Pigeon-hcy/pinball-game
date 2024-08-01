using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EmoBall : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameManger GameManger;
    public GameObject MParticle;
    public MMF_Player OnHitFeedBack;
    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }
    public void InteractWith()
    {
        StartCoroutine(AddEmo());
        OnHitFeedBack.PlayFeedbacks();
    }

    IEnumerator AddEmo()
    {
        for (int i = 0; i < 1; i++)
        {
            Instantiate(MParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Emotion += 1;
    }
}
