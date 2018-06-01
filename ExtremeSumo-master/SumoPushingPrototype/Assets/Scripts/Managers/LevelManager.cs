using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    //Parametros de la partida
    public int roundsToWin = 1;
    public float startDelay = 1f;
    public float endDelay = 3f;

    public Color[] playerColors;
    public Sprite[] textMarkers;

    //Elementos del nivel que requieren asignacion
    public CameraControl cameraControl;
    public GameObject canvas;
    public GameObject playerMarkerPrefab;
    public AIDirector aiDirector;
    public GameObject playerPrefab;
    public ParticleSystem[] particles;
    public GameObject AICharacterPrefab;
    public Transform[] spawnPoints;
    public SpawnPowerUp powerUpManager;
    public GameObject winnerText;
    private Text textoInicio;

    //Elemento proporcionados en la selección del personaje
    public GameObject[] playersAvatar;    
    public int[] playerAsignations;
    public int playerCounter = 2;
    public int cpuCounter = 0;

    //Elementos a instanciar en el nivel
    public PlayersManager[] players;
    public PlayersManager[] cpuCharacters;
    private PlayerMarkerController[] markers;

    //Tiempos de espera al empezar y al acabar cada ronda
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private float initialTime=5;

    //Players round winner and game winner
    private PlayersManager roundWinner;
    private PlayersManager gameWinner;

	// Use this for initialization
	void Start () {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        textoInicio = winnerText.GetComponent<Text>();

        //SetUp();
	}

    public void SetUp()
    {
        players = new PlayersManager[playerCounter];
        markers = new PlayerMarkerController[playerCounter + cpuCounter];
        cpuCharacters = new PlayersManager[cpuCounter];

        SpawnCharacters();
        SpawnCPUCharactes();
        SetCameraTragets();
        SetAITargets();

        StartCoroutine(GameLoop());
    }

    private void SpawnCharacters()
    {
        for(int i = 0; i < playerCounter; i++)
        {
            players[i] = new PlayersManager();
        }

        for(int i = 0; i < playerCounter; i++)
        {
            
            //Instanciar personaje del jugador
            players[i].playerInstance =
                (GameObject) Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.LookRotation(new Vector3(0f, spawnPoints[i].position.y, 0f) - spawnPoints[i].position));
            //Instanciar marker del jugador
            GameObject marker = Instantiate(playerMarkerPrefab, canvas.transform);
            markers[i] = marker.GetComponent<PlayerMarkerController>();           
            markers[i].target = players[i].playerInstance.transform;
            players[i].marker = markers[i];
            markers[i].playerNumber.sprite = textMarkers[i + 1];
            //Asignar spwnpoint
            players[i].spawnPoint = spawnPoints[i];
            //Asignar avatar al jugador
            players[i].playerAvatar =
                (GameObject) Instantiate(playersAvatar[i], players[i].playerInstance.transform);
            //Asignar Input
            players[i].playerInput = playerAsignations[i];
            //Asignar color
            players[i].playerColor = playerColors[i + 1];
            players[i].playerInstance.tag = "Player" + (i + 1);
            //Asignar las partículas pertinentes
            players[i].particles = particles[i];
            players[i].SetUp();
        }
    }

    private void SpawnCPUCharactes()
    {
        for (int i = 0; i < cpuCounter; i++)
        {
            cpuCharacters[i] = new PlayersManager();
        }

        for (int i = 0; i < cpuCounter; i++)
        {
            //Instanciar personaje del jugador
            cpuCharacters[i].playerInstance =
                (GameObject)Instantiate(AICharacterPrefab, spawnPoints[playerCounter + i].position, Quaternion.LookRotation(new Vector3(0f, spawnPoints[playerCounter + i].position.y, 0f) - spawnPoints[playerCounter + i].position));
            //Instanciar marker del jugador
            GameObject marker = Instantiate(playerMarkerPrefab, canvas.transform);
            markers[playerCounter + i] = marker.GetComponent<PlayerMarkerController>();
            markers[playerCounter + i].target = cpuCharacters[i].playerInstance.transform;
            cpuCharacters[i].marker = markers[playerCounter + i];
            markers[playerCounter + i].playerNumber.sprite = textMarkers[0];
            //Asignar spwnpoint
            cpuCharacters[i].spawnPoint = spawnPoints[playerCounter + i];
            //Asignar avatar al jugador
            cpuCharacters[i].playerAvatar =
                (GameObject)Instantiate(playersAvatar[playerCounter + i], cpuCharacters[i].playerInstance.transform);
            //Asignar Input
            cpuCharacters[i].playerInput = 0;
            //Asignar color
            cpuCharacters[i].playerColor = playerColors[0];
            cpuCharacters[i].playerInstance.tag = "Player" + (playerCounter + i + 1);
            cpuCharacters[i].SetUp();
        }
    }

    private void SetCameraTragets()
    {
        Transform[] targets = new Transform[playerCounter + cpuCounter];

        for (int i = 0; i < players.Length; i++)
        {
            targets[i] = players[i].playerInstance.transform;
        }
        for(int i = 0; i < cpuCounter; i++)
        {
            targets[playerCounter + i] = cpuCharacters[i].playerInstance.transform;
        }

        cameraControl.m_Targets = targets;
    }

    private void SetAITargets()
    {
        Transform[] targets = new Transform[playerCounter];

        for (int i = 0; i < players.Length; i++)
        {
            targets[i] = players[i].playerInstance.transform;
        }

        aiDirector.targets = targets;
    }

    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].playerInstance.activeSelf)
                numPlayersLeft++;
        }

        return numPlayersLeft <= 1; //Hará falta otra funcion si se quiere implementar el combate por equipos
    }

    private bool NoPlayerLeft()
    {
        int numPlayersLeft = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].playerInstance.activeSelf)
                numPlayersLeft++;
        }

        return numPlayersLeft <= 0; //Hará falta otra funcion si se quiere implementar el combate por equipos
    }

    private bool NoneCpuCharactersLeft()
    {
        int cpuCharactersLeft = 0;

        for(int i = 0; i < cpuCharacters.Length; i++)
        {
            if (cpuCharacters[i].playerInstance.activeSelf)
                cpuCharactersLeft++;
        }

        return cpuCharactersLeft <= 0;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if(gameWinner != null)
        {
            //SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        //ResetAllPlayers();
        DisablePlayerContol();
        powerUpManager.Setup();

        cameraControl.SetStartPositionAndSize();
        
        
        while (initialTime > 0) {
            if (initialTime == 4) {
                textoInicio.color = playerColors[4];
                textoInicio.text = "" + initialTime;
                winnerText.SetActive(true);
            } else if (initialTime==3) {
                textoInicio.color = playerColors[3];
                textoInicio.text = "" + initialTime;
            }
            else if (initialTime == 2)
            {
                textoInicio.color = playerColors[2];
                textoInicio.text = "" + initialTime;
            }
            else if (initialTime == 1)
            {
                textoInicio.color = playerColors[1];
                textoInicio.text = "" + initialTime;
            }


            yield return startWait;
            initialTime--;
        }

        textoInicio.color = playerColors[0];
        textoInicio.text = "GO!";
        yield return startWait;
        winnerText.SetActive(false);

        /*
         roundNumber++;
         messageText.text = "ROUND " + roundNumber;
         */

        // yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerContol();
        //messageText.text = "";
        
        while ((!NoneCpuCharactersLeft() && !NoPlayerLeft()) || !OnePlayerLeft())
        {
            powerUpManager.UpdateSpawn();
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        DisablePlayerContol();

        roundWinner = null;
        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.nWins++;

        gameWinner = GetGameWinner();
        Debug.Log("Yeh");
        //string message = EndMessage();
        //messageText.text = message;

        yield return endWait;

        PauseMenu.GameIsFinish = true;
    }

    private PlayersManager GetRoundWinner()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].playerInstance.activeSelf)
            {
                int player=i+1;
                textoInicio.text = "PLAYER " + player + " WINS!";
                textoInicio.color = players[i].playerColor;
                winnerText.SetActive(true);
                return players[i];
            }            
        }

        textoInicio.text = "THE CPU WINS!!";
        textoInicio.color = cpuCharacters[0].playerColor;
        winnerText.SetActive(true);

        return null;
    }

    private PlayersManager GetGameWinner()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].nWins == roundsToWin)
                return players[i];
        }

        return null;
    }

    private void ResetAllPlayers()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }

        for (int i = 0; i < cpuCounter; i++)
        {
            cpuCharacters[i].Reset();
        }
    }

    private void EnablePlayerContol()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }

        for(int i = 0; i < cpuCounter; i++)
        {
            cpuCharacters[i].EnableControl();
        }
    }

    private void DisablePlayerContol()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }

        for (int i = 0; i < cpuCounter; i++)
        {
            cpuCharacters[i].DisableControl();
        }
    }

    public void ResetGame()
    {
        initialTime= 5;
        ResetAllPlayers();
        powerUpManager.Setup();
        winnerText.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(GameLoop());
    }
}
