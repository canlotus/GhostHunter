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
    public Button blankVoteButton; // Boþ oy butonu

    private List<string> playerNames;
    private Dictionary<string, int> playerVoteCounts;
    private int blankVoteCount = 0;
    private int currentPlayerIndex = 0;

    void Start()
    {
        // GameData'dan oyuncu isimlerini çek
        playerNames = GameData.PlayerNames;

        // Oy verilen oyuncularýn oy sayýsýný tutacak sözlük
        playerVoteCounts = new Dictionary<string, int>();
        foreach (var playerName in playerNames)
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
        currentPlayerText.text = "Oyuncu: " + playerNames[currentPlayerIndex];
    }

    void SetupPlayerButtons()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            // Eðer buton GameData'da tanýmlý bir oyuncuya aitse aktif yap
            if (i < playerNames.Count)
            {
                playerButtons[i].gameObject.SetActive(true);
                playerNamesText[i].text = playerNames[i];
                playerVoteCountTexts[i].text = playerVoteCounts[playerNames[i]].ToString();

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
        string votedPlayerName = playerNames[index];
        playerVoteCounts[votedPlayerName]++;

        // Bir sonraki oyuncuya geç
        currentPlayerIndex++;

        // Tüm oyuncu butonlarýný tekrar aktif hale getir
        if (currentPlayerIndex < playerNames.Count)
        {
            SetupPlayerButtons();
            ShowCurrentPlayer();
        }
        else
        {
            // Tüm oyuncular oy verdiyse butonlarý deaktif et ve nextSceneButton'u aktif hale getir
            foreach (var button in playerButtons)
            {
                button.gameObject.SetActive(false);
            }
            blankVoteButton.gameObject.SetActive(false); // Boþ oy butonunu kapat
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
            blankVoteButton.gameObject.SetActive(false); // Boþ oy butonunu kapat
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
