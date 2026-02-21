using UnityEngine;
using System.Collections;

public class EarnableObject : MonoBehaviour
{
    public EarnableObject_SO earnableObjectSO;

    private float timer;
    private bool isEarning = true;

    private void Start()
    {
        GameManager.instance.RegisterIncome(earnableObjectSO.moneyPerSecond);
    }

    private void OnDestroy()
    {
        GameManager.instance.UnregisterIncome(earnableObjectSO.moneyPerSecond);
    }


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

        StartCoroutine(SpawnCashWithDelay());
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

    IEnumerator SpawnCashWithDelay()
    {
        for (int i = 0; i < earnableObjectSO.moneyPerSecond; i++)
        {
            SpawnCash();
            yield return new WaitForSeconds(0.15f); // small delay between each cash
        }
    }
    IEnumerator JumpToTaxOffice(GameObject cash)
    {
        Vector3 start = cash.transform.position;
        Vector3 target = GameManager.instance.taxOfficeTransform.position;

        float duration = 1.5f;
        float elapsed = 0f;
        float height = 10f;

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