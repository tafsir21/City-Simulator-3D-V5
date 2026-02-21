using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    void Awake()
    {
        // Cache camera transform once (important for performance)
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Face the camera
        transform.LookAt(transform.position + cam.forward);
    }
}