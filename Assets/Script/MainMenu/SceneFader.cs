using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [Header("Referensi UI")]
    public Image layarHitam;
    public float durasiFade = 0.6f;

    private bool sedangTransisi = false;

    void Awake()
    {
        // Menjaga objek biar FadeCanvas tetep hidup pas pindah scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SaatSceneDimuat;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SaatSceneDimuat;
    }

    // Otomatis memudarkan layar hitam kembali jadi terang pas scene baru kebuka
    private void SaatSceneDimuat(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ProsesFade(1f, 0f));
    }

    public void PindahScene(string namaScene)
    {
        if (sedangTransisi) return;
        StartCoroutine(TransisiKeScene(namaScene));
    }

    private IEnumerator TransisiKeScene(string namaScene)
    {
        sedangTransisi = true;

        // 1. Layar meredup menjadi hitam (Fade Out)
        yield return StartCoroutine(ProsesFade(0f, 1f));

        // 2. Pindah ke scene tujuan
        SceneManager.LoadScene(namaScene);

        sedangTransisi = false;
    }

    private IEnumerator ProsesFade(float alphaAwal, float alphaAkhir)
    {
        if (layarHitam == null) yield break;

        // Blokir klik selama layar sedang gelap
        layarHitam.raycastTarget = true;

        float waktu = 0f;
        Color warna = layarHitam.color;

        while (waktu < durasiFade)
        {
            waktu += Time.unscaledDeltaTime;
            warna.a = Mathf.Lerp(alphaAwal, alphaAkhir, waktu / durasiFade);
            layarHitam.color = warna;
            yield return null;
        }

        warna.a = alphaAkhir;
        layarHitam.color = warna;

        // Buka kembali akses klik kalo layar sudah transparan penuh
        layarHitam.raycastTarget = (alphaAkhir > 0.01f);
    }
}