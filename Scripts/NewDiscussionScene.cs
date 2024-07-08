using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewDiscussionScene : MonoBehaviour
{
    public Button startDiscussionButton;
    public Text timerText;
    public Button proceedToVoteButton;
    public Text discussionEndText;
    public float discussionTime = 180f; // 3 dakika

    private float remainingTime;
    private bool isDiscussionStarted = false;

    void Start()
    {
        // Baþlangýçta buton ve sayaç ayarlarý
        startDiscussionButton.onClick.AddListener(StartDiscussion);
        proceedToVoteButton.onClick.AddListener(ProceedToVote);

        timerText.gameObject.SetActive(false);
        discussionEndText.gameObject.SetActive(false);
        proceedToVoteButton.gameObject.SetActive(false); // "Oylamaya Geç" butonu baþlangýçta kapalý
        remainingTime = discussionTime;
    }

    void Update()
    {
        if (isDiscussionStarted)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimerText(remainingTime);
            }
            else
            {
                EndDiscussion();
            }
        }
    }

    void StartDiscussion()
    {
        // Tartýþmayý baþlat
        isDiscussionStarted = true;
        startDiscussionButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        proceedToVoteButton.gameObject.SetActive(true); // "Oylamaya Geç" butonunu aktif et
    }

    void EndDiscussion()
    {
        // Tartýþmayý bitir
        isDiscussionStarted = false;
        timerText.gameObject.SetActive(false);
        discussionEndText.gameObject.SetActive(true);
        discussionEndText.text = "Tartýþma bitti. Þimdi oylama zamaný!";
    }

    void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ProceedToVote()
    {
        SceneManager.LoadScene("NewVoteScene");
    }
}