using UnityEngine;
using System.Collections;

public class EarnableObject : MonoBehaviour
{
    public EarnableObject_SO earnableObjectSO;

    private float timer;
    private bool isEarning = true;

    void Update()
    {
        if (!isEarning || earnableObjectSO == null) return;

        timer += Time.deltaTime;

        if (timer >= 1.5f)
        {
            EarnMoney();
            timer -= 1.5f;
        }
    }

    void EarnMoney()
    {
        GameManager.instance.AddMoney(earnableObjectSO.moneyPerSecond);

        for (int i = 0; i < earnableObjectSO.moneyPerSecond; i++)
        {
            SpawnCash();
        }
    }

    void SpawnCash()
    {
        GameObject cash = Instantiate(
            earnableObjectSO.cashPrefab,
            transform.position,
            Quaternion.identity
        );

        StartCoroutine(JumpToTaxOffice(cash));
    }

    IEnumerator JumpToTaxOffice(GameObject cash)
    {
        Vector3 start = cash.transform.position;
        Vector3 target = GameManager.instance.taxOfficeTransform.position;

        float duration = 0.8f;
        float elapsed = 0f;
        float height = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            Vector3 pos = Vector3.Lerp(start, target, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * height;

            cash.transform.position = pos;

            yield return null;
        }

        Destroy(cash);
    }
}