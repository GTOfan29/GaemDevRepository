using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Değişkenler
    private PlayerControls controls;
    private Rigidbody2D rb;
    private float moveX;

    [Header("Ayarlar")]
    public float moveSpeed = 10f;
    public float jumpForce = 12f;

    [Header("Yer Kontrolü")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // ÖNEMLİ: Eğer dosyanın adı farklıysa burayı güncelle!
        controls = new PlayerControls();

        // Zıplama olayını bağlıyoruz
        controls.Player.Jump.performed += OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void Update()
    {
        // Hareket değerini oku
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        moveX = input.x;

        // Karakterin yönünü çevir
        if (moveX > 0.1f) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (moveX < -0.1f) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    private void FixedUpdate()
    {
        // Fizik hareketi
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        // GroundCheck objesi atanmamışsa hata vermemesi için kontrol
        if (groundCheck == null) return false;
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}