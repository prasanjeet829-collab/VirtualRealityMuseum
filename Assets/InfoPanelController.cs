using UnityEngine;
using TMPro;

public class InfoPanelController : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject panel;           // Drag your InfoPanel (or the whole canvas) here
    public TextMeshProUGUI infoText;   // Drag InfoText (TMP) here

    [TextArea(2, 6)]
    public string defaultBody = "Exhibit information will appear here...";

    private void Awake()
    {
        if (panel != null) panel.SetActive(false);   // Start hidden
    }

    // Show with optional custom text
    public void ShowInfo(string body = null)
    {
        if (panel == null || infoText == null) return;
        panel.SetActive(true);
        infoText.text = string.IsNullOrEmpty(body) ? defaultBody : body;
    }

    // Hide
    public void HideInfo()
    {
        if (panel == null) return;
        panel.SetActive(false);
    }

    // Toggle visibility
    public void Toggle()
    {
        if (panel == null) return;
        panel.SetActive(!panel.activeSelf);
    }
}
