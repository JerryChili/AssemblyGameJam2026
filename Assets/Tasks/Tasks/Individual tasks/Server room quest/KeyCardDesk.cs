using System;
using UnityEngine;

public class KeyCardDesk : Interactable
{
    public Transform spawnPoint;
    public PickupItem keycardPrefab;

    public ServerTask task;


    private PickupItem currentKeycard;


    public Action OnKeycardTaken;
    public Action OnKeycardReturned;



    public void SpawnKeycard()
    {
        if (currentKeycard != null)
            return;

        //Debug.Log("Spawn point world position: " + spawnPoint.position);

        currentKeycard = Instantiate(
            keycardPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        if(currentKeycard != null)
        {
            //Debug.Log("Keycard spawned");
        }
    }



    public override void Interact()
    {
        InteractionController player =
            FindAnyObjectByType<InteractionController>();


        if (!player.HasItem())
            return;


        KeycardHeldItem keycard =
            player.GetHeldItem()
            .GetComponent<KeycardHeldItem>();


        if (keycard == null)
            return;


        if (task == null)
        {
            //Debug.LogError("KeyCardDesk has no ServerTask assigned!");
            return;
        }

        if (!task.CanReturnKeycard)
        {
            //Debug.Log("Server has not been reset yet.");
            return;
        }


        player.RemoveHeldItem();


        if (OnKeycardReturned == null)
        {
            //Debug.Log("No keycardreturn listeners!");
        }
        else
        {
            //Debug.Log("Keycardreturn listeners found!");
        }
        OnKeycardReturned?.Invoke();
    }



    public void KeycardPickedUp()
    {
        currentKeycard = null;

        OnKeycardTaken?.Invoke();
    }



    public void ClearKeycard()
    {
        if (currentKeycard != null)
        {
            Destroy(currentKeycard.gameObject);
            currentKeycard = null;
        }
    }
}