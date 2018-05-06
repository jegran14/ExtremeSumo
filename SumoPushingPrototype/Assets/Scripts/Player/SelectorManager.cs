using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour {

    private int[] playerList;
    public EleccionPersonaje [] eleccionPersonaje;
    private int contador;

	// Use this for initialization
	void Start () {
        playerList = new int[4];
		
	}
	// Update is called once per frame
	void Update () {
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
        if (contador < 4)
        {
            playerList[contador] = currentPlayer;
            eleccionPersonaje[contador].player = currentPlayer;
            contador++;
        }

        
    }
}
