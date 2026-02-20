using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Humans")]
    public MoveableObject[] npc_human_prefabs;
    public MoveableObject_Network humanNetwork;

    [Header("Cars")]
    public MoveableObject[] npc_car_prefabs;
    public MoveableObject_Network carNetwork;

    private int lastHumanIndex = -1;
    private int lastCarIndex = -1;

    private void Start()
    {
        lastHumanIndex = -1;
        lastCarIndex = -1;
    }

    public void SpawnRandomObject()
    {
        bool spawnHuman = Random.value > 0.5f;

        if (spawnHuman)
            SpawnPeople();
        else
            SpawnCar();
    }

    public void SpawnPeople()
    {
        if (npc_human_prefabs == null || npc_human_prefabs.Length == 0)
        {
            Debug.LogWarning("No human prefabs assigned.");
            return;
        }

        int randomIndex = Random.Range(0, npc_human_prefabs.Length);

        // Prevent same prefab twice in a row
        if (npc_human_prefabs.Length > 1)
        {
            while (randomIndex == lastHumanIndex)
            {
                randomIndex = Random.Range(0, npc_human_prefabs.Length);
            }
        }

        MoveableObject people = Instantiate(npc_human_prefabs[randomIndex]);
        people.network = humanNetwork;
        people.Spawn();

        lastHumanIndex = randomIndex;
    }

    public void SpawnCar()
    {
        if (npc_car_prefabs == null || npc_car_prefabs.Length == 0)
        {
            Debug.LogWarning("No car prefabs assigned.");
            return;
        }

        int randomIndex = Random.Range(0, npc_car_prefabs.Length);

        // Prevent same prefab twice in a row
        if (npc_car_prefabs.Length > 1)
        {
            while (randomIndex == lastCarIndex)
            {
                randomIndex = Random.Range(0, npc_car_prefabs.Length);
            }
        }

        MoveableObject car = Instantiate(npc_car_prefabs[randomIndex]);
        car.network = carNetwork;
        car.Spawn();

        lastCarIndex = randomIndex;
    }
}