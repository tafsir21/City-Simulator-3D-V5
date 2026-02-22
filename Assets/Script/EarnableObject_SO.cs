using UnityEngine;

public enum EarnableObjectType { Static, Moveable }
public enum MoveableNetworkType { Human, Car }

[CreateAssetMenu(fileName = "EarnableObject_SO", menuName = "Scriptable Objects/EarnableObject_SO")]
public class EarnableObject_SO : ScriptableObject
{
    public string objectName;
    public Sprite icon;
    public int basePrice;       // original price, never changes
    public int moneyPerSecond;
    public GameObject cashPrefab;
    public bool isTaxOfficeObject = false;

    public EarnableObjectType type;

    [Header("Moveable")]
    public bool hasVariants = false;
    public MoveableObject moveablePrefab;
    public MoveableObject[] moveableVariants;
    public MoveableNetworkType networkType;

    // ── RUNTIME ──────────────────────────────────────────────────────────────
    [HideInInspector] public int spawnCount = 0; // how many have been spawned

    public int CurrentPrice => type == EarnableObjectType.Static
        ? basePrice
        : Mathf.RoundToInt(basePrice * Mathf.Pow(2, spawnCount)); // 3x per spawn
}