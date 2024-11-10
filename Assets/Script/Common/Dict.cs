using UnityEngine;

namespace Script.Common
{
    public class Dict
    {
        public static readonly int IsMoving = Animator.StringToHash("isMoving");
        public static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        public static readonly int YVelocity = Animator.StringToHash("yVelocity");
        public static readonly int IsDashing = Animator.StringToHash("isDashing");
        public static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        public static readonly int ComboCounter = Animator.StringToHash("comboCounter");
    }
}