using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BlackHole : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public GameObject EParticle;
    public GameManger GameManger;
    public MMF_Player OnHitFeedBack;
    public GameObject BlackHoleForceField;
    public PhaseManger PhaseManger;
    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        PhaseManger = GameObject.FindGameObjectWithTag("PhaseManger").GetComponent <PhaseManger>();
    }

    public void InteractWith()
    {
        OnHitFeedBack.PlayFeedbacks();
        StartCoroutine(AddEmo());
    }

    public void Update()
    {
        if (PhaseManger.turnEnd == true)
        {
            BlackHoleForceField.SetActive(true);
        }
        else { 
            BlackHoleForceField.SetActive(false);
        }
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
