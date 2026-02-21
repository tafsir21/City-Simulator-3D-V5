using UnityEngine;

public class StaticObject : MonoBehaviour
{
    public Animator EffectAnim;
    public Animator BuildingAnim;


    public void PlayEffectAnim()
    {
        EffectAnim.SetTrigger("isAction");
    }

    public void PlayDropAnim()
    {
        BuildingAnim.SetTrigger("isDrop");
    }
}