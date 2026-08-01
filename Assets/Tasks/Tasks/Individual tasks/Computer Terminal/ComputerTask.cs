using System.Collections.Generic;
using UnityEngine;

public class ComputerTask : Task
{
    public List<ComputerTerminal> computers = new List<ComputerTerminal>();

    private ComputerTerminal infectedComputer;


    public override void Activate()
    {
        base.Activate();

        ActivateBug();
    }

    public override void ResetTask()
    {
        base.ResetTask();

        if (infectedComputer != null)
        {
            infectedComputer.isInfected = false;
            infectedComputer.OnBugFixed -= BugFixed;
            infectedComputer = null;
        }
    }


    private void ActivateBug()
    {
        infectedComputer = computers[
            Random.Range(0, computers.Count)
        ];


        infectedComputer.isInfected = true;
        infectedComputer.SetTaskHighlight(true);


        infectedComputer.OnBugFixed -= BugFixed;
        infectedComputer.OnBugFixed += BugFixed;


        requiredAmount = 1;


        //Debug.Log($"Computer infected: {infectedComputer.name}");
    }


    private void BugFixed()
    {
        infectedComputer.SetTaskHighlight(false);

        AddProgress();

        infectedComputer.OnBugFixed -= BugFixed;
    }
}