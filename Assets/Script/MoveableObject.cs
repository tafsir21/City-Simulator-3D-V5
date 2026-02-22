using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public float speed = 2f;
    public MoveableObject_Network network;

    private MoveableObject_Route currentRoute;
    private int nodeIndex;
    private Transform currentGate;

    public void Spawn()
    {
        if (network == null)                                   { Debug.LogError("No network: " + gameObject.name); return; }
        if (network.gates == null || network.gates.Count == 0) { Debug.LogError("No gates: "   + gameObject.name); return; }

        currentGate = network.gates[Random.Range(0, network.gates.Count)];
        if (currentGate == null) return;

        transform.position = currentGate.position;
        ChooseNewRoute();
    }

    void Update()
    {
        if (currentRoute == null) return;

        Transform target = currentRoute.GetNode(nodeIndex);
        if (target == null) return;

        Vector3 targetPos  = target.position;
        Vector3 currentPos = transform.position;

        // how far we'd travel this frame
        float stepSize = speed * Time.deltaTime;
        float distance = Vector3.Distance(currentPos, targetPos);

        if (distance <= stepSize)
        {
            // snap exactly to node, no overshoot
            transform.position = targetPos;
            nodeIndex++;

            if (nodeIndex >= currentRoute.NodeCount)
            {
                Respawn();
                return;
            }

            return; // next node starts next frame
        }

        // safe normalized direction
        Vector3 moveDir = (targetPos - currentPos) / distance; // manual normalize, avoids near-zero issues
        transform.position = currentPos + moveDir * stepSize;

        // rotation — horizontal only
        Vector3 lookDir = new Vector3(moveDir.x, 0f, moveDir.z);
        if (lookDir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lookDir),
                10f * Time.deltaTime
            );
        }
    }

    void Respawn()
    {
        if (currentRoute == null || currentRoute.endGate == null) return;

        currentGate = currentRoute.endGate;
        transform.position = currentGate.position;
        ChooseNewRoute();

        if (currentRoute == null)
        {
            currentGate = network.GetRandomGateExcept(currentGate);
            if (currentGate == null) return;
            transform.position = currentGate.position;
            ChooseNewRoute();
        }
    }

    void ChooseNewRoute()
    {
        currentRoute = network.GetRandomRoute(currentGate);
        if (currentRoute == null) return;
        nodeIndex = 0;
    }
}