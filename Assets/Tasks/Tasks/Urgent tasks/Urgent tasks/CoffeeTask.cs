using UnityEngine;

public class CoffeeTask : UrgentTask
{
    public BossDoor bossDoor;
    public CoffeeMachine machine;

    private void Start()
    {
        machine = FindAnyObjectByType<CoffeeMachine>();
    }

    public override void Activate()
    {
        base.Activate();
        bossDoor.SetTaskHighlight(true);
        machine.SetTaskHighlight(true);

        if (bossDoor == null)
        {
            Debug.LogError("CoffeeTask has no BossDoor assigned!");
            return;
        }


        bossDoor.SetCoffeeTask(this);


        Debug.Log("Boss requested coffee");
    }


    public void CoffeeDelivered()
    {
        machine.SetTaskHighlight(false);
        bossDoor.SetTaskHighlight(false);
        AddProgress();
    }
}