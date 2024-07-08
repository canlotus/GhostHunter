using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PlayerSetup : MonoBehaviour
{
    public List<GameObject> inputFields;
    public Button addPlayerButton;
    public Button removePlayerButton;
    public Button startGameButton;
    public Button backButton;
    public Text warningText; // Yeni uyar� metni

    private int maxPlayers = 12;
    private int activePlayers = 4;

    void Start()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            inputFields[i].SetActive(true);
        }

        addPlayerButton.onClick.AddListener(AddPlayer);
        removePlayerButton.onClick.AddListener(RemovePlayer);
        startGameButton.onClick.AddListener(StartGame);
        backButton.onClick.AddListener(BackToMainMenu);

        warningText.gameObject.SetActive(false); // Ba�lang��ta uyar� metnini gizle

        // Her input field'a de�i�iklik oldu�unda UpdatePlayerNames kontrol�n� ekleyin
        foreach (GameObject inputFieldObject in inputFields)
        {
            InputField inputField = inputFieldObject.GetComponent<InputField>();
            if (inputField != null)
            {
                inputField.onValueChanged.AddListener(delegate { UpdatePlayerNames(); });
            }
        }

        UpdatePlayerNames(); // �lk kontrol
    }

    void AddPlayer()
    {
        if (activePlayers < maxPlayers)
        {
            inputFields[activePlayers].SetActive(true);
            activePlayers++;
        }
    }

    void RemovePlayer()
    {
        if (activePlayers > 4)
        {
            activePlayers--;
            inputFields[activePlayers].SetActive(false);
        }
    }

    void StartGame()
    {
        List<string> playerNamesList = new List<string>();
        foreach (GameObject inputFieldObject in inputFields)
        {
            InputField inputField = inputFieldObject.GetComponent<InputField>();
            if (inputField != null && !string.IsNullOrEmpty(inputField.text))
            {
                playerNamesList.Add(inputField.text);
            }
        }

        // Ayn� ada sahip oyuncu kontrol�
        if (playerNamesList.Count != playerNamesList.Distinct().Count())
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Ayn� ada sahip oyuncular var! Farkl� isimler girin.";
            return;
        }

        warningText.gameObject.SetActive(false); // Uyar� metnini gizle

        Debug.Log("Player Names in PlayerSetup: " + string.Join(", ", playerNamesList));
        GameData.PlayerNames = playerNamesList;
        Debug.Log("GameData Player Names: " + string.Join(", ", GameData.PlayerNames));

        SceneManager.LoadScene("RoleAssignment");
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void UpdatePlayerNames()
    {
        List<string> playerNamesList = new List<string>();
        bool allFieldsFilled = true;

        foreach (GameObject inputFieldObject in inputFields)
        {
            InputField inputField = inputFieldObject.GetComponent<InputField>();
            if (inputField != null)
            {
                if (!string.IsNullOrEmpty(inputField.text))
                {
                    playerNamesList.Add(inputField.text);
                }
                else if (inputFieldObject.activeSelf)
                {
                    allFieldsFilled = false;
                }
            }
        }

        // Ayn� ada sahip oyuncu kontrol�
        if (playerNamesList.Count != playerNamesList.Distinct().Count())
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Ayn� ada sahip oyuncular var! Farkl� isimler girin.";
            startGameButton.interactable = false;
        }
        else if (!allFieldsFilled)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Oyuncu ismi giriniz.";
            startGameButton.interactable = false;
        }
        else
        {
            warningText.gameObject.SetActive(false);
            startGameButton.interactable = true;
        }
    }
}