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
        if (entries.ContainsKey(task))
        {
            Destroy(entries[task].gameObject);
            entries.Remove(task);
        }
    }
}