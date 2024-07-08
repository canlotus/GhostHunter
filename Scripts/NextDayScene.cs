using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq; // Select kullanýmý için gerekli

public class NextDayScene : MonoBehaviour
{
    public void GoToRolePlayScene()
    {

        SceneManager.LoadScene("RolePlayScene");
    }
}
