using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public Text gameOverText;
    public Button mainMenuButton;

    void Start()
    {
        // Oyun bitiþ koþulunu belirle
        if (AllGhostsEliminated())
        {
            gameOverText.text = "Tüm hayaletler elendi! Hayalet Avcýlarý kazandý!";
        }
        else
        {
            gameOverText.text = "Hayaletler ve diðer roller eþit sayýda veya daha fazla! Hayaletler kazandý!";
        }

        // Ana menü butonunun týklama olayýný ayarla
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        // Ana menü sahnesine geçiþ yap
        SceneManager.LoadScene("MainMenu");
    }

    // Tüm hayaletlerin elenip elenmediðini kontrol eden fonksiyon
    bool AllGhostsEliminated()
    {
        foreach (var playerName in GameData.PlayerNames)
        {
            if (!GameData.EliminatedPlayers.Contains(playerName) && GameData.AssignedCharacters[playerName] == "Ghost")
            {
                return false;
            }
        }
        return true;
    }

    // Hayaletlerin sayýsýnýn diðer rollerin toplam sayýsýna eþit veya daha fazla olup olmadýðýný kontrol eden fonksiyon
    bool GhostsOutnumberOthers()
    {
        int ghostCount = 0;
        int otherRolesCount = 0;

        foreach (var playerName in GameData.PlayerNames)
        {
            if (!GameData.EliminatedPlayers.Contains(playerName))
            {
                if (GameData.AssignedCharacters[playerName] == "Ghost")
                {
                    ghostCount++;
                }
                else if (GameData.AssignedCharacters[playerName] == "Ghost Hunter" ||
                         GameData.AssignedCharacters[playerName] == "Protector" ||
                         GameData.AssignedCharacters[playerName] == "Detector")
                {
                    otherRolesCount++;
                }
            }
        }

        return ghostCount >= otherRolesCount;
    }
}