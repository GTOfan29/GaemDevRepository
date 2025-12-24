using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private float speed = 50000f; // Hızı buradan değiştirebilirsin

    void Awake()
    {
        // Kontrol sınıfını hafızaya al
        controls = new PlayerControls();
    }

    void Update()
    {
        // 1. Mevcut yönü oku (W-A-S-D'den gelen Vector2)
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();

        // 2. Eğer bir tuşa basılıyorsa hareket et
        if (moveInput != Vector2.zero)
        {
            Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    // ÇOK ÖNEMLİ: Kontrolleri aktif etmezsen hiçbir tuş çalışmaz!
    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}