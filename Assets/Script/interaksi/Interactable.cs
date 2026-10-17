using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string dialogueMessage = "Halo Fathir! Ini adalah pesan eksplorasi.";
    [SerializeField] private GameObject interactPrompt;
    private bool isPlayerNearby = false;

    private void Start()
    {
        // Sembunyikan logo tombol E saat game dimulai
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        // Menekan tombol E saat berada di area sensor
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (DialogueManager.Instance != null && !DialogueManager.Instance.IsDialogueActive())
            {
                DialogueManager.Instance.ShowDialogue(dialogueMessage);

                // Sembunyikan logo tombol E saat dialog muncul
                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(false);
                }
            }
        }

        // Kalau dialog ditutup tapi pemain masih di area interaksi, munculkan logo tombol E lagi
        if (isPlayerNearby && DialogueManager.Instance != null && !DialogueManager.Instance.IsDialogueActive())
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
            Debug.Log("Kamu memasuki area interaksi! Tekan E untuk bicara.");

            // Munculkan logo tombol E saat pemain masuk area interaksi
            if (interactPrompt != null)
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

            // Sembunyikan logo tombol E saat pemain keluar area interaksi
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
            
            // Otomatis menutup dialog jika pemain berjalan keluar area
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.HideDialogue();
            }
        }
    }
}