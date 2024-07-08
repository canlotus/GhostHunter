using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NewVoteScene : MonoBehaviour
{
    public Text currentPlayerText;
    public Button[] playerButtons;
    public Text[] playerNamesText;
    public Text[] playerVoteCountTexts;
    public Button nextSceneButton;
    public Button blankVoteButton; // Boþ oy butonu

    private List<string> playerNames;
    private Dictionary<string, int> playerVoteCounts;
    private int blankVoteCount = 0;
    private int currentPlayerIndex = 0;
    private List<string> activePlayers = new List<string>();

    void Start()
    {
        // GameData'dan oyuncu isimlerini ve elenen oyuncularý çek
        playerNames = GameData.PlayerNames;
        List<string> eliminatedPlayers = GameData.EliminatedPlayers;

        // Elenen oyuncularý filtrele
        foreach (var playerName in playerNames)
        {
            if (!eliminatedPlayers.Contains(playerName))
            {
                activePlayers.Add(playerName);
            }
        }

        // Oy verilen oyuncularýn oy sayýsýný tutacak sözlük
        playerVoteCounts = new Dictionary<string, int>();
        foreach (var playerName in activePlayers)
        {
            playerVoteCounts.Add(playerName, 0);
        }

        // Baþlangýçta sadece ilk oyuncunun adýný göster
        ShowCurrentPlayer();

        // Oyuncu butonlarýný ve isimlerini aktif hale getir
        SetupPlayerButtons();

        // Boþ oy butonunu ayarla
        blankVoteButton.onClick.AddListener(OnBlankVoteButtonClicked);
    }

    void ShowCurrentPlayer()
    {
        currentPlayerText.text = "Oyuncu: " + activePlayers[currentPlayerIndex];
    }

    void SetupPlayerButtons()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            // Eðer buton aktif bir oyuncuya aitse aktif yap
            if (i < activePlayers.Count)
            {
                playerButtons[i].gameObject.SetActive(true);
                playerNamesText[i].text = activePlayers[i];
                playerVoteCountTexts[i].text = playerVoteCounts[activePlayers[i]].ToString();

                // Oy veren oyuncunun kendi butonunu devre dýþý býrak
                playerButtons[i].interactable = i != currentPlayerIndex;

                // Butonun týklama olayýný dinleyelim
                int index = i;
                playerButtons[i].onClick.RemoveAllListeners();
                playerButtons[i].onClick.AddListener(() => OnPlayerButtonClicked(index));
            }
            else
            {
                playerButtons[i].gameObject.SetActive(false);
                playerNamesText[i].text = "";
                playerVoteCountTexts[i].text = "";
            }
        }
    }

    void OnPlayerButtonClicked(int index)
    {
        // Oy verilen oyuncunun sayacýný artýr
        string votedPlayerName = activePlayers[index];
        playerVoteCounts[votedPlayerName]++;

        // Bir sonraki oyuncuya geç
        currentPlayerIndex++;
        GoToNextPlayer();
    }

    void OnBlankVoteButtonClicked()
    {
        blankVoteCount++;
        currentPlayerIndex++;
        GoToNextPlayer();
    }

    void GoToNextPlayer()
    {
        // Sýradaki aktif oyuncuyu bulana kadar devam et
        while (currentPlayerIndex < activePlayers.Count && !IsPlayerActive(currentPlayerIndex))
        {
            currentPlayerIndex++;
        }

        if (currentPlayerIndex < activePlayers.Count)
        {
            SetupPlayerButtons();
            ShowCurrentPlayer();
        }
        else
        {
            foreach (var button in playerButtons)
            {
                button.gameObject.SetActive(false);
            }
            blankVoteButton.gameObject.SetActive(false); // Boþ oy butonunu kapat
            nextSceneButton.gameObject.SetActive(true);
        }
    }

    bool IsPlayerActive(int index)
    {
        return index < activePlayers.Count && !GameData.EliminatedPlayers.Contains(activePlayers[index]);
    }

    public void GoToNextScene()
    {
        foreach (var playerName in activePlayers)
        {
            GameData.SetPlayerVoteCount(playerName, playerVoteCounts[playerName]);
        }
        GameData.BlankVoteCount = blankVoteCount;

        SceneManager.LoadScene("NewResultScene");
    }
}
