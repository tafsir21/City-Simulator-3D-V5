using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestMarketButtons : MonoBehaviour
{
    [Header("References")]
    public Transform staticButtonContainer;
    public Transform moveableButtonContainer;
    public EarnableButton buttonPrefab;

    void Start()
    {
        BuildButtons(GameManager.instance.staticObjects,   staticButtonContainer);
        BuildButtons(GameManager.instance.moveableObjects, moveableButtonContainer);
    }

    void BuildButtons(System.Collections.Generic.List<EarnableObject_SO> list, Transform container)
    {
        if (list == null || list.Count == 0)   { Debug.LogWarning("List is empty or null!");       return; }
        if (container == null)                  { Debug.LogError("Container is not assigned!");     return; }
        if (buttonPrefab == null)               { Debug.LogError("Button prefab is not assigned!"); return; }

        foreach (var so in list)
        {
            if (so == null) { Debug.LogWarning("Null SO in list, skipping."); continue; }

            EarnableButton btn = Instantiate(buttonPrefab, container);
            var captured = so;
            btn.Setup(so, () => Spawner.instance.Spawn(captured));
        }
    }
}