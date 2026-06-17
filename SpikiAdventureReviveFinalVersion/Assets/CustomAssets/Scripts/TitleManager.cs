using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("DialogueStage1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}