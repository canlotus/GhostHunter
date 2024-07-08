using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public Button returnToMainMenuButton; // Ana Men�'ye d�n butonu
    public GameObject returnToMainMenuPanel; // Ana Men�'ye d�n paneli
    public Text returnToMainMenuText; // Ana Men�'ye d�n metni
    public Button yesButton; // Evet butonu
    public Button noButton; // Hay�r butonu

    void Start()
    {
        // Buton olaylar�n� ba�lama
        returnToMainMenuButton.onClick.AddListener(ShowReturnToMainMenuPanel);
        yesButton.onClick.AddListener(ReturnToMainMenuConfirmed);
        noButton.onClick.AddListener(HideReturnToMainMenuPanel);

        // Panel ba�lang��ta kapal� olsun
        returnToMainMenuPanel.SetActive(false);

        // Ana Men�'ye d�n metnini ayarlama
        returnToMainMenuText.text = "Ana Men�'ye d�nmek istiyor musun?";
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