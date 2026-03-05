using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için bu kütüphane şart

public class SahneYoneticisi : MonoBehaviour
{
    public void PenguenSahnesineGit()
    {
        Debug.Log("Penguen sahnesine geçiliyor...");
        // Sahne adının Project penceresindekiyle birebir aynı (küçük harf) olduğuna dikkat et
        SceneManager.LoadScene("penguen"); 
    }

    public void KittySahnesineGit()
    {
        Debug.Log("Kitty sahnesine geçiliyor...");
        SceneManager.LoadScene("kitty");
    }
}