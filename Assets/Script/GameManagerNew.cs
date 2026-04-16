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

    public GridSystem GridSystem;

    [Header("Special block placement (grid)")]
    public GameObject GoldPit;
    [Tooltip("Cost to enter gold-pit placement mode (charged once per attempt).")]
    public int GoldPitSelectCost;

    public GameObject PizzaBlock;
    [Tooltip("Cost to enter pizza-block placement mode (charged once per attempt).")]
    public int PizzaSelectCost;

    public GameObject PepsiBlock;
    [Tooltip("Cost to enter pepsi-block placement mode (charged once per attempt).")]
    public int PepsiSelectCost;

    private bool _goldPitSelectMode;
    private bool _pizzaSelectMode;
    private bool _pepsiSelectMode;

    private void Awake()
    {
        if (GridSystem == null)
        {
            GridSystem = FindFirstObjectByType<GridSystem>();
        }
    }

    /// <summary>True if any paid grid-placement mode is active (gold / pizza / pepsi).</summary>
    public bool IsAnySpecialBlockSelectMode =>
        _goldPitSelectMode || _pizzaSelectMode || _pepsiSelectMode;

    /// <summary>Spend money and enter placement mode: next valid grid click places <see cref="GoldPit"/>.</summary>
    public void TryEnterGoldPitSelectMode()
    {
        if (IsAnySpecialBlockSelectMode) return;
        if (GoldPit == null || GridSystem == null) return;
        if (money < GoldPitSelectCost) return;

        money -= GoldPitSelectCost;
        _goldPitSelectMode = true;
        RefreshMoneyText();
    }

    /// <summary>Spend money and enter placement mode: next valid grid click places <see cref="PizzaBlock"/>.</summary>
    public void TryEnterPizzaSelectMode()
    {
        if (IsAnySpecialBlockSelectMode) return;
        if (PizzaBlock == null || GridSystem == null) return;
        if (money < PizzaSelectCost) return;

        money -= PizzaSelectCost;
        _pizzaSelectMode = true;
        RefreshMoneyText();
    }

    /// <summary>Spend money and enter placement mode: next valid grid click places <see cref="PepsiBlock"/>.</summary>
    public void TryEnterPepsiSelectMode()
    {
        if (IsAnySpecialBlockSelectMode) return;
        if (PepsiBlock == null || GridSystem == null) return;
        if (money < PepsiSelectCost) return;

        money -= PepsiSelectCost;
        _pepsiSelectMode = true;
        RefreshMoneyText();
    }

    public bool IsGoldPitSelectMode => _goldPitSelectMode;
    public bool IsPizzaSelectMode => _pizzaSelectMode;
    public bool IsPepsiSelectMode => _pepsiSelectMode;

    private void ClearSpecialPlacementModes()
    {
        _goldPitSelectMode = false;
        _pizzaSelectMode = false;
        _pepsiSelectMode = false;
    }

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

        if (!IsAnySpecialBlockSelectMode) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSpecialPlacementModes();
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0f;

        if (!GridSystem.TryWorldToCell(world, out int gx, out int gy)) return;

        // Bottom row (y == 0) is reserved for score pits.
        if (gy == 0) return;

        GameObject prefab = null;
        if (_goldPitSelectMode) prefab = GoldPit;
        else if (_pizzaSelectMode) prefab = PizzaBlock;
        else if (_pepsiSelectMode) prefab = PepsiBlock;

        if (prefab == null)
        {
            ClearSpecialPlacementModes();
            return;
        }

        GridSystem.PlaceOrReplaceInCell(gx, gy, prefab);
        ClearSpecialPlacementModes();
    }
}
