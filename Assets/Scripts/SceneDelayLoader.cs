using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDelayLoader : MonoBehaviour
{
    [Header("Loading UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private string textPrefix = "Загрузка уровня через: ";

    [Header("Loading settings")]
    [SerializeField] private float delayBeforeLoad = 10f;

    private const string LoadingSceneName = "Load Scene";
    private static string selectedLevelScene = "Arama";

    /// <summary>
    /// Call this from a level-selection button to remember the chosen level
    /// and open the shared loading screen.
    /// Example: SceneDelayLoader.LoadSelectedScene("Arama");
    /// </summary>
    public static void LoadSelectedScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("SceneDelayLoader: selected scene name is empty.");
            return;
        }

        selectedLevelScene = sceneName;
        SceneManager.LoadScene(LoadingSceneName);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        float timeRemaining = Mathf.Max(0f, delayBeforeLoad);

        while (timeRemaining > 0f)
        {
            if (timerText != null)
            {
                timerText.text = textPrefix + Mathf.CeilToInt(timeRemaining);
            }

            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        if (timerText != null)
        {
            timerText.text = "Загрузка...";
        }

        if (!Application.CanStreamedLevelBeLoaded(selectedLevelScene))
        {
            Debug.LogError($"SceneDelayLoader: scene '{selectedLevelScene}' is not available in Build Settings.");
            if (timerText != null)
            {
                timerText.text = "Не удалось загрузить уровень";
            }

            yield break;
        }

        SceneManager.LoadScene(selectedLevelScene);
    }
}
