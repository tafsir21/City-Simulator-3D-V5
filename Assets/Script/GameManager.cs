using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int money = 0; // Private variable to track money

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
        }
    }

    public bool RemoveMoney(int amount)
    {
        if (amount > 0 && money >= amount)
        {
            money -= amount;
            return true; 
        }
        else
        {
            Debug.Log("no money");
            return false; 
        }
    }

    public int GetCurrentMoney()
    {
        return money;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}