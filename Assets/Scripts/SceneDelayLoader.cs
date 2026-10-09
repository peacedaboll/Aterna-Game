using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneDelayLoader : MonoBehaviour
{
    [Header("Настройки UI")]
    [SerializeField] private TextMeshProUGUI timerText; 
    [SerializeField] private string textPrefix = "До начала: "; 

    [Header("Настройки таймера")]
    [SerializeField] private float delayBeforeLoad = 10f;

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Пожалуйста, перетащите объект текста в поле Timer Text в Инспекторе!");
            return;
        }

        StartCoroutine(LoadSceneWithCountdown());
    }

    private IEnumerator LoadSceneWithCountdown()
    {
        float timeRemaining = delayBeforeLoad;

        while (timeRemaining > 0)
        {
            timerText.text = textPrefix + Mathf.CeilToInt(timeRemaining).ToString();

            yield return new WaitForSeconds(1f);

            timeRemaining -= 1f;
        }

        timerText.text = "Загрузка...";

        SceneManager.LoadScene("Map");
    }
}
