using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Press E to interact";

    public abstract void Interact();

    public virtual string GetPrompt()
    {
        return interactionPrompt;
    }
}