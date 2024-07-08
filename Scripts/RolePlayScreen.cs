using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class RolePlayScreen : MonoBehaviour
{
    public Text playerNameText;
    public Text playerRoleText;
    public Button roleButton;
    public Button nextPlayerButton;
    public Button nextSceneButton;
    public Button[] actionButtons;
    public Text[] actionButtonTexts;
    public Text instructionText;
    public Text actionResultText;

    private List<string> playerNames;
    private Dictionary<string, string> assignedCharacters;
    private List<string> eliminatedPlayers;
    private int currentPlayerIndex = 0;
    private List<string> activePlayers = new List<string>();

    void Start()
    {
        // GameData'dan oyuncu isimlerini ve rollerini �ek
        playerNames = GameData.PlayerNames;
        assignedCharacters = GameData.AssignedCharacters;
        eliminatedPlayers = GameData.EliminatedPlayers;

        // Bo� liste kontrol�
        if (playerNames == null || playerNames.Count == 0)
        {
            Debug.LogError("RolePlayScreen: Player names list is empty or not initialized.");
            return;
        }

        // Debug log ekleyin
        Debug.Log("RolePlayScreen: Player Names: " + string.Join(", ", playerNames));
        Debug.Log("RolePlayScreen: Assigned Characters: " + string.Join(", ", assignedCharacters.Select(kv => kv.Key + "-" + kv.Value)));
        Debug.Log("RolePlayScreen: Eliminated Players: " + string.Join(", ", eliminatedPlayers));

        // Elenen oyuncular� filtrele
        FilterEliminatedPlayers();

        // �lk oyuncunun bilgilerini g�ster
        ShowPlayerName();

        // Rol� g�ster butonuna t�klama olay�n� ekle
        roleButton.onClick.AddListener(() =>
        {
            // Rol� g�ster
            ShowPlayerRole(currentPlayerIndex);
            // �lk butonu kapat, di�er butonu a�
            roleButton.gameObject.SetActive(false);
            nextPlayerButton.gameObject.SetActive(true);

            // Eylem butonlar�n� g�ster
            string role = assignedCharacters[activePlayers[currentPlayerIndex]];
            if (role == "Ghost" || role == "Protector" || role == "Detector")
            {
                ShowActionButtons(role);
            }
        });

        // Di�er oyuncuya ge� butonuna t�klama olay�n� ekle
        nextPlayerButton.onClick.AddListener(() =>
        {
            // Bir sonraki oyuncuya ge�
            currentPlayerIndex++;
            if (currentPlayerIndex < activePlayers.Count)
            {
                // Bir sonraki oyuncunun bilgilerini g�ster
                ShowPlayerName();
                // �kinci butonu kapat, ilk butonu a�
                nextPlayerButton.gameObject.SetActive(false);
                roleButton.gameObject.SetActive(true);
                actionResultText.text = ""; // Eylem sonucunu temizle
                HideActionButtons();
            }
            else
            {
                // T�m oyuncular rollerini ��rendi�inde sonraki sahne butonunu a�
                nextPlayerButton.gameObject.SetActive(false);
                nextSceneButton.gameObject.SetActive(true);
            }
        });

        // Sonraki sahne butonuna t�klama olay�n� ekle
        nextSceneButton.onClick.AddListener(() =>
        {
            // Sonraki sahneye ge�
            SceneManager.LoadScene("RolePlayResults");
        });

        // Di�er butonlar ba�lang��ta kapal� olsun
        nextPlayerButton.gameObject.SetActive(false);
        nextSceneButton.gameObject.SetActive(false);
        HideActionButtons();
    }

    void ShowPlayerName()
    {
        if (currentPlayerIndex < 0 || currentPlayerIndex >= activePlayers.Count)
        {
            Debug.LogError("RolePlayScreen: Current player index is out of range.");
            return;
        }
        playerNameText.text = "Oyuncu: " + activePlayers[currentPlayerIndex];
        playerRoleText.text = ""; // Rol� temizle
    }

    void ShowPlayerRole(int index)
    {
        if (index < 0 || index >= activePlayers.Count)
        {
            Debug.LogError("RolePlayScreen: Player index is out of range.");
            return;
        }
        playerRoleText.text = "Rol: " + assignedCharacters[activePlayers[index]];
    }

    void FilterEliminatedPlayers()
    {
        activePlayers.Clear(); // Aktif oyuncular listesini temizle
        foreach (var playerName in playerNames)
        {
            if (!eliminatedPlayers.Contains(playerName))
            {
                activePlayers.Add(playerName);
            }
        }
        Debug.Log("RolePlayScreen: Active Players: " + string.Join(", ", activePlayers));
    }

    void ShowActionButtons(string role)
    {
        instructionText.gameObject.SetActive(true);
        instructionText.text = "Bir eylem ger�ekle�tirmek istemiyorsan di�er oyuncuya ge� butonuna t�kla.";

        for (int i = 0; i < actionButtons.Length; i++)
        {
            if (i < activePlayers.Count)
            {
                bool isSelf = activePlayers[i] == activePlayers[currentPlayerIndex];
                actionButtons[i].gameObject.SetActive(true);
                actionButtonTexts[i].text = activePlayers[i];
                int index = i; // Capture the current value of i
                actionButtons[i].onClick.RemoveAllListeners();
                actionButtons[i].onClick.AddListener(() => PerformAction(role, index));
                if (role == "Ghost" && assignedCharacters[activePlayers[i]] == "Ghost")
                {
                    actionButtons[i].interactable = false;
                }
                else if (role == "Protector" && isSelf && GameData.ProtectedSelfCount.ContainsKey(activePlayers[currentPlayerIndex]) && GameData.ProtectedSelfCount[activePlayers[currentPlayerIndex]] >= 1)
                {
                    actionButtons[i].interactable = false;
                }
                else
                {
                    actionButtons[i].interactable = true;
                }
            }
            else
            {
                actionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void HideActionButtons()
    {
        instructionText.gameObject.SetActive(false);
        for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].gameObject.SetActive(false);
        }
    }

    void PerformAction(string role, int index)
    {
        string targetPlayer = activePlayers[index];
        switch (role)
        {
            case "Ghost":
                GameData.EliminatedPlayer = targetPlayer;
                actionResultText.text = "Ghost " + targetPlayer + " oyuncusunu se�ti.";
                break;
            case "Protector":
                GameData.ProtectedPlayer = targetPlayer;
                actionResultText.text = "Protector " + targetPlayer + " oyuncusunu korudu.";
                if (targetPlayer == activePlayers[currentPlayerIndex])
                {
                    if (GameData.ProtectedSelfCount.ContainsKey(activePlayers[currentPlayerIndex]))
                    {
                        GameData.ProtectedSelfCount[activePlayers[currentPlayerIndex]]++;
                    }
                    else
                    {
                        GameData.ProtectedSelfCount[activePlayers[currentPlayerIndex]] = 1;
                    }
                }
                break;
            case "Detector":
                string targetRole = assignedCharacters[targetPlayer];
                actionResultText.text = targetPlayer + " oyuncusunun rol� " + targetRole;
                break;
        }
        nextPlayerButton.gameObject.SetActive(true);
        HideActionButtons();
    }
}
