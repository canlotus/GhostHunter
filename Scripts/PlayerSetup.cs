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
    public Text warningText; // Yeni uyarý metni

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

        warningText.gameObject.SetActive(false); // Baþlangýçta uyarý metnini gizle

        // Her input field'a deðiþiklik olduðunda UpdatePlayerNames kontrolünü ekleyin
        foreach (GameObject inputFieldObject in inputFields)
        {
            InputField inputField = inputFieldObject.GetComponent<InputField>();
            if (inputField != null)
            {
                inputField.onValueChanged.AddListener(delegate { UpdatePlayerNames(); });
            }
        }

        UpdatePlayerNames(); // Ýlk kontrol
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

        // Ayný ada sahip oyuncu kontrolü
        if (playerNamesList.Count != playerNamesList.Distinct().Count())
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Ayný ada sahip oyuncular var! Farklý isimler girin.";
            return;
        }

        warningText.gameObject.SetActive(false); // Uyarý metnini gizle

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

        // Ayný ada sahip oyuncu kontrolü
        if (playerNamesList.Count != playerNamesList.Distinct().Count())
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Ayný ada sahip oyuncular var! Farklý isimler girin.";
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