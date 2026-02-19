using System.Collections.Generic;
using UnityEngine;

public class MoveableObject_Network : MonoBehaviour
{
    public List<Transform> gates = new List<Transform>();
    public List<MoveableObject_Route> routes = new List<MoveableObject_Route>();

    public MoveableObject_Route GetRandomRoute(Transform startGate)
    {
        List<MoveableObject_Route> possible = new List<MoveableObject_Route>();

        foreach (var r in routes)
        {
            if (r.startGate == startGate && r.endGate != startGate)
                possible.Add(r);
        }

        if (possible.Count == 0) return null;

        return possible[Random.Range(0, possible.Count)];
    }

    public Transform GetRandomGateExcept(Transform except)
    {
        List<Transform> possible = new List<Transform>();

        foreach (var g in gates)
        {
            if (g != except)
                possible.Add(g);
        }

        return possible[Random.Range(0, possible.Count)];
    }
}
