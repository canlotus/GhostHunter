using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ResultScreen : MonoBehaviour
{
    public Text resultText;
    public Text playerRoleText;
    public Text otherText;
    public Text tieText;
    public Text blankVoteText;
    public Button nextDaySceneButton;

    void Start()
    {
        Dictionary<string, int> playerVotes = GameData.PlayerVotes;
        int blankVoteCount = GameData.BlankVoteCount;

        int maxVotes = 0;
        List<string> highestVotedPlayers = new List<string>();

        // En �ok oy alan oyuncu(lar) ve maksimum oy say�s�n� bul
        foreach (var playerVote in playerVotes)
        {
            if (playerVote.Value > maxVotes)
            {
                maxVotes = playerVote.Value;
                highestVotedPlayers.Clear();
                highestVotedPlayers.Add(playerVote.Key);
            }
            else if (playerVote.Value == maxVotes)
            {
                highestVotedPlayers.Add(playerVote.Key);
            }
        }

        // Sonu�lara g�re metinleri g�ncelle
        if (highestVotedPlayers.Count == 1 && highestVotedPlayers[0] != null && blankVoteCount < maxVotes)
        {
            string highestVotedPlayer = highestVotedPlayers[0];
            resultText.text = "En �ok oy alan oyuncu: " + highestVotedPlayer;
            playerRoleText.text = "Rol�: " + GameData.AssignedCharacters[highestVotedPlayer];
            otherText.gameObject.SetActive(true);
            tieText.gameObject.SetActive(false);
            blankVoteText.gameObject.SetActive(false);

            // Elenen oyuncuyu ekle
            GameData.EliminatedPlayers.Add(highestVotedPlayer);
        }
        else if (blankVoteCount >= maxVotes || blankVoteCount == playerVotes.Count)
        {
            resultText.text = "En �ok bo� oy kullan�ld� veya bo� oy say�s� oyuncu say�s�na e�it. Kimse Elenmedi";
            blankVoteText.gameObject.SetActive(true);
            tieText.gameObject.SetActive(false);
            otherText.gameObject.SetActive(false);
            playerRoleText.gameObject.SetActive(false);
        }
        else if (highestVotedPlayers.Count > 1)
        {
            resultText.text = "Oylarda e�itlik var. Kimse Elenmedi.";
            tieText.gameObject.SetActive(true);
            blankVoteText.gameObject.SetActive(false);
            otherText.gameObject.SetActive(false);
            playerRoleText.gameObject.SetActive(false);
        }

        // Oyunun biti� ko�ullar�n� kontrol et
        if (CheckGameOver())
        {
            // Game Over sahnesine ge�
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Next Day scene ge�i�i i�in butonun t�klama olay�n� ayarla
            nextDaySceneButton.onClick.AddListener(GoToNextDayScene);
        }
    }

    // Next Day scene ge�i�i fonksiyonu
    void GoToNextDayScene()
    {
        // GameData'daki oyuncu oylar�n� s�f�rlama
        GameData.PlayerVotes.Clear();
        GameData.BlankVoteCount = 0;

        // Next Day scene'e ge�i� yap
        SceneManager.LoadScene("NextDayScene");
    }

    // Oyunun biti� ko�ullar�n� kontrol eden fonksiyon
    bool CheckGameOver()
    {
        int ghostCount = 0;
        int otherCount = 0;

        foreach (var player in GameData.PlayerNames)
        {
            if (!GameData.EliminatedPlayers.Contains(player))
            {
                if (GameData.AssignedCharacters[player] == "Ghost")
                {
                    ghostCount++;
                }
                else
                {
                    otherCount++;
                }
            }
        }

        // Biti� ko�ullar�: t�m Ghost'lar elenmi�se veya Ghost say�s� di�er oyuncular�n toplam�ndan fazlaysa
        if (ghostCount == 0 || ghostCount >= otherCount)
        {
            return true;
        }

        return false;
    }
}
