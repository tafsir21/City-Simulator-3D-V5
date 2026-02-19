using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PedestrianNetwork network;
    public PedestrianAgent prefab;

    public void SpwanPeople()
    {
        PedestrianAgent people = Instantiate(prefab);
        people.Spawn();        
    }
}
