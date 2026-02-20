using UnityEngine;

public class PlayerTapDetection : MonoBehaviour
{
    // This method will be called when the object is tapped
    protected virtual void OnTap()
    {
        // Default behavior when tapped (can be overridden)
        Debug.Log("Tapped on: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is left-click or tap on the screen
        {
            // Create a ray from the camera to the mouse position (or tap position)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // If the ray hits something
            {
                // Check if the object hit is this one
                if (hit.transform == transform)
                {
                    // Call the overridden OnTap method
                    OnTap();
                }
            }
        }
    }
}