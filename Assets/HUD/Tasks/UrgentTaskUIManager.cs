using System.Collections.Generic;
using UnityEngine;

public class UrgentTaskUIManager : MonoBehaviour
{
    public static UrgentTaskUIManager Instance;


    public Transform listParent;
    public GameObject entryPrefab;


    private Dictionary<UrgentTask, UrgentTaskUIEntry> entries =
        new();



    private void Awake()
    {
        Instance = this;
    }



    public void AddTask(UrgentTask task)
    {
        Debug.Log("Adding urgent task UI: " + task.taskName);


        GameObject obj = Instantiate(
            entryPrefab,
            listParent
        );


        Debug.Log("Spawned prefab: " + obj.name);


        UrgentTaskUIEntry entry =
            obj.GetComponent<UrgentTaskUIEntry>();


        if (entry == null)
        {
            Debug.LogError(
                "UrgentTaskUIEntry missing from prefab!"
            );

            return;
        }


        entries.Add(task, entry);


        entry.Setup(task);
    }



    public void RemoveTask(UrgentTask task)
    {
        if (entries.TryGetValue(task, out var entry))
        {
            Destroy(entry.gameObject);

            entries.Remove(task);
        }
    }
}