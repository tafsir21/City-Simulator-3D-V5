using UnityEngine;

public class PedestrianAgent : MonoBehaviour
{
    public float speed = 2f;

    private PedestrianRoute currentRoute;
    private int nodeIndex;
    private PedestrianNetwork network;
    private Transform currentGate;

    public void Spawn()
    {
        if (network == null)
            network = FindObjectOfType<PedestrianNetwork>();

        currentGate = network.gates[Random.Range(0, network.gates.Count)];

        transform.position = currentGate.position;

        ChooseNewRoute();
    }

    void Update()
    {
        if (currentRoute == null) return;

        Transform target = currentRoute.GetNode(nodeIndex);

        Vector3 targetPos = target.position;
        Vector3 toTarget = targetPos - transform.position;

        // -------- HORIZONTAL DISTANCE CHECK --------
        Vector2 flat = new Vector2(toTarget.x, toTarget.z);

        if (flat.sqrMagnitude < 0.04f)   // arrived horizontally
        {
            // snap exactly to node height
            transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);

            nodeIndex++;

            if (nodeIndex >= currentRoute.NodeCount)
            {
                Respawn();
                return;
            }

            target = currentRoute.GetNode(nodeIndex);
            toTarget = target.position - transform.position;
        }

        // -------- MOVEMENT --------
        Vector3 moveDir = toTarget.normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        // -------- ROTATION (no tilting) --------
        Vector3 lookDir = new Vector3(moveDir.x, 0f, moveDir.z);

        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }
    }

    void Respawn()
    {
        Transform newGate = network.GetRandomGateExcept(currentGate);

        currentGate = newGate;
        transform.position = currentGate.position;

        ChooseNewRoute();
    }

    void ChooseNewRoute()
    {
        currentRoute = network.GetRandomRoute(currentGate);
        if (currentRoute == null) return;

        nodeIndex = 0;
    }
}
