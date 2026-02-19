using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MoveableObject npc_human_prefab;

    public void SpwanPeople()
    {
        MoveableObject people = Instantiate(npc_human_prefab);
        people.Spawn();        
    }
}
