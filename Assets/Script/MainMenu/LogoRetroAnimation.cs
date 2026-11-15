using UnityEngine;

public class LogoRetroBob : MonoBehaviour
{
    public float jarakPiksel = 4f;     // ini tinggi naik turun dalam piksel
    public float kecepatan = 2.5f;     // Kecepatan naik turunnya
    public float pixelsPerUnit = 32f;  // Sesuai setting sprite logo (32 PPU)

    private Vector3 posisiAwal;

    void Start()
    {
        posisiAwal = transform.localPosition;
    }

    void Update()
    {
        // Menghitung gelombang naik turun lalu dibulatkan ke grid piksel
        float gelombang = Mathf.Sin(Time.time * kecepatan) * jarakPiksel;
        float offsetY = Mathf.Round(gelombang) / pixelsPerUnit;

        transform.localPosition = posisiAwal + new Vector3(0f, offsetY, 0f);
    }
}