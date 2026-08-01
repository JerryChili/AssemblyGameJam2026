using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Press E to interact";

    private Outline outline;

    protected virtual void Awake()
    {
        outline = GetComponent<Outline>();
        //Debug.Log(outline);


        if (outline != null)
            outline.enabled = false;
    }

    public void Highlight(bool value)
    {
        //Debug.Log("Highlight value: " + value);
        if (outline != null)
            outline.enabled = value;
    }

    public abstract void Interact();

    public virtual string GetPrompt()
    {
        return interactionPrompt;
    }
}