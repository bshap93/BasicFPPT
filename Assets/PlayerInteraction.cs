using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // How far the player can interact
    public LayerMask interactableLayer; // Only detect objects in this layer
    public Camera playerCamera; // Reference to the player’s camera
    public Image reticle; 
    public Color defaultReticleColor = Color.white;
    public Color interactReticleColor = Color.green;

    void Update()
    {
        CheckForInteractable();
        
        if (Input.GetKeyDown(KeyCode.E)) // Press E to interact
        {
            PerformInteraction();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(playerCamera.transform.position, 
            playerCamera.transform.TransformDirection(Vector3.forward) * interactionDistance);
        
    }

    void CheckForInteractable()
    {
        if (playerCamera == null)
        {
            Debug.LogError("PlayerInteraction: No camera assigned!");
            return;
        }
        
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position; // Start from the camera
        Vector3 rayDirection = playerCamera.transform.TransformDirection(Vector3.forward); // Cast forward from camera

        
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance, interactableLayer))
        {

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                reticle.color = interactReticleColor;
                return;
            }
        }
        
        reticle.color = defaultReticleColor;
    } 

    void PerformInteraction()
    {
        if (playerCamera == null)
        {
            Debug.LogError("PlayerInteraction: No camera assigned!");
            return;
        }

        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position; // Start from the camera
        Vector3 rayDirection = playerCamera.transform.forward; // Cast forward from camera

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
