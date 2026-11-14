using UnityEngine;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    [Header("Data Percakapan 2 Arah")]
    [SerializeField] private DialogueLine[] dialogueLines;

    public void Interact()
    {
        if (DialogueManager.Instance != null && !DialogueManager.Instance.IsDialogueActive)
        {
            DialogueManager.Instance.StartDialogue(dialogueLines);
        }
    }
}