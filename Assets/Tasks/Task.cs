using System;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public event Action<Task> OnTaskUpdated;

    [Header("Task Information")]
    public TaskType taskType;
    public string taskName;

    [Header("Task Progress")]
    [SerializeField] protected int requiredAmount = 1;
    

    protected int currentProgress;

    public bool IsActive { get; private set; }
    public bool IsComplete => currentProgress >= requiredAmount;

    public int CurrentProgress => currentProgress;
    public int RequiredAmount => requiredAmount;

    protected virtual void Awake()
    {
        
    }

    public virtual void Activate()
    {
        IsActive = true;
        currentProgress = 0;

        //gameObject.SetActive(true);

        Debug.Log($"{taskName} activated");
    }

    public virtual void Deactivate()
    {
        //IsActive = false;

        gameObject.SetActive(false);
    }

    public virtual void ResetTask()
    {
        currentProgress = 0;
        IsActive = false;

        gameObject.SetActive(false);
    }

    protected void AddProgress(int amount = 1)
    {
        if (!IsActive || IsComplete)
            return;

        currentProgress += amount;

        OnTaskUpdated?.Invoke(this);

        if (IsComplete)
        {
            Complete();
        }
    }

    protected virtual void Complete()
    {
        Debug.Log($"{taskName} completed!");

        TaskManager.Instance.TaskCompleted(this);
    }
}