using System;
using UnityEngine;

public class ComputerTerminal : Interactable
{
    public bool isInfected;

    public GameObject codeUI;
    public CodeMinigame minigame;

    public Action OnBugFixed;


    public override void Interact()
    {
        if (!isInfected)
            return;


        minigame.ResetMinigame();

        codeUI.SetActive(true);

        minigame.OnCompleted -= FixBug;
        minigame.OnCompleted += FixBug;
    }

    public void ResetTerminal()
    {
        isInfected = false;

        minigame.OnCompleted -= FixBug;
    }

    public void FixBug()
    {
        isInfected = false;

        minigame.OnCompleted -= FixBug;

        OnBugFixed?.Invoke();
    }
}