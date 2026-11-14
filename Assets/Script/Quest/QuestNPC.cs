using UnityEngine;

// Polimorfisme, QuestNPC mengimplementasikan interface IInteractable
public class QuestNPC : MonoBehaviour, IInteractable
{
    [Header("NPC Profile")]
    [SerializeField] private string npcName = "Tetua Desa";

    [Header("1. Dialog Sebelum Quest Dimulai")]
    [SerializeField] private DialogueLine[] initialDialogue = new DialogueLine[]
    {
        new DialogueLine { speakerName = "Tetua Desa", sentence = "Halo Fathir! Selamat datang." },
        new DialogueLine { speakerName = "Fathir", sentence = "Ada yang bisa saya bantu, Mbah?" },
        new DialogueLine { speakerName = "Tetua Desa", sentence = "Desa ini kehilangan 5 artefak kuno. Maukah kamu mencarikannya?" }
    };

    [Header("2. Dialog Saat Masih Mencari Barang")]
    [SerializeField] private DialogueLine[] inProgressDialogue = new DialogueLine[]
    {
        new DialogueLine { speakerName = "Tetua Desa", sentence = "Kamu belum menemukan ke 5 artefak tersebut." },
        new DialogueLine { speakerName = "Tetua Desa", sentence = "Coba periksa seluruh sudut ruangan ini ya!" }
    };

    [Header("3. Dialog Setelah 5 Barang Terkumpul")]
    [SerializeField] private DialogueLine[] completedDialogue = new DialogueLine[]
    {
        new DialogueLine { speakerName = "Tetua Desa", sentence = "Luar biasa! Kamu berhasil menemukan ke 5 artefak tersebut!" },
        new DialogueLine { speakerName = "Fathir", sentence = "Sama-sama Mbah, senang bisa membantu!" }
    };

    public void Interact()
    {
        // Cegah interaksi ganda kalo dialog atau pop up barang sedang aktif
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive) return;
        if (ItemPopupUI.Instance != null && ItemPopupUI.Instance.IsPopupActive) return;

        if (QuestManager.Instance != null)
        {
            if (!QuestManager.Instance.IsQuestStarted)
            {
                // Pertama kali bicara, aktifin quest dan tampilkan dialog awal
                QuestManager.Instance.StartQuest();
                DialogueManager.Instance.StartDialogue(initialDialogue);
            }
            else if (!QuestManager.Instance.IsQuestCompleted)
            {
                // Quest sedang berjalan tapi 5 barang belum lengkap
                DialogueManager.Instance.StartDialogue(inProgressDialogue);
            }
            else
            {
                // Quest sudah selesai (5 barang terkumpul)
                DialogueManager.Instance.StartDialogue(completedDialogue);
            }
        }
    }
}