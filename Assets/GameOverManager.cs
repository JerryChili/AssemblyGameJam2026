using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public AudioClip deathSound;
    public AudioClip deathLoop;


    [Header("UI")]
    public GameObject deathPanel;
    public GameObject credits;

    public RectTransform deathMessageTransform;
    public TMP_Text deathMessageText;

    public CanvasGroup quotaCanvas;
    public CanvasGroup buttonCanvas;

    public TMP_Text quotaText;

    public Button restartButton;
    public Button creditsButton;


    [Header("Animation")]
    public float moveAmount = 60f;
    public float moveDuration = 0.8f;
    public float fadeDuration = 1f;


    [Header("Death Messages")]
    public string[] deathMessages =
    {
        "UNHIREABLE",
        "EMPLOYEE TERMINATED",
        "CONTRACT VOIDED",
        "PERFORMANCE REVIEW FAILED",
        "YOUR CAREER IS OVER",
        "HR WILL NOT SAVE YOU",
        "THE FINAL NDA"
    };


    private Vector2 startingPosition;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        startingPosition =
            deathMessageTransform.anchoredPosition;


        deathPanel.SetActive(false);


        restartButton.onClick.AddListener(RestartGame);
        creditsButton.onClick.AddListener(ToggleCredits);
    }

    public void GameOver()
    {
        AudioManager.Instance.StopAllSounds();
        AudioManager.Instance.PlaySFX(deathSound);
        AudioManager.Instance.PlayAmbience(deathLoop);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathPanel.SetActive(true);


        deathMessageText.text =
            deathMessages[
                Random.Range(0, deathMessages.Length)
            ];


        quotaText.text =
            $"You filled {QuotaManager.Instance.GetCompletedQuotas()} quotas";


        // Reset UI states

        deathMessageTransform.anchoredPosition =
            startingPosition;


        quotaCanvas.alpha = 0;
        buttonCanvas.alpha = 0;


        restartButton.interactable = false;


        StartCoroutine(DeathSequence());
    }



    private IEnumerator DeathSequence()
    {
        // Big text appears instantly

        yield return null;


        Vector2 target =
            startingPosition + Vector2.up * moveAmount;


        float timer = 0;


        while (timer < moveDuration)
        {
            timer += Time.unscaledDeltaTime;


            float t =
                timer / moveDuration;


            deathMessageTransform.anchoredPosition =
                Vector2.Lerp(
                    startingPosition,
                    target,
                    t
                );


            yield return null;
        }



        // Fade quota text

        yield return StartCoroutine(
            FadeCanvas(quotaCanvas)
        );


        // Fade button

        yield return StartCoroutine(
            FadeCanvas(buttonCanvas)
        );


        restartButton.interactable = true;
    }



    private IEnumerator FadeCanvas(CanvasGroup canvas)
    {
        float timer = 0;


        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;


            canvas.alpha =
                timer / fadeDuration;


            yield return null;
        }


        canvas.alpha = 1;
    }

    private void ToggleCredits()
    {
        if (credits == null)
        {
            Debug.LogWarning("No target object assigned!");
            return;
        }


        credits.SetActive(!credits.activeSelf);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}