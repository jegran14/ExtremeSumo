using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour {

    public int[] playerList;
    public EleccionPersonaje [] eleccionPersonaje;
    private int contador;

    public GameObject nextMenu;
    public GameObject[] charactersPrefabs;
	// Use this for initialization
	void Start () {
        playerList = new int[4];
        contador = 0;
	}
	// Update is called once per frame
	void Update () {
        if (AllPlayersReady())
        {
            if (Input.GetButtonDown("Jump" + 1) || Input.GetButtonDown("Jump" + 2) || Input.GetButtonDown("Jump" + 3) || Input.GetButtonDown("Jump" + 4))
            {
                GameObject[] avatar = new GameObject[contador];
                for (int i = 0; i < contador; i++)
                {
                    avatar[i] = charactersPrefabs[eleccionPersonaje[i].GetCharacter()];
                }

                FindObjectOfType<GameManager>().FromCharToLevels(contador, playerList, avatar, nextMenu);
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetButtonDown("Jump" + 1))
        {
            PlayerAsign(1);
        }
        else if (Input.GetButtonDown("Jump" + 2))
        {
            PlayerAsign(2);
        }
        else if (Input.GetButtonDown("Jump" + 3))
        {
            PlayerAsign(3);
        }
        else if (Input.GetButtonDown("Jump" + 4))
        {
            PlayerAsign(4);
        }
    }
    private void PlayerAsign(int currentPlayer)
    {
        int i;
        for (i = 0; i < playerList.Length; i++)
        {
            if (playerList[i] == currentPlayer)
            {
                return;
            }
        }
        if (contador < 3)
        {
            playerList[contador] = currentPlayer;
            eleccionPersonaje[contador].player = currentPlayer;
            contador++;
        }

        
    }

    public bool AllPlayersReady()
    {
        int readyCounter = 0;

        for(int i = 0; i < contador; i++)
        {
            if(eleccionPersonaje[i].isReady)
                readyCounter++;
        }

        return readyCounter == contador && contador > 0;
    }
}
