using UnityEngine;

public abstract class HeldItem : MonoBehaviour
{
    public string itemName;

    public GameObject worldItemPrefab;

    public abstract void Use();
}