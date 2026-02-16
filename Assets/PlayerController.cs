using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private float speed = 5f; // Hızı buradan değiştirebilirsin

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


public class ControllerDegistir : MonoBehaviour
{
    // Inspector'dan atayacağın yeni Animator Controller dosyası
    public RuntimeAnimatorController yeniController; 

    // Geri dönmek istersen diye eski controller'ı saklamak için
    private RuntimeAnimatorController eskiController;
    
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Başlangıçtaki controller'ı hafızaya alalım
        eskiController = anim.runtimeAnimatorController;
    }

    void Update()
    {
        // 'K' tuşuna basınca yeni controller'a geç
        if (Input.GetKeyDown(KeyCode.K))
        {
            ControlleriGuncelle(yeniController);
        }
        
        // 'L' tuşuna basınca eskiye dön
        if (Input.GetKeyDown(KeyCode.L))
        {
            ControlleriGuncelle(eskiController);
        }
    }

    void ControlleriGuncelle(RuntimeAnimatorController hedefController)
    {
        // Animator'ün beynini tamamen değiştiriyoruz
        anim.runtimeAnimatorController = hedefController;
    }
}