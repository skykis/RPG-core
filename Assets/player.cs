using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moverSpeed;
    [SerializeField] private float jumpForce;

    private float xInput; // private时 其他脚本不可见不可用

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        CheckInput();

        AnimatorController();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moverSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorController()
    {
        anim.SetBool(IsMoving, rb.velocity.x != 0);
    }
}