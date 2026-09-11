using Unity.VisualScripting;
using UnityEngine;

public class ScriptPauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Start() 
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
    }  

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (ScriptGameManager.Instance == null) return;

        if (ScriptGameManager.Instance.State == ScriptGameManager.GameState.Playing)
        {
            Pause();
        }
        else if (ScriptGameManager.Instance.State == ScriptGameManager.GameState.Paused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        ScriptGameManager.Instance.SetPaused(true);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        ScriptGameManager.Instance.SetPaused(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
    }
}
