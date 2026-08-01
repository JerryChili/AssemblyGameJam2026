using UnityEngine;

public class CoffeeHeldItem : HeldItem
{
    [Header("Coffee Buff")]
    public float sprintBoost = 1.25f;
    public float staminaRecoveryBoost = 1.5f;

    public float buffDuration = 30f;


    private bool consumed;


    public override void Use()
    {
        if (consumed)
            return;


        consumed = true;


        PlayerMovement movement = FindAnyObjectByType<PlayerMovement>();


        if (movement != null)
        {
            movement.SetSprintMultiplier(sprintBoost);
            movement.SetRecoveryMultiplier(staminaRecoveryBoost);


            StartCoroutine(RemoveBuff(movement));
        }


        Debug.Log("Coffee consumed");


        Destroy(gameObject);
    }


    private System.Collections.IEnumerator RemoveBuff(
        PlayerMovement movement
    )
    {
        yield return new WaitForSeconds(buffDuration);


        movement.SetSprintMultiplier(1f);
        movement.SetRecoveryMultiplier(1f);
    }
}