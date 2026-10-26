using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    [SerializeField] private PlayerController movement; // atau PlayerMovement
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animator.SetBool(IsMoving, movement.Velocity.sqrMagnitude > 0.01f);
        animator.SetFloat(MoveX, movement.Facing.x);
        animator.SetFloat(MoveY, movement.Facing.y);

        // Balik sprite (flip) jika karakter menghadap ke kiri
        if (movement.Facing.x != 0)
        {
            spriteRenderer.flipX = movement.Facing.x < 0;
        }
    }
}