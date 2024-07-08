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

        // Oyunu baþlat
        SceneManager.LoadScene("PlayerName"); // Oyun sahnesine geçiþ yapýlabilir
    }
}
