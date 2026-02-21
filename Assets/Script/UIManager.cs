using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI moneyText; 
    public TextMeshProUGUI perSecond_incomeText; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void UpdateMoneyUI(int money)
    {
        moneyText.text = "$"+money.ToString();
    }
    public void UpdateIncomePerSecondUI(int money)
    {
        perSecond_incomeText.text = "$"+money.ToString() + " /SEC";
    }
}