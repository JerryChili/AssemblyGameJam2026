using UnityEngine;

public class SpillTask : Task
{
    public override void Activate()
    {
        base.Activate();
        Debug.Log("Hello from SpillTask.cs");
        // Enable spill locations here
    }


    public void SpillCleaned()
    {
        AddProgress();
    }
}