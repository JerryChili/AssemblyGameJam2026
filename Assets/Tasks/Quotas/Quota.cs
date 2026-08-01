using System.Collections.Generic;

public class Quota
{
    public List<Task> requiredTasks = new List<Task>();

    public float timeLimit;


    public bool IsComplete
    {
        get
        {
            foreach (Task task in requiredTasks)
            {
                if (!task.IsComplete)
                    return false;
            }

            return true;
        }
    }


    public int MissingTasks()
    {
        int missing = 0;

        foreach (Task task in requiredTasks)
        {
            if (!task.IsComplete)
                missing++;
        }

        return missing;
    }
}