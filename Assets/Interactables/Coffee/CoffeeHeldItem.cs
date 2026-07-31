using UnityEngine;

public class CoffeeHeldItem : HeldItem
{
    public PlayerMovement player;
    public override void Use()
    {
        // Set sprint and recovery multipliers for a short while too
        player.RestoreStamina(5f);

        Debug.Log("Drank coffee");

        Destroy(gameObject);
    }
}
