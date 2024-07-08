using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGameClicked);
    }

    void StartGameClicked()
    {

        // Oyunu ba�lat
        SceneManager.LoadScene("PlayerName"); // Oyun sahnesine ge�i� yap�labilir
    }
}
