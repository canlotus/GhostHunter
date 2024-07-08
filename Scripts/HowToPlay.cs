using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Button howToPlayButton; // Nasýl oynanýr butonu
    public GameObject howToPlayPanel; // Nasýl oynanýr paneli
    public Text howToPlayText; // Nasýl oynanýr metni
    public Button closeButton; // Kapat butonu

    void Start()
    {
        // Buton olaylarýný baðlama
        howToPlayButton.onClick.AddListener(ShowHowToPlayPanel);
        closeButton.onClick.AddListener(HideHowToPlayPanel);

        // Panel baþlangýçta kapalý olsun
        howToPlayPanel.SetActive(false);

        // Nasýl oynanýr metnini ayarlama
        howToPlayText.text = "Oyun Nasýl Oynanýr:\n\n1. Oyuncular isimlerini girer.\n2. Sýrayla telefonu elinize alýp rollerinizi öðreninirsiniz.\n3. Oylama sýralarýnda 1. Oyuncu tek tek herkese kime oy vereceðini sorar.\n4. Sýrasý gelen oyuncular rollerinin eylemlerini gerçekleþtirir.\n5. Hayaletler, diðer tüm oyuncularý ele geçirmeye çalýþýr.\n6. Oyuncular, hayaletleri yakalamaya çalýþýr.\n\nÝyi oyunlar!"
;
    }

    void ShowHowToPlayPanel()
    {
        howToPlayPanel.SetActive(true);
    }

    void HideHowToPlayPanel()
    {
        howToPlayPanel.SetActive(false);
    }
}