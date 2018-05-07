using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    //Parametros de la partida
    public int roundsToWin = 1;
    public float startDelay = 3f;
    public float endDelay = 3f;

    public Color[] playerColors;
    public Sprite[] textMarkers;

    //Elementos del nivel que requieren asignacion
    public CameraControl cameraControl;
    public GameObject canvas;
    public GameObject playerMarkerPrefab;
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    //Elemento proporcionados en la selección del personaje
    public GameObject[] playersAvatar;    
    public int[] playerAsignations;
    private int playerCounter = 2;

    //Elementos a instanciar en el nivel
    public PlayersManager[] players;
    private PlayerMarkerController[] markers;

    //Tiempos de espera al empezar y al acabar cada ronda
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;

	// Use this for initialization
	void Start () {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        players = new PlayersManager[playerCounter];
        markers = new PlayerMarkerController[playerCounter];

        SpawnCharacters();
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
                (GameObject) Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
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

        cameraControl.m_Targets = targets;
    }
}
