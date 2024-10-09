using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Kecepatan horizontal
    public float jumpForce = 7f;  // Kekuatan lompat
    private Rigidbody2D rb;  // Reference ke Rigidbody2D
    private bool isGrounded;  // Cek apakah karakter di tanah
    public bool facingRight = true;  // Arah karakter
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        // Gerakan horizontal
        float moveInput = Input.GetAxis("Horizontal");

        // Mengatur animasi lari: hanya aktif jika ada input gerakan
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);  // Set animasi lari ke false jika tidak ada input
        }

        // Menggerakkan karakter
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Balik karakter saat bergerak
        if (moveInput > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    void Jump()
    {
        // Melompat
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger("IsJump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek jika karakter menyentuh tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Mengecek jika karakter meninggalkan tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Fungsi untuk membalik arah karakter
    void FlipCharacter()
    {
        facingRight = !facingRight;  // Balik nilai arah karakter
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;  // Membalik skala karakter
        transform.localScale = scaler;
    }
}
