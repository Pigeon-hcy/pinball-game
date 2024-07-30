using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.ParticleSystem;

public class PiiggyBank : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public int pointword = 25;
    public int duriablity = 10;
    public float currentDuriablit;
    public TextMeshPro TMP;
    public MMF_Player onHitFeedBack;
    public MMF_Player onBreakFeedBack;
    public GameManger GameManger;

    private void Awake()
    {
        currentDuriablit = duriablity;
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }
    public void InteractWith()
    {
        currentDuriablit -= 1;
        onHitFeedBack.PlayFeedbacks();
        if (currentDuriablit == 0)
        {
            StartCoroutine(AddEmoExplode());
           
        }
    }

    public void Update()
    {
        TMP.text = currentDuriablit.ToString() + "/" + "10";
    }

    IEnumerator AddEmoExplode()
    {
        onBreakFeedBack.PlayFeedbacks();
        yield return new WaitForSeconds(1f);
        GameManger.Score += pointword;
        Destroy(this.gameObject);
    }
}
