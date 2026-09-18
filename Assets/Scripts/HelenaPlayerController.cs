using UnityEngine;
using UnityEngine.InputSystem;

namespace HelenaGame
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(SpriteRenderer))]
    public sealed class HelenaPlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpSpeed = 8.5f;
        [SerializeField] private float groundCheckDistance = 0.12f;

        private Rigidbody2D body;
        private CapsuleCollider2D capsule;
        private SpriteRenderer spriteRenderer;
        private float horizontalInput;
        private bool jumpRequested;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            capsule = GetComponent<CapsuleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return;

            horizontalInput = 0f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontalInput -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontalInput += 1f;

            if (horizontalInput != 0f)
            {
                spriteRenderer.flipX = horizontalInput < 0f;
            }

            if (keyboard.spaceKey.wasPressedThisFrame && IsGrounded())
            {
                jumpRequested = true;
            }
        }

        private void FixedUpdate()
        {
            Vector2 velocity = body.linearVelocity;
            velocity.x = horizontalInput * moveSpeed;

            if (jumpRequested)
            {
                velocity.y = jumpSpeed;
                jumpRequested = false;
            }

            body.linearVelocity = velocity;
        }

        private bool IsGrounded()
        {
            Bounds bounds = capsule.bounds;
            Vector2 origin = new Vector2(bounds.center.x, bounds.min.y + 0.02f);
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, Vector2.down, groundCheckDistance);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider != capsule)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
