using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [Header("Available Tasks")]
    public List<Task> allTasks = new List<Task>();

    private List<Task> activeTasks = new List<Task>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        FindAllTasks();
    }

    private void Start()
    {
        // TESTING PURPOSES
        ActivateTask(allTasks[0]);
    }

    public void RegisterTask(Task task)
    {
        if (!allTasks.Contains(task))
        {
            allTasks.Add(task);
        }
    }

    public void ActivateTask(Task task)
    {
        if (activeTasks.Contains(task))
            return;

        task.Activate();

        activeTasks.Add(task);

        TaskUIManager.Instance.AddTask(task);
    }

    public void DeactivateTask(Task task)
    {
        if (!activeTasks.Contains(task))
            return;


        task.Deactivate();

        activeTasks.Remove(task);
    }

    private void FindAllTasks()
{
    Task[] tasks = FindObjectsByType<Task>();

    foreach(Task task in tasks)
    {
        RegisterTask(task);
    }
}

    public void TaskCompleted(Task task)
    {
        Debug.Log(
            $"Task completed: {task.taskName}"
        );

        activeTasks.Remove(task);
    }

    public List<Task> GetActiveTasks()
    {
        return activeTasks;
    }
}