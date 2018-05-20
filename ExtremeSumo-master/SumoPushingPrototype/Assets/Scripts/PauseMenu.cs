using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public static bool GameIsFinish = false;

    public GameObject pauseMenuUI;
    public GameObject finishMenuUI;
	
	// Update is called once per frame
	void Update () {
        if (GameIsFinish)
        {
            
            FinishGame();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }


	}
    public void FinishGame()
    {
        finishMenuUI.SetActive(true);
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused=false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        finishMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameIsFinish = false;
        SceneManager.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void retry()
    {
        finishMenuUI.SetActive(false);
        GameIsFinish = false;
        FindObjectOfType<LevelManager>().ResetGame();


    }

}
