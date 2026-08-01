using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuotaManager : MonoBehaviour
{
    public static QuotaManager Instance;

    public TMP_Text quotaStatusText;

    public AudioClip newQuota;
    public AudioClip quotaSuccess;
    public AudioClip quotaFail;
    private bool quotaRunning;

    [Header("Difficulty")]
    public int quotaNumber = 1;

    public int minimumTasks = 1;
    public int maximumTasks = 3;

    public float startingTime = 180f;
    public float timeDecreasePerQuota = 15f;


    private Quota currentQuota;
    public int completedQuotas;

    private float timer;

    public float quotaBreakTime = 5f;

    private bool waitingForNextQuota;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        StartQuota();
    }


    private void Update()
    {
        if (waitingForNextQuota)
            return;


        if (currentQuota == null)
            return;


        timer -= Time.deltaTime;

        UpdateQuotaText();

        if (timer <= 0)
        {
            FailQuota();
            return;
        }


        if (currentQuota.IsComplete)
        {
            CompleteQuota();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            TaskManager.Instance.RefreshTasks();
            StartQuota();
        }
    }

    public void StartQuota()
    {
        if (quotaRunning)
            return;

        currentQuota = new Quota();
        AudioManager.Instance.PlaySFX(newQuota);

        int amount = Random.Range(
            minimumTasks,
            maximumTasks + 1
        );


        List<Task> available =
            new List<Task>(TaskManager.Instance.allTasks);


        for (int i = 0; i < amount; i++)
        {
            if (available.Count == 0)
                break;


            int index = Random.Range(
                0,
                available.Count
            );


            Task chosen = available[index];

            available.RemoveAt(index);


            currentQuota.requiredTasks.Add(chosen);


            // Reset and activate fresh task
            if (TaskManager.Instance != null)
            {
                TaskManager.Instance.PrepareTask(chosen);
            }
        }


        timer = Mathf.Max(
            30,
            startingTime - ((quotaNumber - 1) * timeDecreasePerQuota)
        );


        Debug.Log(
            $"Quota {quotaNumber} started with {amount} tasks. Time: {timer}"
        );

        UpdateQuotaText();
    }

    public int GetCompletedQuotas()
    {
        return completedQuotas;
    }

    private void CompleteQuota()
    {
        Debug.Log("Quota completed!");

        completedQuotas++;
        AudioManager.Instance.PlaySFX(quotaSuccess);
        BossAngerManager.Instance.QuotaCompleted();
        StartCoroutine(NextQuotaDelay());
    }

    private IEnumerator NextQuotaDelay()
    {
        waitingForNextQuota = true;
        quotaRunning = false;

        float countdown = quotaBreakTime;

        while (countdown > 0)
        {
            quotaStatusText.text =
                $"Next quota in {Mathf.Ceil(countdown)}...";

            countdown -= Time.deltaTime;

            yield return null;
        }


        quotaStatusText.text = "";


        ClearQuotaUI();

        TaskManager.Instance.ClearActiveTasks();

        quotaNumber++;

        IncreaseDifficulty();

        StartQuota();


        waitingForNextQuota = false;
    }

    private void FailQuota()
    {
        Debug.Log(
            $"Quota failed. Missing tasks: {currentQuota.MissingTasks()}"
        );

        int failedTasks = currentQuota.MissingTasks();

        if (failedTasks > 0)
        {
            BossAngerManager.Instance.QuotaFailed(failedTasks);
        }

        IncreaseDifficulty();
        AudioManager.Instance.PlaySFX(quotaFail);
        StartCoroutine(NextQuotaDelay());
    }

    private void ClearQuotaUI()
    {
        foreach (Task task in currentQuota.requiredTasks)
        {
            TaskUIManager.Instance.RemoveTask(task);
        }
    }

    private void UpdateQuotaText()
    {
        quotaStatusText.text = $"Quota {quotaNumber}: ({Mathf.CeilToInt(timer)}s)";
    }


    private void IncreaseDifficulty()
    {
        maximumTasks = Mathf.Min(
            maximumTasks + 1,
            TaskManager.Instance.allTasks.Count
        );
    }


    public float GetRemainingTime()
    {
        return timer;
    }


    public Quota GetCurrentQuota()
    {
        return currentQuota;
    }
}