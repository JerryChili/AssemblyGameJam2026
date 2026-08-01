using UnityEngine;

public class BossDoor : Interactable
{
    private bool coffeeRequired;

    private CoffeeTask currentCoffeeTask;



    public void SetCoffeeTask(CoffeeTask task)
    {
        currentCoffeeTask = task;
        coffeeRequired = true;
    }



    public override void Interact()
    {
        if (!coffeeRequired)
            return;


        InteractionController player =
            FindAnyObjectByType<InteractionController>();


        if (!player.HasItem())
            return;


        HeldItem item = player.GetHeldItem();


        CoffeeHeldItem coffee =
            item.GetComponent<CoffeeHeldItem>();


        if (coffee != null)
        {
            DeliverCoffee(player);
        }
    }



    private void DeliverCoffee(InteractionController player)
    {
        coffeeRequired = false;

        player.RemoveHeldItem();

        currentCoffeeTask?.CoffeeDelivered();

        Debug.Log("Coffee delivered!");
    }
}