using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MopHeldItem : HeldItem
{
    public float sweepRange = 2f;

    public Camera playerCamera;
    private void Awake()
    {
        playerCamera = Camera.main;
    }

    public override void Use()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f)
        );


        if (Physics.SphereCast(
            ray,
            0.3f,
            out RaycastHit hit,
            sweepRange
        ))
        {
            Debug.Log("Mop hit: " + hit.collider.name);

            Spill spill = hit.collider.GetComponentInParent<Spill>();

            if (spill != null)
            {
                spill.Clean();
            }
        }
    }
}