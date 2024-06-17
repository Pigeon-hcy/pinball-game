using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePit_Shop : MonoBehaviour
{
    public ShopManger shopManger;
    public ScorePit scorePit;
    public TMP_Text Level;
    public TMP_Text Score;
    public TMP_Text Cost;
    public UnityEngine.UI.Button shopButton;

    private void Awake()
    {
        shopManger = GameObject.FindGameObjectWithTag("shopManger").GetComponent<ShopManger>();
    }
    // Update is called once per frame
    void Update()
    {
        if (shopManger.coins >= scorePit.Cost)
        {
            shopButton.interactable = true;
        } else
        {
            shopButton.interactable = false;
        }
         Level.text = "Level:" + scorePit.Level.ToString();
         Score.text = scorePit.Score.ToString() + "/" + scorePit.Emo.ToString();        
         Cost.text = "Cost: " + scorePit.Cost.ToString();
    }
}
