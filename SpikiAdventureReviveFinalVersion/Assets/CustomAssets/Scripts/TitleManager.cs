using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1Dialogue");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}