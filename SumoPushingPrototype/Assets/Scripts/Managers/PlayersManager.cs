using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayersManager {
    public Color playerColor;
    public Transform spawnPoint;
    public GameObject playerInstance;
    public GameObject playerAvatar;
    public PlayerMarkerController marker;
    public int playerNumber;
    public int playerInput;
    public int nWins;

    private PlayerController movement;

	public void SetUp()
    {
        movement = playerInstance.GetComponent<PlayerController>();

        movement.inputNumber = playerInput;     

        marker.spriteColor = playerColor;

        nWins = 0;
    }

    public void DisableControl()
    {
        movement.enabled = false;
       // marker.gameObject.SetActive(false);      
    }

    public void EnableControl()
    {
        movement.enabled = true;
        //marker.gameObject.SetActive(true);
    }

    public void Reset()
    {
        playerInstance.transform.position = spawnPoint.transform.position;

        playerInstance.SetActive(false);
        playerInstance.SetActive(true);
    }
}
