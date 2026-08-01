using UnityEngine;

public class CoffeeMachine : Interactable
{
    public float brewTime = 10f;

    public PickupItem coffeePickup;


    private bool brewing;
    private bool ready;


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


    private System.Collections.IEnumerator Brew()
    {
        brewing = true;

        Debug.Log("Brewing coffee...");


        yield return new WaitForSeconds(brewTime);


        brewing = false;
        ready = true;


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