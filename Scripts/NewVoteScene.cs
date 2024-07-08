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
    public Button blankVoteButton; // Bo� oy butonu

    private List<string> playerNames;
    private Dictionary<string, int> playerVoteCounts;
    private int blankVoteCount = 0;
    private int currentPlayerIndex = 0;
    private List<string> activePlayers = new List<string>();

    void Start()
    {
        // GameData'dan oyuncu isimlerini ve elenen oyuncular� �ek
        playerNames = GameData.PlayerNames;
        List<string> eliminatedPlayers = GameData.EliminatedPlayers;

        // Elenen oyuncular� filtrele
        foreach (var playerName in playerNames)
        {
            if (!eliminatedPlayers.Contains(playerName))
            {
                activePlayers.Add(playerName);
            }
        }

        // Oy verilen oyuncular�n oy say�s�n� tutacak s�zl�k
        playerVoteCounts = new Dictionary<string, int>();
        foreach (var playerName in activePlayers)
        {
            playerVoteCounts.Add(playerName, 0);
        }

        // Ba�lang��ta sadece ilk oyuncunun ad�n� g�ster
        ShowCurrentPlayer();

        // Oyuncu butonlar�n� ve isimlerini aktif hale getir
        SetupPlayerButtons();

        // Bo� oy butonunu ayarla
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
            // E�er buton aktif bir oyuncuya aitse aktif yap
            if (i < activePlayers.Count)
            {
                playerButtons[i].gameObject.SetActive(true);
                playerNamesText[i].text = activePlayers[i];
                playerVoteCountTexts[i].text = playerVoteCounts[activePlayers[i]].ToString();

                // Oy veren oyuncunun kendi butonunu devre d��� b�rak
                playerButtons[i].interactable = i != currentPlayerIndex;

                // Butonun t�klama olay�n� dinleyelim
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
        // Oy verilen oyuncunun sayac�n� art�r
        string votedPlayerName = activePlayers[index];
        playerVoteCounts[votedPlayerName]++;

        // Bir sonraki oyuncuya ge�
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
        // S�radaki aktif oyuncuyu bulana kadar devam et
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
            blankVoteButton.gameObject.SetActive(false); // Bo� oy butonunu kapat
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
