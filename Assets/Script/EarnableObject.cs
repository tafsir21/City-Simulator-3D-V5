using UnityEngine;

public class EarnableObject : MonoBehaviour
{
    public EarnableObject_SO earnableObjectSO;  // ScriptableObject reference for money per second
    private float timer = 0f;                   // Timer to track time
    private bool isEarning = true;              // Flag to control if we are earning money

    void Update()
    {
        if (isEarning)
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f)   // Wait for 1 sec
            {
                EarnMoney();
                timer -= 1.5f;    // Reset timer
            }
        }
    }

    void EarnMoney()
    {
        // Ensure the earnableObjectSO exists and has valid data
        if (earnableObjectSO != null)
        {
            // Add money to the GameManager
            GameManager.instance.AddMoney(earnableObjectSO.moneyPerSecond);
        }
    }
}