using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class RoleAssignment : MonoBehaviour
{
    public Text playerCountText;
    public Text ghostCountText;
    public Text ghostHunterCountText;
    public Text protectorCountText;
    public Text detectorCountText;
    public Text messageText;

    public Button increaseGhostButton;
    public Button decreaseGhostButton;
    public Button increaseGhostHunterButton;
    public Button decreaseGhostHunterButton;
    public Button increaseProtectorButton;
    public Button decreaseProtectorButton;
    public Button increaseDetectorButton;
    public Button decreaseDetectorButton;
    public Button nextButton;
    public Button backButton;

    private int totalPlayers;
    private int ghosts = 1;
    private int ghostHunters = 1;
    private int protectors = 0;
    private int detectors = 0;

    void Start()
    {
        totalPlayers = GameData.PlayerNames.Count;
        Debug.Log("Player Names in RoleAssignment: " + string.Join(", ", GameData.PlayerNames));
        UpdatePlayerCountText();

        // Baþlangýçta karakter sayýlarýný güncelle
        UpdateRoleTexts();

        // Buton olaylarýný ayarla
        increaseGhostButton.onClick.AddListener(() => ChangeRoleCount(ref ghosts, 1));
        decreaseGhostButton.onClick.AddListener(() => ChangeRoleCount(ref ghosts, -1));
        increaseGhostHunterButton.onClick.AddListener(() => ChangeRoleCount(ref ghostHunters, 1));
        decreaseGhostHunterButton.onClick.AddListener(() => ChangeRoleCount(ref ghostHunters, -1));
        increaseProtectorButton.onClick.AddListener(() => ChangeRoleCount(ref protectors, 1));
        decreaseProtectorButton.onClick.AddListener(() => ChangeRoleCount(ref protectors, -1));
        increaseDetectorButton.onClick.AddListener(() => ChangeRoleCount(ref detectors, 1));
        decreaseDetectorButton.onClick.AddListener(() => ChangeRoleCount(ref detectors, -1));

        // Ýleri ve Geri butonlarýný ayarla
        nextButton.onClick.AddListener(NextButtonClicked);
        backButton.onClick.AddListener(BackButtonClicked);
        backButton.gameObject.SetActive(true); // Geri butonu baþlangýçta aktif olsun
    }

    void ChangeRoleCount(ref int roleCount, int change)
    {
        int newTotal = ghosts + ghostHunters + protectors + detectors + change;

        if (newTotal <= totalPlayers && roleCount + change >= (roleCount == ghosts || roleCount == ghostHunters ? 1 : 0))
        {
            roleCount += change;
            UpdateRoleTexts();
        }
    }

    void UpdatePlayerCountText()
    {
        playerCountText.text = "Oyuncu Sayýsý: " + totalPlayers.ToString();
    }

    void UpdateRoleTexts()
    {
        ghostCountText.text = ghosts.ToString();
        ghostHunterCountText.text = ghostHunters.ToString();
        protectorCountText.text = protectors.ToString();
        detectorCountText.text = detectors.ToString();

        // Toplam atanacak karakter sayýsýný kontrol et ve mesajý güncelle
        int totalAssigned = ghosts + ghostHunters + protectors + detectors;
        if (totalAssigned < totalPlayers)
        {
            messageText.gameObject.SetActive(true);
            nextButton.interactable = false;
        }
        else
        {
            messageText.gameObject.SetActive(false);
            nextButton.interactable = true;
        }
    }

    void NextButtonClicked()
    {
        if (nextButton.interactable)
        {
            // Karakter eþleþtirmelerini GameData'da sakla
            SaveAssignedCharacters();

            // Sonraki sahneye geçiþ yap
            SceneManager.LoadScene("StartGame"); // Sonraki sahnenin adýný buraya yazýn
        }
    }

    void BackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // Geri dönülecek sahnenin adýný buraya yazýn
    }

    // Karakter eþleþtirmelerini GameData'da saklayan metod
    void SaveAssignedCharacters()
    {
        Dictionary<string, string> assignedCharacters = new Dictionary<string, string>();

        List<string> playerNames = GameData.PlayerNames;
        List<string> characters = new List<string>();

        // Ghost Hunters ekle
        for (int i = 0; i < ghostHunters; i++)
        {
            characters.Add("Ghost Hunter");
        }

        // Ghosts ekle
        for (int i = 0; i < ghosts; i++)
        {
            characters.Add("Ghost");
        }

        // Protectors ekle
        for (int i = 0; i < protectors; i++)
        {
            characters.Add("Protector");
        }

        // Detectors ekle
        for (int i = 0; i < detectors; i++)
        {
            characters.Add("Detector");
        }

        // Karakterleri rastgele sýrala
        GameData.Shuffle(characters);

        // Oyuncu isimleriyle eþleþtirerek atama yap
        for (int i = 0; i < playerNames.Count; i++)
        {
            if (i < characters.Count)
            {
                assignedCharacters[playerNames[i]] = characters[i];
            }
            else
            {
                assignedCharacters[playerNames[i]] = "Default Character";
            }
        }

        Debug.Log("Assigned Characters: " + string.Join(", ", assignedCharacters.Select(kv => kv.Key + "-" + kv.Value)));
        // GameData'ya atanan karakterleri kaydet
        GameData.AssignedCharacters = assignedCharacters;
    }
}
