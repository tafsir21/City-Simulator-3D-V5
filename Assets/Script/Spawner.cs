using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MoveableObject npc_human_prefab;
    public MoveableObject_Network humanNetwork;   // assign in Inspector


    public MoveableObject npc_car_prefab;
    public MoveableObject_Network carNetwork;   // assign in Inspector


    public void SpawnPeople()
    {
        MoveableObject people = Instantiate(npc_human_prefab);
        people.network = humanNetwork;   // override the prefab's network if needed
        people.Spawn();

        Debug.Log(people);
        Debug.Log(humanNetwork);
    }

    public void SpawnCar()
    {
        MoveableObject car = Instantiate(npc_car_prefab);
        car.network = carNetwork;   // override the prefab's network if needed
        car.Spawn();

    }
}