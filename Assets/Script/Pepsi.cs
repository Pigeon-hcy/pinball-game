using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pepsi : MonoBehaviour, IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public int burstRequire;
    public int currentBurstRequire;
    public MMF_Player onHitFeedBack;
    public MMF_Player onBurstFeedBack;
    public GameObject MParticle;
    public GameObject Brust;
    public bool BurstisOn;
    public int BurstTime;
    public int currentBurstTime;
    public GameManger GameManger;
    public TMP_Text Tmp;

    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        currentBurstRequire = burstRequire;
    }

    public void InteractWith()
    {
        
        checkBrust();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tmp.text = currentBurstRequire + "/ 3";
        if (BurstisOn && currentBurstTime > 0) {
            
            currentBurstTime--;

        }


        if (BurstisOn && currentBurstTime == 0) {
            Brust.SetActive(false);
            BurstisOn = false;
        }
    }

    void checkBrust()
    {
        currentBurstRequire -= 1;
        if (currentBurstRequire == 0)
        {
            
            BurstisOn = true;
            Brust.SetActive(true);
            currentBurstTime = BurstTime;
            onBurstFeedBack.PlayFeedbacks();
            currentBurstRequire = burstRequire;
            StartCoroutine(AddEmo());
        }
        else
        {
            
            onHitFeedBack.PlayFeedbacks();
        }
    }


    IEnumerator AddEmo()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(MParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        GameManger.Emotion += 4;
    }
}
