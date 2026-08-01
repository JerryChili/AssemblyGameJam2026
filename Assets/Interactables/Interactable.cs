using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Press E to interact";

    private Outline outline;

    private bool isHovered;
    private bool isTaskHighlighted;


    protected virtual void Awake()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;
    }


    public void Highlight(bool value)
    {
        isHovered = value;

        UpdateOutline();
    }


    public void SetTaskHighlight(bool value)
    {
        isTaskHighlighted = value;

        UpdateOutline();
    }


    private void UpdateOutline()
    {
        if (outline == null)
            return;


        outline.enabled =
            isHovered || isTaskHighlighted;
    }


    public abstract void Interact();


    public virtual string GetPrompt()
    {
        return interactionPrompt;
    }
}