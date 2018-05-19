using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public LevelInformation lvlInfo;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        lvlInfo = new LevelInformation();
    }

    private void InitLevel()
    {

    }

    public void FromCharToLevels(int pc, int[] pList, GameObject[] pPrefabs, GameObject menu)
    {
        lvlInfo.playerCounter = pc;
        lvlInfo.playerList = pList;
        lvlInfo.playerPrefab = pPrefabs;

        menu.SetActive(true);
    }

    public void LoadGameLevel(int i)
    {
        SceneManager.LoadScene(i);
    }


    // Update is called once per frame
    void Update () {
	    
	}

    public void MainToChar()
    {
        SceneManager.sceneLoaded += GameLevelLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void GameLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex > 1)
        {
            LevelManager lvlManager = FindObjectOfType<LevelManager>();
            if (lvlManager)
            {
                lvlManager.playerAsignations = lvlInfo.playerList;
                lvlManager.playersAvatar = lvlInfo.playerPrefab;
                lvlManager.playerCounter = lvlInfo.playerCounter;

                lvlManager.SetUp();
            }
        }
    }
    [System.Serializable]
    public class LevelInformation
    {
        public int playerCounter;
        public int[] playerList;
        public GameObject[] playerPrefab;
    }
}
