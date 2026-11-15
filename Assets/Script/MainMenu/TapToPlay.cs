using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TapToPlay : MonoBehaviour, IPointerClickHandler
{
    [Header("Pengaturan Scene")]
    public string namaSceneGame = "NamaSceneGameplayKamu";

    [Header("Efek Kedip Teks")]
    public Text teksTap;
    public float kecepatanKedip = 3f;

    private bool sudahDitekan = false;

    void Update()
    {
        // Membuat Legacy Text berkedip halus saat menunggu input
        if (teksTap != null)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * kecepatanKedip));
            Color warna = teksTap.color;
            warna.a = alpha;
            teksTap.color = warna;
        }
    }

    // Mendeteksi klik mouse atau sentuhan layar di mana saja pada TapArea
    public void OnPointerClick(PointerEventData eventData)
    {
        if (sudahDitekan) return;
        sudahDitekan = true;

        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.PindahScene(namaSceneGame);
        }
        else
        {
            SceneManager.LoadScene(namaSceneGame);
        }
    }
}