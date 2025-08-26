using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    public Transform target; // leave empty to auto-use main camera

    void LateUpdate()
    {
        var t = target ? target : Camera.main?.transform;
        if (t == null) return;

        // Face the camera but keep panel upright
        Vector3 forward = (transform.position - t.position);
        forward.y = 0f;
        if (forward.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(forward.normalized, Vector3.up);
    }
}
