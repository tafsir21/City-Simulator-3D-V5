using UnityEngine;

public class CashCounter : PlayerTapDetection
{
    [SerializeField] private Animator CounterAnim;
    [SerializeField] private Animator PerticaleAnim;

    protected override void OnTap()
    {
        PerticaleAnim.SetTrigger("Action");
        CounterAnim.SetTrigger("Tapped");
    }
}