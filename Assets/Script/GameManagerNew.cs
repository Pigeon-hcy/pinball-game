using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    // levels are 0-based and capped at 6
    public int ballLevel;
    public int pitLevel;

    public int ballMax = 6;
    public int PitMax = 6;

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

    public GameObject SlimeBlock;
    [Tooltip("Cost to enter slime placement mode (charged once per attempt).")]
    public int SlimeSelectCost;

    [Header("UI Buttons (optional)")]
    public Button upgradeBallCooldownButton;
    public Button upgradeBallAmountButton;
    public Button upgradeBallValueButton;
    public Button upgradePitButton;
    public Button placeGoldPitButton;
    public Button placePizzaButton;
    public Button placePepsiButton;
    public Button placeSlimeButton;

    private bool _goldPitSelectMode;
    private bool _pizzaSelectMode;
    private bool _pepsiSelectMode;
    private bool _slimeSelectMode;

    

    private void Awake()
    {
        if (GridSystem == null)
        {
            GridSystem = FindFirstObjectByType<GridSystem>();
        }
    }

    /// <summary>True if any paid selection mode is active (grid placement or pit upgrade).</summary>
    public bool IsAnySpecialBlockSelectMode =>
        _goldPitSelectMode || _pizzaSelectMode || _pepsiSelectMode || _slimeSelectMode;

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

    /// <summary>Spend money and enter placement mode: next valid grid click places <see cref="SlimeBlock"/>.</summary>
    public void TryEnterSlimeSelectMode()
    {
        if (IsAnySpecialBlockSelectMode) return;
        if (SlimeBlock == null || GridSystem == null) return;
        if (money < SlimeSelectCost) return;

        money -= SlimeSelectCost;
        _slimeSelectMode = true;
        RefreshMoneyText();
    }

    public bool IsGoldPitSelectMode => _goldPitSelectMode;
    public bool IsPizzaSelectMode => _pizzaSelectMode;
    public bool IsPepsiSelectMode => _pepsiSelectMode;
    public bool IsSlimeSelectMode => _slimeSelectMode;

    private void ClearSpecialPlacementModes()
    {
        _goldPitSelectMode = false;
        _pizzaSelectMode = false;
        _pepsiSelectMode = false;
        _slimeSelectMode = false;
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

    private void RefreshButtonsInteractable()
    {
        bool inSelectMode = IsAnySpecialBlockSelectMode;

        // ball upgrades
        SetButton(upgradeBallCooldownButton, !inSelectMode && ballLevel < 6 && money >= GetUpgradePrice(BallCooldownCost, _ballCooldownPurchases));
        SetButton(upgradeBallAmountButton, !inSelectMode && ballLevel < 6 && money >= GetUpgradePrice(BallAmountCost, _ballAmountPurchases));
        SetButton(upgradeBallValueButton, !inSelectMode && ballLevel < 6 && money >= GetUpgradePrice(BallValueCost, _ballValuePurchases));

        // pit upgrade
        SetButton(upgradePitButton, !inSelectMode && pitLevel < 6 && money >= GetUpgradePrice(pitMultiplierCost, _pitMultiplierPurchases));

        // placement buttons (flat cost)
        SetButton(placeGoldPitButton, !inSelectMode && GoldPit != null && GridSystem != null && money >= GoldPitSelectCost);
        SetButton(placePizzaButton, !inSelectMode && PizzaBlock != null && GridSystem != null && money >= PizzaSelectCost);
        SetButton(placePepsiButton, !inSelectMode && PepsiBlock != null && GridSystem != null && money >= PepsiSelectCost);
        SetButton(placeSlimeButton, !inSelectMode && SlimeBlock != null && GridSystem != null && money >= SlimeSelectCost);
    }

    private static void SetButton(Button button, bool interactable)
    {
        if (button == null) return;
        button.interactable = interactable;
    }

    public void UpgradeBallCooldown()
    {
        if (ballLevel >= 6) return;

        int cost = GetUpgradePrice(BallCooldownCost, _ballCooldownPurchases);
        if (money < cost) return;

        money -= cost;
        _ballCooldownPurchases++;
        BallCooldownMax = Mathf.Max(0f, BallCooldownMax * 0.9f);
        ballLevel = Mathf.Min(6, ballLevel + 1);
        RefreshMoneyText();
    }

    public void UpgradeBallAmount()
    {
        if (ballLevel >= 6) return;

        int cost = GetUpgradePrice(BallAmountCost, _ballAmountPurchases);
        if (money < cost) return;

        money -= cost;
        _ballAmountPurchases++;
        BallAmountMax++;
        ballLevel = Mathf.Min(6, ballLevel + 1);
        RefreshMoneyText();
    }

    public void UpgradeBallValue()
    {
        if (ballLevel >= 6) return;

        int cost = GetUpgradePrice(BallValueCost, _ballValuePurchases);
        if (money < cost) return;

        money -= cost;
        _ballValuePurchases++;
        BallValue++;
        ballLevel = Mathf.Min(6, ballLevel + 1);
        RefreshMoneyText();
    }

    // Upgrade ALL BallPits by 1 level (capped at 6).
    public void UpgradePitMultiplier()
    {
        if (pitLevel >= 6) return;

        int cost = GetUpgradePrice(pitMultiplierCost, _pitMultiplierPurchases);
        if (money < cost) return;

        money -= cost;
        _pitMultiplierPurchases++;

        foreach (BallPit pit in FindObjectsByType<BallPit>(FindObjectsSortMode.None))
        {
            pit.UpgradeLevel(); // BallPit itself caps at 6
        }

        pitLevel = Mathf.Min(6, pitLevel + 1);
        RefreshMoneyText();
    }

    void Update()
    {
        RefreshMoneyText();
        RefreshButtonsInteractable();

        if (!IsAnySpecialBlockSelectMode) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSpecialPlacementModes();
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        // Robust 2D mouse world position (works even if camera Z != 0)
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;
        Vector3 world = cam.ScreenToWorldPoint(mouse);
        world.z = 0f;

        // Grid placement modes (gold / pizza / pepsi)
        if (!GridSystem.TryWorldToCell(world, out int gx, out int gy)) return;

        // Bottom row (y == 0) is reserved for score pits.
        if (gy == 0) return;

        GameObject prefab = null;
        if (_goldPitSelectMode) prefab = GoldPit;
        else if (_pizzaSelectMode) prefab = PizzaBlock;
        else if (_pepsiSelectMode) prefab = PepsiBlock;
        else if (_slimeSelectMode) prefab = SlimeBlock;

        if (prefab == null)
        {
            ClearSpecialPlacementModes();
            return;
        }

        GridSystem.PlaceOrReplaceInCell(gx, gy, prefab);
        ClearSpecialPlacementModes();
    }
}
