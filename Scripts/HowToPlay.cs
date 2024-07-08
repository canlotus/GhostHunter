using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Button howToPlayButton; // Nas�l oynan�r butonu
    public GameObject howToPlayPanel; // Nas�l oynan�r paneli
    public Text howToPlayText; // Nas�l oynan�r metni
    public Button closeButton; // Kapat butonu

    void Start()
    {
        // Buton olaylar�n� ba�lama
        howToPlayButton.onClick.AddListener(ShowHowToPlayPanel);
        closeButton.onClick.AddListener(HideHowToPlayPanel);

        // Panel ba�lang��ta kapal� olsun
        howToPlayPanel.SetActive(false);

        // Nas�l oynan�r metnini ayarlama
        howToPlayText.text = "Oyun Nas�l Oynan�r:\n\n1. Oyuncular isimlerini girer.\n2. S�rayla telefonu elinize al�p rollerinizi ��reninirsiniz.\n3. Oylama s�ralar�nda 1. Oyuncu tek tek herkese kime oy verece�ini sorar.\n4. S�ras� gelen oyuncular rollerinin eylemlerini ger�ekle�tirir.\n5. Hayaletler, di�er t�m oyuncular� ele ge�irmeye �al���r.\n6. Oyuncular, hayaletleri yakalamaya �al���r.\n\n�yi oyunlar!"
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