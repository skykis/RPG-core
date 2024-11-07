using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moverSpeed;
    public float jumpForce;
    private float xInput; // private时 其他脚本不可见不可用

    // Start is called before the first frame update
    private void Start()
    {
        rb.velocity = new Vector2(5, rb.velocity.y);
    }

    // Update is called once per frame
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * moverSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}