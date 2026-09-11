using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    public const string MainMenuScene = "Mainmenu";
    public const string GameScene = "Game";

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Reset time scale to normal speed
        SceneManager.LoadScene(sceneName);
    }

    public void PlayerGame() => LoadScene(GameScene);

    public void GotToMainMenu() => LoadScene(MainMenuScene);

    public void RestartGame() 
    { 
        Time.timeScale = 1f; // Reset time scale to normal speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

}
