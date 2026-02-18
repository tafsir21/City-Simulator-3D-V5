using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed = 0.035f;
    public Vector2 limitX = new Vector2(-200, 200);
    public Vector2 limitZ = new Vector2(-200, 200);

    private Vector3 lastTouchPosition;
    private bool isDragging;
    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        HandleMouse();
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 delta = Input.mousePosition - lastTouchPosition;
            MoveCamera(delta);
            lastTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
            isDragging = false;
    }

    void MoveCamera(Vector3 delta)
    {
        // Get camera's right and forward but flatten them to XZ plane
        Vector3 right = cam.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 move = (-delta.x * right + -delta.y * forward) * dragSpeed;

        transform.position += move;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, limitX.x, limitX.y);
        pos.z = Mathf.Clamp(pos.z, limitZ.x, limitZ.y);
        transform.position = pos;
    }
}