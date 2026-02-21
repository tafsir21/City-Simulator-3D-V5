using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform taxOfficeTransform;
    [SerializeField] private int money = 0; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddMoney(int amount)
    {
        if (amount > 0)
        {
            money += amount;
            UIManager.instance.UpdateMoneyUI(money); // Update the UI whenever money is added
        }
    }

    public bool RemoveMoney(int amount)
    {
        if (amount > 0 && money >= amount)
        {
            money -= amount;
            UIManager.instance.UpdateMoneyUI(money); // Update the UI whenever money is removed
            return true;
        }
        else
        {
            Debug.Log("Not enough money.");
            return false;
        }
    }

    public int GetCurrentMoney()
    {
        return money;
    }
}