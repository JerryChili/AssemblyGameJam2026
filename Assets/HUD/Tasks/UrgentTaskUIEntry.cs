using TMPro;
using UnityEngine;

public class UrgentTaskUIEntry : MonoBehaviour
{
    private TMP_Text text;

    private UrgentTask currentTask;


    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }


    public void Setup(UrgentTask task)
    {
        currentTask = task;

        task.OnUrgentTaskUpdated += UpdateTask;

        UpdateTask(task);
    }


    private void UpdateTask(UrgentTask task)
    {
        if (text == null)
            return;


        text.text =
            $"{task.taskName}: {task.CurrentProgress}/{task.RequiredAmount}\n" +
            $"TIME: {Mathf.Ceil(task.RemainingTime)}s";
    }


    private void OnDestroy()
    {
        if (currentTask != null)
        {
            currentTask.OnUrgentTaskUpdated -= UpdateTask;
        }
    }
}