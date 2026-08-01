using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgentTaskManager : MonoBehaviour
{
    public static UrgentTaskManager Instance;


    private List<UrgentTask> availableTasks =
        new List<UrgentTask>();


    private List<UrgentTask> activeTasks =
        new List<UrgentTask>();


    public float minimumDelay = 45f;
    public float maximumDelay = 90f;



    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        StartCoroutine(UrgentLoop());
    }


    private IEnumerator UrgentLoop()
    {
        while (true)
        {
            float delay = Random.Range(
                minimumDelay,
                maximumDelay
            );


            yield return new WaitForSeconds(delay);


            ActivateRandomUrgentTask();
        }
    }



    public void RegisterUrgentTask(UrgentTask task)
    {
        if (!availableTasks.Contains(task))
        {
            availableTasks.Add(task);
        }
    }



    private void ActivateRandomUrgentTask()
    {
        if (availableTasks.Count == 0)
            return;


        UrgentTask task =
            availableTasks[
                Random.Range(
                    0,
                    availableTasks.Count
                )
            ];


        if (activeTasks.Contains(task))
            return;


        task.Activate();

        activeTasks.Add(task);

        /*if(UrgentTaskUIManager.Instance == null)
        {
            Debug.Log("UrgentTaskUIManager is null");
        }*/

        UrgentTaskUIManager.Instance.AddTask(task);


        Debug.Log(
            $"Urgent task activated: {task.taskName}"
        );
    }



    public void TaskCompleted(UrgentTask task)
    {
        activeTasks.Remove(task);

        UrgentTaskUIManager.Instance.RemoveTask(task);
    }



    public void TaskFailed(UrgentTask task)
    {
        activeTasks.Remove(task);

        UrgentTaskUIManager.Instance.RemoveTask(task);

        // Future:
        // Boss anger += failure amount
    }
}