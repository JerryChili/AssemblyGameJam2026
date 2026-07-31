using TMPro;
using UnityEngine;

public class TaskUIEntry : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void UpdateTask(Task task)
    {
        text.text = $"{task.taskName}: {task.CurrentProgress}/{task.RequiredAmount}";
    }
}