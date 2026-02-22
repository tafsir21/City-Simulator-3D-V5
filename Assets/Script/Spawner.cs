using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header("Networks")]
    public MoveableObject_Network humanNetwork;
    public MoveableObject_Network carNetwork;

    private HashSet<string> spawnedStatics    = new HashSet<string>();
    private Dictionary<string, int> lastVariantIndex = new Dictionary<string, int>();

    public List<EarnableObject_SO> StaticObjects   => GameManager.instance.staticObjects;
    public List<EarnableObject_SO> MoveableObjects => GameManager.instance.moveableObjects;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Spawn(EarnableObject_SO so)
    {
        if (so.type == EarnableObjectType.Static) SpawnStatic(so);
        else                                       SpawnMoveable(so);
    }

    // ── STATIC ───────────────────────────────────────────────────────────────

    void SpawnStatic(EarnableObject_SO so)
    {
        if (spawnedStatics.Contains(so.objectName)) return;
        spawnedStatics.Add(so.objectName);

        StaticObject target = PickStatic(so);
        if (target == null) { Debug.LogError($"No static prefab found for {so.objectName}"); return; }

        StartCoroutine(SpawnWithDelay(target.transform, () =>
        {
            target.gameObject.SetActive(true);
            target.PlayEffectAnim();
            target.PlayDropAnim();
        }));
    }

    StaticObject PickStatic(EarnableObject_SO so)
    {
        if (so.hasVariants && so.staticVariants != null && so.staticVariants.Length > 0)
            return so.staticVariants[GetUniqueRandomIndex(so.staticVariants.Length, so.objectName)];

        return so.staticPrefab;
    }

    // ── MOVEABLE ─────────────────────────────────────────────────────────────

    void SpawnMoveable(EarnableObject_SO so)
    {
        MoveableObject prefab = PickMoveable(so);
        if (prefab == null) { Debug.LogError($"No moveable prefab found for {so.objectName}"); return; }

        MoveableObject_Network network = ResolveNetwork(so);
        if (network == null) { Debug.LogError($"No network resolved for {so.objectName}"); return; }

        MoveableObject obj = Instantiate(prefab);
        obj.network = network;
        obj.Spawn();
    }

    MoveableObject PickMoveable(EarnableObject_SO so)
    {
        if (so.hasVariants && so.moveableVariants != null && so.moveableVariants.Length > 0)
            return so.moveableVariants[GetUniqueRandomIndex(so.moveableVariants.Length, so.objectName)];

        return so.moveablePrefab;
    }

    MoveableObject_Network ResolveNetwork(EarnableObject_SO so)
    {
        return so.networkType == MoveableNetworkType.Human ? humanNetwork : carNetwork;
    }

    // ── HELPERS ──────────────────────────────────────────────────────────────

    int GetUniqueRandomIndex(int length, string key)
    {
        if (!lastVariantIndex.ContainsKey(key)) lastVariantIndex[key] = -1;
        if (length == 1) { lastVariantIndex[key] = 0; return 0; }

        int index;
        do { index = Random.Range(0, length); }
        while (index == lastVariantIndex[key]);

        lastVariantIndex[key] = index;
        return index;
    }

    IEnumerator SpawnWithDelay(Transform target, System.Action spawnAction)
    {
        CameraMovement.Instance.FocusOnTarget(target);
        yield return new WaitForSeconds(0.5f);
        spawnAction?.Invoke();
    }
}