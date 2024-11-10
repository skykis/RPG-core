using Script.Common;
using UnityEngine;

namespace Script
{
    public class Player : Entity
    {
        private float xInput; // private时 其他脚本不可见不可用
        
        [Header("Move info")]
        [SerializeField] private float moverSpeed;
        [SerializeField] private float jumpForce;
    
        [Header("Dash info")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;
        [SerializeField] private float dashCooldown;
        private float dashTime;
        private float dashCooldownTimer;

        [Header("Attack info")]
        [SerializeField] private float comboTime;
        private float comboTimeWindow;
        private bool isAttacking;
        private int comboCounter;

        protected override void Update()
        {
            base.Update();
        
            // 输入检测
            CheckInput();
            // 人物方向控制
            FlipController();
            // 动画控制
            AnimatorController();

            dashTime -= Time.deltaTime;
            dashCooldownTimer -= Time.deltaTime;
            comboTimeWindow -= Time.deltaTime;
        }
        
        private void CheckInput()
        {
            xInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                DashAbility();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartAttackEvent();
            }
        }

        private void StartAttackEvent()
        {
            if (!IsGrounded)
            {
                return;
            }
            if (comboTimeWindow < 0)
            {
                comboCounter = 0;
            }
            isAttacking = true;
            comboTimeWindow = comboTime;
        }

        private void DashAbility()
        {
            if (dashCooldownTimer < 0 && !isAttacking)
            {
                dashCooldownTimer = dashCooldown;
                dashTime = dashDuration;
            }
        }

        protected override void Movement()
        {
            if (isAttacking)
            {
                Rb.velocity = new Vector2(0, 0);
            }
            else if (dashTime > 0)
            {
                Rb.velocity = new Vector2(FacingDirection * dashSpeed, 0);
            }
            else
            {
                Rb.velocity = new Vector2(xInput * moverSpeed, Rb.velocity.y);
            }
        }

        private void Jump()
        {
            if (isAttacking)
            {
                return;
            }
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }

        private void AnimatorController()
        {
            var isMoving = Rb.velocity.x != 0;

            Anim.SetFloat(Dict.YVelocity, Rb.velocity.y);

            Anim.SetBool(Dict.IsMoving, isMoving);
            Anim.SetBool(Dict.IsGrounded, IsGrounded);
            Anim.SetBool(Dict.IsDashing, dashTime > 0);
            Anim.SetBool(Dict.IsAttacking, isAttacking);
            Anim.SetInteger(Dict.ComboCounter, comboCounter);
        }

        protected override void FlipController()
        {
            switch (Rb.velocity.x)
            {
                case > 0 when !FacingRight:
                case < 0 when FacingRight:
                    Flip();
                    break;
            }
        }

        public void AttackOver()
        {
            isAttacking = false;
        
            comboCounter++;
            if (comboCounter > 2)
            {
                comboCounter = 0;
            }
        }
    }
}