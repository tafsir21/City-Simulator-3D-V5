using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;

    [Header("Drag Settings")]
    public float dragSpeed = 0.035f;
    public Vector2 limitX = new Vector2(-200, 200);
    public Vector2 limitZ = new Vector2(-200, 200);

    [Header("Focus Settings")]
    public float focusSmoothTime = 0.3f;
    public float focusForwardOffset = 5f; // adjust in inspector

    private Vector3 lastTouchPosition;
    private bool isDragging;
    private Camera cam;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private bool isFocusing = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (!isFocusing)
            HandleMouse();
        else
            SmoothMoveToTarget();
    }

    public void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
            isDragging = true;
            isFocusing = false; 
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

    public void MoveCamera(Vector3 delta)
    {
        Vector3 right = cam.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 move = (-delta.x * right + -delta.y * forward) * dragSpeed;

        transform.position += move;

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, limitX.x, limitX.y);
        pos.z = Mathf.Clamp(pos.z, limitZ.x, limitZ.y);
        transform.position = pos;
    }


    public void FocusOnTarget(Transform target)
    {
        Vector3 offset = cam.transform.forward;
        offset.y = 0;
        offset.Normalize();

        targetPosition = new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
        ) - offset * focusForwardOffset;

        isFocusing = true;
        isDragging = false;
    }

    void SmoothMoveToTarget()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            focusSmoothTime
        );

        ClampPosition();

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            isFocusing = false;
        }
    }
}