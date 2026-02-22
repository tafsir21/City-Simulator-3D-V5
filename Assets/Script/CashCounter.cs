using System.Collections;
using UnityEngine;

public class CashCounter : PlayerTapDetection
{
    [SerializeField] private Animator CounterAnim;
    [SerializeField] private Animator PerticaleAnim;

    private Coroutine boostCoroutine;

    protected override void OnTap()
    {
        PerticaleAnim.SetTrigger("Action");
        CounterAnim.SetTrigger("Tapped");

        // restart the boost every tap
        if (boostCoroutine != null) StopCoroutine(boostCoroutine);
        boostCoroutine = StartCoroutine(IncomeBoost());
    }

IEnumerator IncomeBoost()
{
    int current = GameManager.instance.GetIncomePerSecond();
    int bonus   = current;

    GameManager.instance.RegisterIncome(bonus);

    yield return new WaitForSeconds(0.2f);

    GameManager.instance.UnregisterIncome(bonus);
    boostCoroutine = null;
}
}