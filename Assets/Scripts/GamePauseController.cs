using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] Button buttonMenu;
    public static bool gameIsPaused=false;

    private void Awake()
    {
        buttonMenu.onClick.AddListener(LobbyScene);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    private void LobbyScene()
    {
        SceneManager.LoadScene("lobby");
    }
}
