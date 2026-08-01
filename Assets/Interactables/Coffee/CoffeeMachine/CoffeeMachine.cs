using System.Collections;
using UnityEngine;

public class CoffeeMachine : Interactable
{
    public float brewTime = 10f;

    public PickupItem coffeePickup;
    private AudioSource machineAudio;


    private bool brewing;
    private bool ready;

    private void Awake()
    {
        machineAudio = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (ready)
        {
            GiveCoffee();
            return;
        }


        if (!brewing)
        {
            StartCoroutine(Brew());
        }
    }


    private IEnumerator Brew()
    {
        brewing = true;
        if(!machineAudio.isPlaying && machineAudio != null)
        {
            machineAudio.Play();
        }

        Debug.Log("Brewing coffee...");


        yield return new WaitForSeconds(brewTime);


        brewing = false;
        ready = true;

        if (machineAudio.isPlaying && machineAudio != null)
        {
            machineAudio.Stop();
        }

        Debug.Log("Coffee ready!");
    }


    private void GiveCoffee()
    {
        InteractionController player = FindAnyObjectByType<InteractionController>();


        if (player.HasItem())
        {
            Debug.Log("Hands full!");
            return;
        }


        player.PickupItem(coffeePickup);


        ready = false;


        Debug.Log("Coffee taken");
    }
}