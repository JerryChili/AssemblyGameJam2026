using UnityEngine;

public class KeyCard : PickupItem
{
    public KeyCardDesk desk;
    private void Start()
    {
        SetTaskHighlight(true);
    }

    public override void RemoveFromWorld()
    {
        desk.KeycardPickedUp();
        base.RemoveFromWorld();
    }
}