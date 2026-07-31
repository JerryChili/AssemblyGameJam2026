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
        //Debug.Log("Sweeping floor");

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

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