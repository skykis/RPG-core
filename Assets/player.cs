using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int ComboCounter = Animator.StringToHash("comboCounter");

    private Rigidbody2D rb;
    private Animator anim;
    
    private float xInput; // private时 其他脚本不可见不可用
    private int facingDirection = 1;
    private bool facingRight = true;
    
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
    
    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // 移动
        Movement();
        // 输入检测
        CheckInput();
        // 碰撞控制
        CollisionChecks();
        // 人物方向控制
        FlipController();
        // 动画控制
        AnimatorController();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
        if (!isGrounded)
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

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moverSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isAttacking)
        {
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorController()
    {
        var isMoving = rb.velocity.x != 0;

        anim.SetFloat(YVelocity, rb.velocity.y);

        anim.SetBool(IsMoving, isMoving);
        anim.SetBool(IsGrounded, isGrounded);
        anim.SetBool(IsDashing, dashTime > 0);
        anim.SetBool(IsAttacking, isAttacking);
        anim.SetInteger(ComboCounter, comboCounter);
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        switch (rb.velocity.x)
        {
            case > 0 when !facingRight:
            case < 0 when facingRight:
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