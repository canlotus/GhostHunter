using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class VotingScreen : MonoBehaviour
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

    void Start()
    {
        // GameData'dan oyuncu isimlerini �ek
        playerNames = GameData.PlayerNames;

        // Oy verilen oyuncular�n oy say�s�n� tutacak s�zl�k
        playerVoteCounts = new Dictionary<string, int>();
        foreach (var playerName in playerNames)
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
        currentPlayerText.text = "Oyuncu: " + playerNames[currentPlayerIndex];
    }

    void SetupPlayerButtons()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            // E�er buton GameData'da tan�ml� bir oyuncuya aitse aktif yap
            if (i < playerNames.Count)
            {
                playerButtons[i].gameObject.SetActive(true);
                playerNamesText[i].text = playerNames[i];
                playerVoteCountTexts[i].text = playerVoteCounts[playerNames[i]].ToString();

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
        string votedPlayerName = playerNames[index];
        playerVoteCounts[votedPlayerName]++;

        // Bir sonraki oyuncuya ge�
        currentPlayerIndex++;

        // T�m oyuncu butonlar�n� tekrar aktif hale getir
        if (currentPlayerIndex < playerNames.Count)
        {
            SetupPlayerButtons();
            ShowCurrentPlayer();
        }
        else
        {
            // T�m oyuncular oy verdiyse butonlar� deaktif et ve nextSceneButton'u aktif hale getir
            foreach (var button in playerButtons)
            {
                button.gameObject.SetActive(false);
            }
            blankVoteButton.gameObject.SetActive(false); // Bo� oy butonunu kapat
            nextSceneButton.gameObject.SetActive(true);
        }
    }

    void OnBlankVoteButtonClicked()
    {
        blankVoteCount++;
        currentPlayerIndex++;
        if (currentPlayerIndex < playerNames.Count)
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

    public void GoToNextScene()
    {
        foreach (var playerName in playerNames)
        {
            GameData.SetPlayerVoteCount(playerName, playerVoteCounts[playerName]);
        }
        GameData.BlankVoteCount = blankVoteCount;

        SceneManager.LoadScene("ResultScene");
    }
}
