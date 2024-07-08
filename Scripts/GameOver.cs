using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public Text gameOverText;
    public Button mainMenuButton;

    void Start()
    {
        // Oyun biti� ko�ulunu belirle
        if (AllGhostsEliminated())
        {
            gameOverText.text = "T�m hayaletler elendi! Hayalet Avc�lar� kazand�!";
        }
        else
        {
            gameOverText.text = "Hayaletler ve di�er roller e�it say�da veya daha fazla! Hayaletler kazand�!";
        }

        // Ana men� butonunun t�klama olay�n� ayarla
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        // Ana men� sahnesine ge�i� yap
        SceneManager.LoadScene("MainMenu");
    }

    // T�m hayaletlerin elenip elenmedi�ini kontrol eden fonksiyon
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

    // Hayaletlerin say�s�n�n di�er rollerin toplam say�s�na e�it veya daha fazla olup olmad���n� kontrol eden fonksiyon
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