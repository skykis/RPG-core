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
        [Space]
        [SerializeField] protected Transform wallCheck;
        [SerializeField] protected float wallCheckDistance;
        
        protected bool IsGrounded;
        protected bool IsWallDetected;

        protected virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponentInChildren<Animator>();

            if (wallCheck == null)
            {
                wallCheck = transform;
            }
        }

        protected virtual void Update()
        {
            // 移动
            Movement();
            // 碰撞控制
            CollisionChecks();
            // 人物方向控制
            FlipController();
        }

        protected virtual void Movement()
        {
        }
        protected virtual void FlipController()
        {
        }
        
        protected virtual void CollisionChecks()
        {
            IsGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            IsWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * FacingDirection,
                whatIsGround);
        }

        protected void Flip()
        {
            FacingDirection = FacingDirection * -1;
            FacingRight = !FacingRight;
            transform.Rotate(0, 180, 0);
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position,
                new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.DrawLine(wallCheck.position,
                new Vector3(wallCheck.position.x + wallCheckDistance * FacingDirection, wallCheck.position.y));
        }
    }
}