using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManagement : MonoBehaviour
{
    [Header("Scenes")]
    public string sceneJouer;
    public string sceneCredits;

    public void Jouer()
    {
        SceneManager.LoadScene(sceneJouer);
    }

    public void Credits()
    {
        SceneManager.LoadScene(sceneCredits);
    }

    public void RetourMenu()
    {
        SceneManager.LoadScene("0_Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}