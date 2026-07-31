using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    private Camera playerCamera;

    private Interactable currentInteractable;

    public Transform handSocket;
    private HeldItem heldItem;

    public float throwForce = 8f;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (heldItem != null)
            {
                heldItem.Use();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    void CheckForInteractable()
    {
        currentInteractable = null;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            //Debug.Log("Hit: " + hit.collider.name);
            currentInteractable = hit.collider.GetComponent<Interactable>();

            if (currentInteractable != null)
            {
                //Debug.Log(currentInteractable.GetPrompt());
            }
        }
    }
    
    public bool HasItem()
    {
        return heldItem != null;
    }

    public void PickupItem(PickupItem item)
    {
        heldItem = Instantiate(
            item.GetHeldItem(),
            handSocket
        );

        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        item.RemoveFromWorld();
    }


    public void DropItem()
    {
        if (heldItem == null)
            return;

        GameObject droppedObject = Instantiate(
            heldItem.worldItemPrefab,
            handSocket.position,
            handSocket.rotation
        );

        Rigidbody rb = droppedObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(
                handSocket.forward * throwForce,
                ForceMode.Impulse
            );
        }

        Destroy(heldItem.gameObject);

        heldItem = null;
    }
}