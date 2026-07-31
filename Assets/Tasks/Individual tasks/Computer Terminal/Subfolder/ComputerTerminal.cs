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


        codeUI.SetActive(true);


        minigame.OnCompleted += FixBug;
    }


    public void FixBug()
    {
        isInfected = false;

        minigame.OnCompleted -= FixBug;

        OnBugFixed?.Invoke();
    }
}