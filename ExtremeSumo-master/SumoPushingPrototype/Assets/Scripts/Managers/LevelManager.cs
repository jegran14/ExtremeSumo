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
    public GameObject AICharacterPrefab;
    public Transform[] spawnPoints;
    public GameObject winnerText;
    private Text textoInicio;

    //Elemento proporcionados en la selección del personaje
    public GameObject[] playersAvatar;    
    public int[] playerAsignations;
    public int playerCounter = 2;

    //Elementos a instanciar en el nivel
    public PlayersManager[] players;
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
        markers = new PlayerMarkerController[playerCounter];

        SpawnCharacters();
        //SpawnCPUCharactes();
        SetCameraTragets();

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
            players[i].SetUp();
        }
    }

    private void SpawnCPUCharactes()
    {
        for(int i = playerCounter; i < players.Length; i++)
        {
            //Instanciar personaje del jugador
            players[i].playerInstance =
                (GameObject)Instantiate(AICharacterPrefab, spawnPoints[i].position, Quaternion.LookRotation(new Vector3(0f, spawnPoints[i].position.y, 0f) - spawnPoints[i].position));
            //Instanciar marker del jugador
            GameObject marker = Instantiate(playerMarkerPrefab, canvas.transform);
            markers[i] = marker.GetComponent<PlayerMarkerController>();
            markers[i].target = players[i].playerInstance.transform;
            players[i].marker = markers[i];
            markers[i].playerNumber.sprite = textMarkers[0];
            //Asignar spwnpoint
            players[i].spawnPoint = spawnPoints[i];
            //Asignar avatar al jugador
            players[i].playerAvatar =
                (GameObject)Instantiate(playersAvatar[i], players[i].playerInstance.transform);
            //Asignar Input
            players[i].playerInput = 0;
            //Asignar color
            players[i].playerColor = playerColors[0];
            players[i].playerInstance.tag = "Player" + (i + 1);
            players[i].SetUp();
        }
    }

    private void SetCameraTragets()
    {
        Transform[] targets = new Transform[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            targets[i] = players[i].playerInstance.transform;
        }

        aiDirector.targets = targets;
        cameraControl.m_Targets = targets;
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

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if(gameWinner != null)
        {
            Debug.Log("Hey we finished");

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

        while (!OnePlayerLeft())
        {
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

        PauseMenu.GameIsFinish = true;

        //string message = EndMessage();
        //messageText.text = message;

        yield return endWait;
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
    }

    private void EnablePlayerContol()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }

    private void DisablePlayerContol()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }

    public void ResetGame()
    {
        initialTime= 5;
        ResetAllPlayers();
        winnerText.SetActive(false);
        StartCoroutine(GameLoop());
    }
}
