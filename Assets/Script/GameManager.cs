// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform taxOfficeTransform;
    [SerializeField] private int money = 0;
    [SerializeField] private int incomePerSecond = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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

    public int GetCurrentMoney() => money;
    public int GetIncomePerSecond() => incomePerSecond;
}