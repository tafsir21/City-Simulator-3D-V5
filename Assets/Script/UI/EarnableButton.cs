using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EarnableButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Button button;

    void Awake()
    {
        // Auto-grab if not assigned in Inspector
        if (button == null) button = GetComponent<Button>();
    }

    public void Setup(EarnableObject_SO so, System.Action onClick)
    {
        if (button == null)
        {
            Debug.LogError("No Button component found on " + gameObject.name);
            return;
        }

        if (icon != null)  icon.sprite = so.icon;
        if (label != null) label.text  = so.objectName;
        if (price != null) price.text  = "$"+so.price.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }
}