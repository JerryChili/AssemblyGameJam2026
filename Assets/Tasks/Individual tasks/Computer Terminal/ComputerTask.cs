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


    private void ActivateBug()
    {
        // Pick random computer

        infectedComputer = computers[
            Random.Range(0, computers.Count)
        ];


        infectedComputer.isInfected = true;

        infectedComputer.OnBugFixed += BugFixed;

        requiredAmount = 1;


        Debug.Log(
            $"Computer infected: {infectedComputer.name}"
        );
    }


    private void BugFixed()
    {
        AddProgress();

        infectedComputer.OnBugFixed -= BugFixed;
    }
}