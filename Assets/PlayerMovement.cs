using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15f; 
    public float jumpForce = 150f; // Senin istediğin devasa güç
    public LayerMask zeminKatmani;

    private float moveInputX;
    private Rigidbody2D rb;
    private bool isFacingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInputX = value.Get<float>(); 
    }

    void OnJump(InputValue value)
    {
        if (IsGrounded())
        {
            // ÇÖZÜM BURADA:
            // Yatay hızı (X), normal yürüme hızının 3'te 2'sine (0.66) düşürüyoruz.
            // Böylece çapraz zıplarken kontrolsüzce fırlamaz.
            float horizontalJumpSpeed = moveInputX * moveSpeed * 0.66f;
            
            rb.linearVelocity = new Vector2(horizontalJumpSpeed, jumpForce);
            
            Debug.Log("Sınırlı Çapraz Zıplama!");
        }
    }

    void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(moveInputX * moveSpeed, rb.linearVelocity.y);
        }
        // else kısmını boş bıraktık; havada hareket engelli.
        
        FlipCharacter(); 
    }

    bool IsGrounded()
    {
        return rb.IsTouchingLayers(zeminKatmani);
    }

    void FlipCharacter()
    {
        if (moveInputX > 0 && !isFacingRight) Flip();
        else if (moveInputX < 0 && isFacingRight) Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight; 
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f; 
        transform.localScale = localScale;
    }
}