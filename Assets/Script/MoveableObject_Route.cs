using System.Collections.Generic;
using UnityEngine;

public class MoveableObject_Route : MonoBehaviour
{
    public Transform startGate;
    public Transform endGate;
    public List<Transform> nodes = new List<Transform>();

    public Transform GetNode(int index)
    {
        return nodes[index];
    }

    public int NodeCount => nodes.Count;
}
