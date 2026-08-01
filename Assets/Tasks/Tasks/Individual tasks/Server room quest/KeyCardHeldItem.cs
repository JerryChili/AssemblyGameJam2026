using UnityEngine;

public class KeycardHeldItem : HeldItem
{
    public override void Use()
    {
        // The keycard isn't "used" directly.
        // It is checked automatically when interacting
        // with the server room door or returning it to the desk.
    }
}