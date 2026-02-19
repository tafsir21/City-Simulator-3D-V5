using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MoveableObject_Network network;
    public MoveableObject prefab;

    public void SpwanPeople()
    {
        MoveableObject people = Instantiate(prefab);
        people.Spawn();        
    }
}
