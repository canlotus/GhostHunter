using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NewResultScene : MonoBehaviour
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

        // En çok oy alan oyuncu(lar) ve maksimum oy sayýsýný bul
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

        // Sonuçlara göre metinleri güncelle
        if (highestVotedPlayers.Count == 1 && highestVotedPlayers[0] != null && blankVoteCount < maxVotes)
        {
            string highestVotedPlayer = highestVotedPlayers[0];
            resultText.text = "En çok oy alan oyuncu: " + highestVotedPlayer;
            playerRoleText.text = "Rolü: " + GameData.AssignedCharacters[highestVotedPlayer];
            otherText.gameObject.SetActive(true);
            tieText.gameObject.SetActive(false);
            blankVoteText.gameObject.SetActive(false);

            // Elenen oyuncuyu ekle
            GameData.EliminatedPlayers.Add(highestVotedPlayer);
        }
        else if (blankVoteCount >= maxVotes || blankVoteCount == playerVotes.Count)
        {
            resultText.text = "En çok boþ oy kullanýldý veya boþ oy sayýsý oyuncu sayýsýna eþit. Kimse Elenmedi";
            blankVoteText.gameObject.SetActive(true);
            tieText.gameObject.SetActive(false);
            otherText.gameObject.SetActive(false);
            playerRoleText.gameObject.SetActive(false);
        }
        else if (highestVotedPlayers.Count > 1)
        {
            resultText.text = "Oylarda eþitlik var. Kimse Elenmedi.";
            tieText.gameObject.SetActive(true);
            blankVoteText.gameObject.SetActive(false);
            otherText.gameObject.SetActive(false);
            playerRoleText.gameObject.SetActive(false);
        }

        // Next Day scene geçiþi için butonun týklama olayýný ayarla
        nextDaySceneButton.onClick.AddListener(() =>
        {
            if (CheckGameOver())
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                GoToNextDayScene();
            }
        });
    }

    // Next Day scene geçiþi fonksiyonu
    void GoToNextDayScene()
    {
        // GameData'daki oyuncu oylarýný sýfýrlama
        GameData.PlayerVotes.Clear();
        GameData.BlankVoteCount = 0;

        // Next Day scene'e geçiþ yap
        SceneManager.LoadScene("NextDayScene");
    }

    bool CheckGameOver()
    {
        return AllGhostsEliminated() || GhostsOutnumberOthers();
    }

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
