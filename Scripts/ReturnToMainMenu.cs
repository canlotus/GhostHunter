using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public Button returnToMainMenuButton; // Ana Menü'ye dön butonu
    public GameObject returnToMainMenuPanel; // Ana Menü'ye dön paneli
    public Text returnToMainMenuText; // Ana Menü'ye dön metni
    public Button yesButton; // Evet butonu
    public Button noButton; // Hayýr butonu

    void Start()
    {
        // Buton olaylarýný baðlama
        returnToMainMenuButton.onClick.AddListener(ShowReturnToMainMenuPanel);
        yesButton.onClick.AddListener(ReturnToMainMenuConfirmed);
        noButton.onClick.AddListener(HideReturnToMainMenuPanel);

        // Panel baþlangýçta kapalý olsun
        returnToMainMenuPanel.SetActive(false);

        // Ana Menü'ye dön metnini ayarlama
        returnToMainMenuText.text = "Ana Menü'ye dönmek istiyor musun?";
    }

    void ShowReturnToMainMenuPanel()
    {
        returnToMainMenuPanel.SetActive(true);
    }

    void HideReturnToMainMenuPanel()
    {
        returnToMainMenuPanel.SetActive(false);
    }

    void ReturnToMainMenuConfirmed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}