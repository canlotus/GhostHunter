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
        // Butonlara t�klama olaylar�n� ekleyin
        howToPlayButton.onClick.AddListener(ShowHowToPlay);
        closeButton.onClick.AddListener(HideHowToPlay);

        // "Nas�l Oynan�r" metnini ayarlay�n
        howToPlayText.text = "Oyun Nas�l Oynan�r:\n\n1. Oyuncular isimlerini girer.\n2. S�rayla telefonu elinize al�p rollerinizi ��reninirsiniz.\n3. Oylama s�ralar�nda 1. Oyuncu tek tek herkese kime oy verece�ini sorar.\n4. S�ras� gelen oyuncular rollerinin eylemlerini ger�ekle�tirir.\n5. Hayaletler, di�er t�m oyuncular� ele ge�irmeye �al���r.\n6. Oyuncular, hayaletleri yakalamaya �al���r.\n\n�yi oyunlar!";

        // Panel ba�lang��ta kapal� olmal�
        howToPlayPanel.SetActive(false);

        // AudioManager'� bulun
        audioManager = AudioManager.instance;

        // Slider'�n ba�lang�� de�erini ayarla
        if (audioManager != null)
        {
            volumeSlider.value = audioManager.backgroundMusic.volume;
        }

        // Slider'�n de�i�im olay�n� ekleyin
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