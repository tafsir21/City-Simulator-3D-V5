using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private string nextScene = "MainScene";

    void Start()
    {
        StartCoroutine(FakeLoad());
    }

    IEnumerator FakeLoad()
    {
        // fast and slow fake load
        yield return FillTo(0.98f, 1.5f);
        yield return FillTo(1f, 0.8f);
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(nextScene);
    }

    IEnumerator FillTo(float target, float duration)
    {
        float start   = progressBar.fillAmount;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            progressBar.fillAmount = Mathf.Lerp(start, target, elapsed / duration);

            if (progressText != null)
                progressText.text = $"{Mathf.FloorToInt(progressBar.fillAmount * 100)}%";

            yield return null;
        }

        progressBar.fillAmount = target;
    }
}