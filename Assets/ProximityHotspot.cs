using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ProximityHotspot : MonoBehaviour
{
    [TextArea(2, 6)]
    [SerializeField] private string body = "Exhibit information will appear here...";

    [SerializeField] private InfoPanelController controller;   // drag InfoPanelController_GO here in Inspector

    [SerializeField] private string playerTag = "MainCamera";  // make sure Main Camera has this tag

    private void Reset()
    {
        // Ensure this collider is a trigger
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            controller.ShowInfo(body);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            controller.HideInfo();
    }
}
