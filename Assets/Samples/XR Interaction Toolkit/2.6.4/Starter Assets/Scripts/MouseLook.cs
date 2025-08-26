using UnityEngine;
using UnityEngine.InputSystem; // ok to keep even if you're not using the new input system

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    public Transform xrOrigin;          // Drag XR Origin (XR Rig)
    public Transform cameraPivot;       // Leave as Main Camera

    [Header("Settings")]
    public float sensitivity = 0.15f;
    public float pitchMin = -85f;
    public float pitchMax = 85f;
    public bool lockCursorOnStart = true;

    [Header("Toggle")]
    public KeyCode toggleKey = KeyCode.Tab;   // Press Tab to toggle lock
    public bool allowRightClickToggle = true; // Right-click to toggle

    float yaw;
    float pitch;
    bool cursorLocked;

    void Start()
    {
        if (cameraPivot == null) cameraPivot = transform;
        // Initialize yaw/pitch from current orientation
        var e = xrOrigin != null ? xrOrigin.eulerAngles : transform.eulerAngles;
        yaw = e.y;
        pitch = cameraPivot.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        SetCursorLocked(lockCursorOnStart);
    }

    void OnDisable()
    {
        // Always restore cursor on disable
        SetCursorLocked(false);
    }

    void Update()
    {
        // --- Toggle lock/unlock ---
        if (Input.GetKeyDown(toggleKey) ||
            (allowRightClickToggle && Input.GetMouseButtonDown(1)))
        {
            SetCursorLocked(!cursorLocked);
        }

        // Also allow Escape to unlock (nice for Editor)
        if (Input.GetKeyDown(KeyCode.Escape))
            SetCursorLocked(false);

        // --- Rotate only when locked ---
        if (!cursorLocked) return;

        Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        yaw += delta.x * 1000f * sensitivity * Time.deltaTime;
        pitch -= delta.y * 1000f * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        if (xrOrigin != null) xrOrigin.rotation = Quaternion.Euler(0f, yaw, 0f);
        if (cameraPivot != null) cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void SetCursorLocked(bool locked)
    {
        cursorLocked = locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
