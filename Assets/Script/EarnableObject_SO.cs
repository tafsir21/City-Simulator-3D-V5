using UnityEngine;

public enum EarnableObjectType { Static, Moveable }
public enum MoveableNetworkType { Human, Car }

[CreateAssetMenu(fileName = "EarnableObject_SO", menuName = "Scriptable Objects/EarnableObject_SO")]
public class EarnableObject_SO : ScriptableObject
{
    public string objectName;
    public Sprite icon;
    public int price;
    public int moneyPerSecond;
    public GameObject cashPrefab;
    public bool isTaxOfficeObject = false;

    public EarnableObjectType type;

    [Header("Variants")]
    public bool hasVariants = false;

    // For Static
    public StaticObject staticPrefab;
    public StaticObject[] staticVariants;

    // For Moveable
    public MoveableObject moveablePrefab;
    public MoveableObject[] moveableVariants;
    public MoveableNetworkType networkType; // Human or Car — Spawner resolves the actual network
}