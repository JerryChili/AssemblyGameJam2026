using System;
using UnityEngine;

public class ServerResetButton : Interactable
{
    public Action OnResetPressed;
    public AudioClip press;


    public override void Interact()
    {
        //Debug.Log("Server reset!");

        if (OnResetPressed == null)
        {
            //Debug.Log("No reset listeners!");
        }
        else
        {
            //Debug.Log("Reset listeners exist!");
        }

        AudioManager.Instance.PlaySFX(press);
        OnResetPressed?.Invoke();
    }
}