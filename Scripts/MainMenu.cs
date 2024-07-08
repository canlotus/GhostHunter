using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public Button howToPlayButton;
    public Button closeButton;
    public Text howToPlayText;
    public Slider volumeSlider;

    private AudioManager audioManager;

    void Start()
    {
        // Butonlara týklama olaylarýný ekleyin
        howToPlayButton.onClick.AddListener(ShowHowToPlay);
        closeButton.onClick.AddListener(HideHowToPlay);

        // "Nasýl Oynanýr" metnini ayarlayýn
        howToPlayText.text = "Oyun Nasýl Oynanýr:\n\n1. Oyuncular isimlerini girer.\n2. Sýrayla telefonu elinize alýp rollerinizi öðreninirsiniz.\n3. Oylama sýralarýnda 1. Oyuncu tek tek herkese kime oy vereceðini sorar.\n4. Sýrasý gelen oyuncular rollerinin eylemlerini gerçekleþtirir.\n5. Hayaletler, diðer tüm oyuncularý ele geçirmeye çalýþýr.\n6. Oyuncular, hayaletleri yakalamaya çalýþýr.\n\nÝyi oyunlar!";

        // Panel baþlangýçta kapalý olmalý
        howToPlayPanel.SetActive(false);

        // AudioManager'ý bulun
        audioManager = AudioManager.instance;

        // Slider'ýn baþlangýç deðerini ayarla
        if (audioManager != null)
        {
            volumeSlider.value = audioManager.backgroundMusic.volume;
        }

        // Slider'ýn deðiþim olayýný ekleyin
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerSetup");
    }

    void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    void SetVolume(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetVolume(volume);
        }
    }
}