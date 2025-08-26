using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils; // XROrigin

[RequireComponent(typeof(XROrigin))]
public class VerticalMoveProvider : MonoBehaviour
{
    [Header("Actions (assign from XRI action asset)")]
    public InputActionProperty moveUpAction;     // XRI ... / MoveUp (E)
    public InputActionProperty moveDownAction;   // XRI ... / MoveDown (Q)

    [Header("Settings")]
    public float verticalSpeed = 2.0f;           // meters per second

    CharacterController _cc;
    XROrigin _origin;

    void Awake()
    {
        _origin = GetComponent<XROrigin>();
        _cc = GetComponent<CharacterController>();
        if (_cc == null) _cc = GetComponentInChildren<CharacterController>();
    }

    void OnEnable()
    {
        moveUpAction.action?.Enable();
        moveDownAction.action?.Enable();
    }

    void OnDisable()
    {
        moveUpAction.action?.Disable();
        moveDownAction.action?.Disable();
    }

    void Update()
    {
        float up = (moveUpAction.action != null && moveUpAction.action.IsPressed()) ? 1f : 0f;
        float down = (moveDownAction.action != null && moveDownAction.action.IsPressed()) ? 1f : 0f;
        float y = up - down;
        if (Mathf.Approximately(y, 0f)) return;

        Vector3 motion = Vector3.up * y * verticalSpeed * Time.deltaTime;

        // Prefer CharacterController if present (same as ContinuousMoveProvider)
        if (_cc != null && _cc.enabled)
            _cc.Move(motion);
        else
            transform.position += motion;
    }
}
