using UnityEngine;

public class QuestItem : MonoBehaviour, IInteractable
{
    [Header("Item Info")]
    [SerializeField] private string itemName = "Artefak Kuno";
    [SerializeField] private Sprite itemSprite;

    [Header("Locked Dialogue")]
    [SerializeField] private DialogueLine[] lockedDialogue = new DialogueLine[]
    {
        new DialogueLine { speakerName = "Fathir", sentence = "Sebuah barang kuno yang aneh..." },
        new DialogueLine { speakerName = "Fathir", sentence = "Aku harus bicara dengan warga desa dulu sebelum mengambilnya." }
    };

    public void Interact()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive) return;
        if (ItemPopupUI.Instance != null && ItemPopupUI.Instance.IsPopupActive) return;

        // Cek apakah quest udah diaktifkan oleh NPC
        if (QuestManager.Instance != null && QuestManager.Instance.IsQuestStarted)
        {
            // Catat barang ke QuestManager n buka pop up UI
            QuestManager.Instance.CollectItem(itemName, itemSprite);

            // Sembunyikan barang dari map
            gameObject.SetActive(false);
        }
        else
        {
            // Tampilin dialog tertutup jika dipencet sebelum bicara ke NPC
            if (DialogueManager.Instance != null && lockedDialogue.Length > 0)
            {
                DialogueManager.Instance.StartDialogue(lockedDialogue);
            }
        }
    }
}