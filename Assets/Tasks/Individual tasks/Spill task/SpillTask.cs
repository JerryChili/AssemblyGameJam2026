using UnityEngine;
using System.Collections.Generic;

public class SpillTask : Task
{
    public List<Spill> spills = new List<Spill>();

    public int minimumSpills = 2;
    public int maximumSpills = 5;

    private List<Spill> activeSpills = new List<Spill>();


    public override void Activate()
    {
        base.Activate();

        ActivateRandomSpills();
    }


    private void ActivateRandomSpills()
    {
        foreach (Spill spill in spills)
        {
            spill.gameObject.SetActive(false);
        }

        activeSpills.Clear();


        int amount = Random.Range(
            minimumSpills,
            maximumSpills + 1
        );

        requiredAmount = amount;


        List<Spill> shuffled = new List<Spill>(spills);

        for (int i = 0; i < shuffled.Count; i++)
        {
            Spill temp = shuffled[i];

            int randomIndex = Random.Range(i, shuffled.Count);

            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }


        for (int i = 0; i < amount; i++)
        {
            Spill spill = shuffled[i];

            spill.Activate();

            spill.OnCleaned += SpillCleaned;

            activeSpills.Add(spill);
        }


        Debug.Log($"Activated {amount} spills");
    }


    private void SpillCleaned()
    {
        AddProgress();
    }
}