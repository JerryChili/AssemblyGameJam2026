using UnityEngine;

public class KeyCardReader : Interactable
{
    public GameObject serverDoor;

    private bool unlocked;


    public override void Interact()
    {
        InteractionController player =
            FindAnyObjectByType<InteractionController>();


        if (!player.HasItem())
        {
            //Debug.Log("Need keycard.");
            return;
        }


        KeycardHeldItem keycard =
            player.GetHeldItem()
            .GetComponent<KeycardHeldItem>();


        if (keycard == null)
        {
            //Debug.Log("Wrong item.");
            return;
        }


        UnlockDoor();
    }



    public void UnlockDoor()
    {
        if (unlocked)
            return;


        unlocked = true;


        serverDoor.SetActive(false);


        //Debug.Log("Server room unlocked.");
    }



    public void LockDoor()
    {
        unlocked = false;


        serverDoor.SetActive(true);
    }
}