using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MopHeldItem : HeldItem
{
    public float sweepRange = 2f;

    public override void Use()
    {
        //Debug.Log("Sweeping floor");

        Ray ray = new Ray(
            transform.position,
            transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, sweepRange))
        {
            Spill spill = hit.collider.GetComponent<Spill>();

            if (spill != null)
            {
                spill.Clean();
            }
        }
    }
}