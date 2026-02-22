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

    [SerializeField] private int money = 0;
    [SerializeField] private int incomePerSecond = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (var obj in staticSceneObjects)
            if (obj != null) obj.gameObject.SetActive(false);
    }

    public StaticObject GetStaticSceneObject(EarnableObject_SO so)
    {
        int index = staticObjects.IndexOf(so);
        if (index < 0 || index >= staticSceneObjects.Count) return null;
        return staticSceneObjects[index];
    }

    public void RegisterIncome(int amount)
    {
        incomePerSecond += amount;
        UIManager.instance.UpdateIncomePerSecondUI(incomePerSecond);
    }

    public void UnregisterIncome(int amount)
    {
        incomePerSecond -= amount;
        UIManager.instance.UpdateIncomePerSecondUI(incomePerSecond);
    }

    public void AddMoney(int amount)
    {
        if (amount > 0)
        {
            money += amount;
            UIManager.instance.UpdateMoneyUI(money);
        }
    }

    public bool RemoveMoney(int amount)
    {
        if (amount > 0 && money >= amount)
        {
            money -= amount;
            UIManager.instance.UpdateMoneyUI(money);
            return true;
        }
        Debug.Log("Not enough money.");
        return false;
    }

    public int GetCurrentMoney()    => money;
    public int GetIncomePerSecond() => incomePerSecond;
}