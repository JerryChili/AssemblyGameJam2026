using UnityEngine;

public class DeathBlock : Interactable
{
    public override void Interact()
    {
        GameOverManager.Instance.GameOver();
    }
}
