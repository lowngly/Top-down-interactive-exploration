using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Memanggil namespace InputSystem untuk mengakses input mouse
using TMPro; // Memanggil namespace TextMeshPro

public class DialogueManager : MonoBehaviour
{
    // Singleton sederhana agar mudah dipanggil dari script mana saja
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private bool isDialogueActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Sembunyikan kotak dialog saat game baru dimulai
        HideDialogue();
    }

    private void Update()
    {
        // Jika dialog aktif dan pemain menekan tombol E, sembunyikan dialog
        if (isDialogueActive && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HideDialogue();
        }
    }

    public void ShowDialogue(string text)
    {
        dialogueText.text = text;
        dialogueBox.SetActive(true);
        isDialogueActive = true;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}