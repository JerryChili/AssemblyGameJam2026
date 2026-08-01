using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuotaManager : MonoBehaviour
{
    public static QuotaManager Instance;

    public TMP_Text quotaStatusText;


    [Header("Difficulty")]
    public int quotaNumber = 1;

    public int minimumTasks = 1;
    public int maximumTasks = 3;

    public float startingTime = 180f;
    public float timeDecreasePerQuota = 15f;


    private Quota currentQuota;
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


    public void StartQuota()
    {
        currentQuota = new Quota();


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
            TaskManager.Instance.PrepareTask(chosen);
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


    private void CompleteQuota()
    {
        Debug.Log("Quota completed!");

        StartCoroutine(NextQuotaDelay());
    }

    private IEnumerator NextQuotaDelay()
    {
        waitingForNextQuota = true;

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

        // Future:
        // Boss anger increases based on missing tasks

        quotaNumber++;

        IncreaseDifficulty();

        StartQuota();
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
        quotaStatusText.text = $"Quota {quotaNumber}";
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