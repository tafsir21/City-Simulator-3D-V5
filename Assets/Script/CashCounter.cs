using System.Collections;
using UnityEngine;

public class CashCounter : PlayerTapDetection
{
    [SerializeField] private Animator CounterAnim;
    [SerializeField] private Animator PerticaleAnim;
    [SerializeField] private float boostDuration = 0.5f;

    private Coroutine boostCoroutine;
    private bool isBoosting = false;
    private float boostEndTime;
    private int activeBonus = 0;

    protected override void OnTap()
    {
        PerticaleAnim.SetTrigger("Action");
        CounterAnim.SetTrigger("Tapped");

        boostEndTime = Time.time + boostDuration;

        if (!isBoosting)
        {
            isBoosting = true;

            GameManager.instance.SetIncomeMultiplier(GameManager.instance.incomeMultiplierValue);

            // bonus = only the extra portion, e.g. 1.2x means +20% of current income
            int current = GameManager.instance.GetIncomePerSecond();
            activeBonus = Mathf.RoundToInt(current * (GameManager.instance.incomeMultiplierValue - 1f));
            GameManager.instance.RegisterIncome(activeBonus);

            if (boostCoroutine != null) StopCoroutine(boostCoroutine);
            boostCoroutine = StartCoroutine(WaitForBoostEnd());
        }
    }

    IEnumerator WaitForBoostEnd()
    {
        while (Time.time < boostEndTime)
            yield return null;

        GameManager.instance.SetIncomeMultiplier(1f);
        GameManager.instance.UnregisterIncome(activeBonus);
        activeBonus    = 0;
        isBoosting     = false;
        boostCoroutine = null;
    }
}