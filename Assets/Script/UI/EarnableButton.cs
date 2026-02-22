using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EarnableButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI priceLabel;
    [SerializeField] private TextMeshProUGUI countLabel;
    [SerializeField] private Button button;

    private EarnableObject_SO so;

    public static event System.Action OnAnySpawn;

    void Awake()
    {
        if (button == null) button = GetComponent<Button>();
    }

    void OnEnable()  => OnAnySpawn += UpdateUI;
    void OnDisable() => OnAnySpawn -= UpdateUI;

    public void Setup(EarnableObject_SO so, System.Action onClick)
    {
        this.so = so;

        if (icon != null)  icon.sprite = so.icon;
        if (label != null) label.text  = so.objectName;

        UpdateUI();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            onClick?.Invoke();
            OnAnySpawn?.Invoke(); // notify ALL buttons to refresh
        });
    }

    void UpdateUI()
    {
        if (so == null) return;

        if (priceLabel != null)
            priceLabel.text = $"${so.CurrentPrice}";

        if (countLabel != null)
        {
            if (so.type == EarnableObjectType.Static)
                countLabel.text = so.spawnCount > 0 ? "Owned" : "Not Owned";
            else
                countLabel.text = $"x{so.spawnCount}";
        }
    }
}