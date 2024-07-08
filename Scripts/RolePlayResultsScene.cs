using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RolePlayResultsScene : MonoBehaviour
{
    public Button nextButton;
    public Text resultText;

    void Start()
    {
        // Ghost ve Protector eylemlerini de�erlendir
        EvaluateActions();

        // Butonun t�klama olay�n� ayarla
        nextButton.onClick.AddListener(() =>
        {
            if (CheckGameOver())
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                GoToNextScene();
            }
        });
    }

    void EvaluateActions()
    {
        string eliminatedPlayer = GameData.EliminatedPlayer;
        string protectedPlayer = GameData.ProtectedPlayer;

        if (eliminatedPlayer != null)
        {
            if (eliminatedPlayer == protectedPlayer)
            {
                resultText.text = "Ghost oyunculardan birini se�ti ama Protector onu korudu.";
            }
            else
            {
                resultText.text = "Ghost " + eliminatedPlayer + " oyuncusunu se�ti ve o elendi.";
                GameData.EliminatedPlayers.Add(eliminatedPlayer);
            }
        }
        else
        {
            resultText.text = "Bu turda kimse elenmedi.";
        }

        // Hamlelerin sonu�lar�n� temizle
        GameData.EliminatedPlayer = null;
        GameData.ProtectedPlayer = null;
    }

    void GoToNextScene()
    {
        // Sonraki sahneye ge�
        SceneManager.LoadScene("NewDiscussionScene");
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
