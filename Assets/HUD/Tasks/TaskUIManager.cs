using System.Collections.Generic;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{
    public static TaskUIManager Instance;

    public Transform taskListParent;
    public GameObject taskEntryPrefab;


    private Dictionary<Task, TaskUIEntry> entries = new Dictionary<Task, TaskUIEntry>();


    private void Awake()
    {
        Instance = this;
    }


    public void AddTask(Task task)
    {
        // Task already has a UI entry
        if (entries.ContainsKey(task))
        {
            TaskUIEntry existingEntry = entries[task];

            task.OnTaskUpdated -= existingEntry.UpdateTask;
            task.OnTaskUpdated += existingEntry.UpdateTask;

            existingEntry.UpdateTask(task);

            return;
        }


        GameObject obj = Instantiate(
            taskEntryPrefab,
            taskListParent
        );


        TaskUIEntry entry =
            obj.GetComponent<TaskUIEntry>();


        entries.Add(task, entry);


        task.OnTaskUpdated += entry.UpdateTask;


        entry.UpdateTask(task);
    }


    public void RemoveTask(Task task)
    {
        if (entries.TryGetValue(task, out TaskUIEntry entry))
        {
            task.OnTaskUpdated -= entry.UpdateTask;

            Destroy(entry.gameObject);

            entries.Remove(task);
        }
    }
}