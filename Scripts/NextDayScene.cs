using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq; // Select kullan�m� i�in gerekli

public class NextDayScene : MonoBehaviour
{
    public void GoToRolePlayScene()
    {

        SceneManager.LoadScene("RolePlayScene");
    }
}
