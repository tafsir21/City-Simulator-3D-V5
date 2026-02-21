using UnityEngine;

[CreateAssetMenu(fileName = "EarnableObject_SO", menuName = "Scriptable Objects/EarnableObject_SO")]
public class EarnableObject_SO : ScriptableObject
{
    public string objectName;
    public Sprite icon;
    public int price;
    public int moneyPerSecond;

    public GameObject cashPrefab;  
}