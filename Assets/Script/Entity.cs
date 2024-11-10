using UnityEngine;

namespace Script
{
    public class Entity : MonoBehaviour
    {
        protected Rigidbody2D Rb;
        protected Animator Anim;

        protected int FacingDirection = 1;
        protected bool FacingRight = true;

        [Header("Collision info")]
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected float groundCheckDistance;
        [SerializeField] protected LayerMask whatIsGround;
        protected bool IsGrounded;

        protected virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            // 碰撞控制
            CollisionChecks();
        }

        protected virtual void CollisionChecks()
        {
            IsGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        }

        protected virtual void Flip()
        {
            FacingDirection = FacingDirection * -1;
            FacingRight = !FacingRight;
            transform.Rotate(0, 180, 0);
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position,
                new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        }
    }
}