using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerNameDisplay : MonoBehaviour
{
    public Text playerNameText;
    public Text playerRoleText;
    public Button roleButton;
    public Button nextPlayerButton;
    public Button ileriButton; // Yeni buton ekliyoruz

    private List<string> playerNames;
    private Dictionary<string, string> assignedCharacters;
    private int currentPlayerIndex = 0;

    void Start()
    {
        // GameData'dan oyuncu isimlerini ve rollerini çek
        playerNames = GameData.PlayerNames;
        assignedCharacters = GameData.AssignedCharacters;

        // Ýlk oyuncunun bilgilerini göster
        ShowPlayerName();

        // Ýlk butona týklama olayýný ekle
        roleButton.onClick.AddListener(() =>
        {
            // Ýlk butonu kapat, ikinci butonu aç
            roleButton.gameObject.SetActive(false);
            nextPlayerButton.gameObject.SetActive(true);
            // Oyuncu ismini ve rolünü göster
            ShowPlayerInfo(currentPlayerIndex);
        });

        // Ýkinci butona týklama olayýný ekle
        nextPlayerButton.onClick.AddListener(() =>
        {
            // Bir sonraki oyuncuya geç
            currentPlayerIndex++;
            // Eðer tüm oyuncularý gösterdikten sonra oy verme sahnesine geçiþ yap
            if (currentPlayerIndex < playerNames.Count)
            {
                // Ýkinci butonu kapat, ilk butonu aç
                nextPlayerButton.gameObject.SetActive(false);
                roleButton.gameObject.SetActive(true);
                // Oyuncu ismini göster
                ShowPlayerName();
            }
            else
            {
                // Tüm oyuncular rollerini öðrendiðinde oy verme butonunu aç
                nextPlayerButton.gameObject.SetActive(false);
                ileriButton.gameObject.SetActive(true);
            }
        });

        // Oy verme butonuna týklama olayýný ekle
        ileriButton.onClick.AddListener(() =>
        {
            // Oy verme sahnesine geç
            SceneManager.LoadScene("DiscussionScene");
        });

        // Ýkinci buton ve oy verme butonu baþlangýçta kapalý olsun
        nextPlayerButton.gameObject.SetActive(false);
        ileriButton.gameObject.SetActive(false);
    }

    void ShowPlayerName()
    {
        playerNameText.text = "Oyuncu: " + playerNames[currentPlayerIndex];
        // RoleButton týklandýðýnda aktif hale gelmeli
        nextPlayerButton.interactable = true;
        // PlayerRoleText'i temizle
        playerRoleText.text = "";
    }

    void ShowPlayerInfo(int index)
    {
        playerRoleText.text = "Rol: " + assignedCharacters[playerNames[index]];
    }
}