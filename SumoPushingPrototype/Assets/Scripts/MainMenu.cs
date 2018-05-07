using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
<<<<<<< HEAD
		
=======
        gameManager = FindObjectOfType<GameManager>();
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
	}

	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelPradera()
    {
        gameManager.LoadGameLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelIceberg()
    {
        gameManager.LoadGameLevel(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void LevelVolcan()
    {
        gameManager.LoadGameLevel(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
