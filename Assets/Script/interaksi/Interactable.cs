using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject interactPrompt;
    
    private IInteractable interactableComponent;
    private bool isPlayerNearby = false;

    private void Awake()
    {
        // ngambil komponen yang mengimplementasikan IInteractable yang ada di GameObject ini (Polimorfisme)
        interactableComponent = GetComponent<IInteractable>();
    }

    private void Start()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        bool isDialogueActive = DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive;

        // menekan tombol F pas dekat di area sensor
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (!isDialogueActive)
            {
                // Panggil method Interact() secara polymorphic
                interactableComponent?.Interact();

                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(false);
                }
            }
        }

        // tampilin logo tombol F lagi jika dialog udah ditutup dan pemain masih di area interaksi
        if (isPlayerNearby && !isDialogueActive)
        {
            if (interactPrompt != null && !interactPrompt.activeSelf)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Kamu memasuki area interaksi! Tekan F untuk bicara.");

            if (interactPrompt != null && (DialogueManager.Instance == null || !DialogueManager.Instance.IsDialogueActive))
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }

            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.HideDialogue();
            }
        }
    }
}