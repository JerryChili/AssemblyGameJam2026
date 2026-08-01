using UnityEngine;

public class KeyCard : PickupItem
{
    public KeyCardDesk desk;


    public override void RemoveFromWorld()
    {
        desk.KeycardPickedUp();

        base.RemoveFromWorld();
    }
}