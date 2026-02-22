using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform taxOfficeTransform;

    [Header("Earnable Object Lists")]
    public List<EarnableObject_SO> staticObjects;
    public List<EarnableObject_SO> moveableObjects;

    [Header("Static Scene Objects (same order as staticObjects list)")]
    public List<StaticObject> staticSceneObjects;

    [SerializeField] private int startingMoney = 200;
    [SerializeField] private int money = 0;
    [SerializeField] private int incomePerSecond = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        money = 0;
        incomePerSecond = 0; // reset serialized value

        foreach (var so in staticObjects)   if (so != null) so.spawnCount = 0;
        foreach (var so in moveableObjects) if (so != null) so.spawnCount = 0;

        foreach (var obj in staticSceneObjects)
            if (obj != null) obj.gameObject.SetActive(false);

        money = startingMoney;
    }

    private void Start()
    {
        UIManager.instance.UpdateMoneyUI(money);
        UIManager.instance.UpdateIncomePerSecondUI(incomePerSecond);
    }

    public StaticObject GetStaticSceneObject(EarnableObject_SO so)
    {
        int index = staticObjects.IndexOf(so);
        if (index < 0 || index >= staticSceneObjects.Count) return null;
        return staticSceneObjects[index];
    }

    public bool CanAfford(EarnableObject_SO so) => money >= so.CurrentPrice;

    public void RegisterIncome(int amount)
    {
        incomePerSecond += amount;
        Debug.Log($"RegisterIncome | amount: {amount} | total: {incomePerSecond}\n{System.Environment.StackTrace}", this);
        UIManager.instance.UpdateIncomePerSecondUI(incomePerSecond);
    }
    public void UnregisterIncome(int amount)
    {
        incomePerSecond -= amount;
        UIManager.instance.UpdateIncomePerSecondUI(incomePerSecond);
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0) return;
        money += amount;
        UIManager.instance.UpdateMoneyUI(money);
    }

    public bool RemoveMoney(int amount)
    {
        if (amount <= 0 || money < amount) return false;
        money -= amount;
        UIManager.instance.UpdateMoneyUI(money);
        return true;
    }

    public int GetCurrentMoney()    => money;
    public int GetIncomePerSecond() => incomePerSecond;
}