using UnityEngine;

public class CoffeeTask : UrgentTask
{
    public BossDoor bossDoor;


    public override void Activate()
    {
        base.Activate();


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
        AddProgress();
    }
}