using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour {

    public int[] playerList;
    public EleccionPersonaje [] eleccionPersonaje;
    private int contador;

    public GameObject canvasStart;
    public GameObject nextMenu;
    public GameObject[] charactersPrefabs;
    private Animator anim;
    public Button startButton;

	// Use this for initialization
	void Start () {
        playerList = new int[4];
        contador = 0;
        canvasStart.SetActive(true);
        anim = startButton.GetComponent<Animator>();

    }
	// Update is called once per frame
	void Update () {
        if (AllPlayersReady())
        {
            if (Input.GetButtonDown("Jump" + 1) || Input.GetButtonDown("Jump" + 2) || Input.GetButtonDown("Jump" + 3) || Input.GetButtonDown("Jump" + 4))
            {
                GameObject[] avatar = new GameObject[4];
                Debug.Log(contador);
                for (int i = 0; i < contador; i++)
                {
                    avatar[i] = charactersPrefabs[eleccionPersonaje[i].GetCharacter()];
                }
                /*for(int i = contador; i < playerList.Length; i++)
                {
                    avatar[i] = charactersPrefabs[Random.Range(0, 20)];
                }*/

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

    public void DesactivarCanvas()
    {
        canvasStart.SetActive(false);
    }

    private void PlayerAsign(int currentPlayer)
    {
        if (canvasStart.activeInHierarchy)
        {
            anim.SetTrigger("Normal");
        }
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
            eleccionPersonaje[contador].canvas.SetActive(true);
            if (eleccionPersonaje[contador].characterList[0])
                eleccionPersonaje[contador].characterList[0].SetActive(true);
            eleccionPersonaje[contador].player = currentPlayer;
            if (currentPlayer == 3 || currentPlayer == 1)
                eleccionPersonaje[contador].ActivarBotones(0, 0);
            else if (currentPlayer == 2)
                eleccionPersonaje[contador].ActivarBotones(1, 1);
            else if (currentPlayer == 4)
                eleccionPersonaje[contador].ActivarBotones(2, 2);
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
