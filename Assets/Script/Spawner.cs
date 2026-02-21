using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Networks")]
    public MoveableObject_Network humanNetwork;
    public MoveableObject_Network carNetwork;

    [Header("Static Prefabs")]
    public StaticObject taxOfficeV2;

    [Header("Moveable Prefabs")]
    public MoveableObject[] npc_human_prefabs;
    public MoveableObject[] npc_car_prefabs;
    public MoveableObject[] npc_truck_prefabs;

    private int lastHumanIndex = -1;
    private int lastCarIndex = -1;
    private int lastTruckIndex = -1;

    public void SpawnRandomObject()
    {
        if (Random.value > 0.5f)
            SpawnPeople();
        else
            SpawnTaxi();
    }

    public void SpawnPeople()  => SpawnFrom(npc_human_prefabs, humanNetwork, ref lastHumanIndex, "human");
    public void SpawnTaxi()    => SpawnFrom(npc_car_prefabs,   carNetwork,   ref lastCarIndex,   "car");
    public void SpawnTruck()   => SpawnFrom(npc_truck_prefabs, carNetwork,   ref lastTruckIndex, "truck");



    public void SpwanTaxOfficeV2()
    {
        if (!taxOfficeV2.gameObject.activeSelf)
        {
            taxOfficeV2.gameObject.SetActive(true);
            taxOfficeV2.PlayEffectAnim();        
            taxOfficeV2.PlayDropAnim();        
        }
    }

    private void SpawnFrom(MoveableObject[] prefabs, MoveableObject_Network network, ref int lastIndex, string label)
    {
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogWarning($"No {label} prefabs assigned.");
            return;
        }

        int index = GetUniqueRandomIndex(prefabs.Length, lastIndex);
        lastIndex = index;

        MoveableObject obj = Instantiate(prefabs[index]);
        obj.network = network;
        obj.Spawn();
    }

    private int GetUniqueRandomIndex(int length, int lastIndex)
    {
        if (length == 1) return 0;

        int index;
        do { index = Random.Range(0, length); }
        while (index == lastIndex);
        return index;
    }
}