using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    private Camera playerCamera;

    private Interactable currentInteractable;

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
            Debug.Log("Hit: " + hit.collider.name);
            currentInteractable = hit.collider.GetComponent<Interactable>();

            if (currentInteractable != null)
            {
                Debug.Log(currentInteractable.GetPrompt());
            }
        }
    }
}