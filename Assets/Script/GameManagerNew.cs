using UnityEngine;
using TMPro;

public class GameManagerNew : MonoBehaviour
{
    public int money;
    public float BallCooldownMax;
    public int BallCooldownCost;
    public int BallAmountMax;
    public int BallAmountCost;
    public int BallValue;
    public int BallValueCost;
    public float pitMultiplier;
    public int pitMultiplierCost;

    public TMP_Text moneyText;

    private int _ballCooldownPurchases;
    private int _ballAmountPurchases;
    private int _ballValuePurchases;
    private int _pitMultiplierPurchases;

    private static int GetUpgradePrice(int baseCost, int purchaseCount)
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(1.2f, purchaseCount));
    }

    private void RefreshMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }

    public void UpgradeBallCooldown()
    {
        int cost = GetUpgradePrice(BallCooldownCost, _ballCooldownPurchases);
        if (money < cost) return;

        money -= cost;
        _ballCooldownPurchases++;
        BallCooldownMax = Mathf.Max(0f, BallCooldownMax * 0.9f);
        RefreshMoneyText();
    }

    public void UpgradeBallAmount()
    {
        int cost = GetUpgradePrice(BallAmountCost, _ballAmountPurchases);
        if (money < cost) return;

        money -= cost;
        _ballAmountPurchases++;
        BallAmountMax++;
        RefreshMoneyText();
    }

    public void UpgradeBallValue()
    {
        int cost = GetUpgradePrice(BallValueCost, _ballValuePurchases);
        if (money < cost) return;

        money -= cost;
        _ballValuePurchases++;
        BallValue++;
        RefreshMoneyText();
    }

    public void UpgradePitMultiplier()
    {
        int cost = GetUpgradePrice(pitMultiplierCost, _pitMultiplierPurchases);
        if (money < cost) return;

        money -= cost;
        _pitMultiplierPurchases++;
        pitMultiplier *= 1.1f;
        RefreshMoneyText();
    }

    void Update()
    {
        RefreshMoneyText();
    }
}
