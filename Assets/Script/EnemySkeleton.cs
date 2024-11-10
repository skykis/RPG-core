using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class EnemySkeleton : Entity
    {
        private bool isAttacking;
        
        [Header("Move info")]
        [SerializeField] private float moverSpeed;

        [Header("Player detection")]
        [SerializeField] private float playerCheckDistance;
        [SerializeField] private LayerMask whatIsPlayer;

        private RaycastHit2D isPlayerDetected;
        
        protected override void Update()
        {
            base.Update();

            if (isPlayerDetected)
            {
                if (isPlayerDetected.distance > 1)
                {
                    Debug.Log("Player detected");
                    Rb.velocity = new Vector2(moverSpeed * FacingDirection * 2f, Rb.velocity.y);
                    isAttacking = false;
                }
                else
                {
                    Debug.Log("Attack!" + isPlayerDetected.collider.gameObject.name);
                    isAttacking = true;
                }
            }
        }

        protected override void Movement()
        {
            Rb.velocity = new Vector2(moverSpeed * FacingDirection, Rb.velocity.y);
        }

        protected override void FlipController()
        {
            if (!IsGrounded || IsWallDetected)
            {
                Flip();
            }
        }

        protected override void CollisionChecks()
        {
            base.CollisionChecks();

            isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right,
                playerCheckDistance * FacingDirection, whatIsPlayer);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x + playerCheckDistance * FacingDirection, transform.position.y));
        }
    }
}