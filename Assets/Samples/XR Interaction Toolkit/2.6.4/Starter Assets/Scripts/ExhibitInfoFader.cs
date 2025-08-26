using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
public class ExhibitInfoFader : MonoBehaviour
{
    [Header("Panel to show/hide")]
    public CanvasGroup panel;                // world-space Canvas with a CanvasGroup

    [Header("Fade Settings")]
    public float fadeDuration = 0.25f;       // seconds
    public bool showOnHover = true;          // show when ray hovers
    public bool showOnSelect = false;        // or only when selected/clicked

    float targetAlpha = 0f;
    float velocity;

    void Awake()
    {
        // Start hidden
        if (panel != null)
        {
            panel.alpha = 0f;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }

        // Subscribe to XR Simple Interactable events
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(_ => { if (showOnHover) Show(); });
        interactable.hoverExited.AddListener(_ => { if (showOnHover) Hide(); });
        interactable.selectEntered.AddListener(_ => { if (showOnSelect) Show(); });
        interactable.selectExited.AddListener(_ => { if (showOnSelect) Hide(); });
    }

    void Update()
    {
        if (panel == null) return;

        // Smooth damp alpha
        float a = Mathf.SmoothDamp(panel.alpha, targetAlpha, ref velocity, fadeDuration);
        panel.alpha = a;

        // Toggle interactability near fully visible
        bool visible = a > 0.98f;
        panel.interactable = visible;
        panel.blocksRaycasts = visible;
    }

    public void Show() => targetAlpha = 1f;
    public void Hide() => targetAlpha = 0f;
}
