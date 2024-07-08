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
        // GameData'dan oyuncu isimlerini ve rollerini �ek
        playerNames = GameData.PlayerNames;
        assignedCharacters = GameData.AssignedCharacters;

        // �lk oyuncunun bilgilerini g�ster
        ShowPlayerName();

        // �lk butona t�klama olay�n� ekle
        roleButton.onClick.AddListener(() =>
        {
            // �lk butonu kapat, ikinci butonu a�
            roleButton.gameObject.SetActive(false);
            nextPlayerButton.gameObject.SetActive(true);
            // Oyuncu ismini ve rol�n� g�ster
            ShowPlayerInfo(currentPlayerIndex);
        });

        // �kinci butona t�klama olay�n� ekle
        nextPlayerButton.onClick.AddListener(() =>
        {
            // Bir sonraki oyuncuya ge�
            currentPlayerIndex++;
            // E�er t�m oyuncular� g�sterdikten sonra oy verme sahnesine ge�i� yap
            if (currentPlayerIndex < playerNames.Count)
            {
                // �kinci butonu kapat, ilk butonu a�
                nextPlayerButton.gameObject.SetActive(false);
                roleButton.gameObject.SetActive(true);
                // Oyuncu ismini g�ster
                ShowPlayerName();
            }
            else
            {
                // T�m oyuncular rollerini ��rendi�inde oy verme butonunu a�
                nextPlayerButton.gameObject.SetActive(false);
                ileriButton.gameObject.SetActive(true);
            }
        });

        // Oy verme butonuna t�klama olay�n� ekle
        ileriButton.onClick.AddListener(() =>
        {
            // Oy verme sahnesine ge�
            SceneManager.LoadScene("DiscussionScene");
        });

        // �kinci buton ve oy verme butonu ba�lang��ta kapal� olsun
        nextPlayerButton.gameObject.SetActive(false);
        ileriButton.gameObject.SetActive(false);
    }

    void ShowPlayerName()
    {
        playerNameText.text = "Oyuncu: " + playerNames[currentPlayerIndex];
        // RoleButton t�kland���nda aktif hale gelmeli
        nextPlayerButton.interactable = true;
        // PlayerRoleText'i temizle
        playerRoleText.text = "";
    }

    void ShowPlayerInfo(int index)
    {
        playerRoleText.text = "Rol: " + assignedCharacters[playerNames[index]];
    }
}