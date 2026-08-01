using UnityEngine;

public class PickupItem : Interactable
{
    public HeldItem heldItemPrefab;
    private GameObject worldItemPrefab;

    private void Start()
    {
        worldItemPrefab = gameObject;
    }

    private InteractionController player;

    public override void Interact()
    {
        player = FindAnyObjectByType<InteractionController>();

        //Debug.Log("Interacted with " + gameObject.name);
        if (player.HasItem())
            return;

        AudioManager.Instance.PlayItemPickupSound();
        player.PickupItem(this);
    }

    public HeldItem GetHeldItem()
    {
        return heldItemPrefab;
    }

    public GameObject GetWorldItem()
    {
        return worldItemPrefab;
    }

    public virtual void RemoveFromWorld()
    {
        gameObject.SetActive(false);
    }
}