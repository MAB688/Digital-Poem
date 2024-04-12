using TMPro;
using UnityEngine;

public class ReadNote : MonoBehaviour, IInteractable
{
    public GameObject textDisplayUI; // Reference to the UI element displaying the text
    public string noteText; // The text content of the note
    public GameObject gameObjectToEnable; // GameObject to enable when interacted with
    public GameObject gameObjectToDisable; // GameObject to disable when interacted with

    private TMP_Text displayTextComponent;
    private PlayerLook cameraMovementScript; // Reference to the CameraMovement script
    private PlayerMovement movementScript; // Reference to the PlayerMovement script

    private void Start()
    {
        // Ensure the text display object is assigned and get the TMP_Text component
        if (textDisplayUI != null)
        {
            displayTextComponent = textDisplayUI.GetComponent<TMP_Text>();
            textDisplayUI.SetActive(false); // Make sure the text is hidden at start
        }
        else
        {
            Debug.LogError("Text display UI object not assigned.");
        }

        // Get the camera and movement control scripts
        cameraMovementScript = FindObjectOfType<PlayerLook>();
        movementScript = FindObjectOfType<PlayerMovement>();
    }

    public void Interact()
    {
        // Display the note text when interacting
        if (displayTextComponent != null)
        {
            displayTextComponent.text = noteText;
            textDisplayUI.SetActive(true);

            // Disable camera and movement
            if (cameraMovementScript != null)
                cameraMovementScript.enabled = false;
            if (movementScript != null)
                movementScript.enabled = false;
        }
    }

    private void Update()
    {
        // Check if the note is being displayed and the player wants to close it
        if (textDisplayUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            // Enable and disable specified GameObjects
            if (gameObjectToEnable != null)
                gameObjectToEnable.SetActive(true);
            if (gameObjectToDisable != null)
                gameObjectToDisable.SetActive(false);
            
            textDisplayUI.SetActive(false);

            // Enable camera and movement
            if (cameraMovementScript != null)
                cameraMovementScript.enabled = true;
            if (movementScript != null)
                movementScript.enabled = true;

        }
    }
}