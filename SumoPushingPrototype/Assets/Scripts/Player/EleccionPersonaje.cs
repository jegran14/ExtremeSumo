using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EleccionPersonaje : MonoBehaviour
{

    private GameObject[] characterList;
    public GameObject canvas;
    private bool isActive;
    private bool isReady;
    private int index;
    public int player;
    public Text text;
    public Button button;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        isActive = false;
        isReady = false;
        characterList = new GameObject[transform.childCount];

        //Rellenamos el array con los modelos.
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        //Desactivamos el renderizado de los modelos.
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        canvas.SetActive(false);

     /*   //Activamos el primer personaje
        if (characterList[0])
            characterList[0].SetActive(true);*/
    }

    public void ToggleLeft()
    {

        //Desactivar actual
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        //Activar siguiente
        characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        //Desactivar caracter actual
        characterList[index].SetActive(false);

        index++;
        if (index > characterList.Length - 1)
        {
            index = 0;
        }
        //Activar personaje siguiente
        characterList[index].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (player > 0 && !isReady)
        {
            if (isActive && (Input.GetButtonDown("Jump" + player)))
            {
                isReady = true;
                ready();
            }
            else
            {
                if (Input.GetButtonDown("Jump" + player))
                {
                    isActive = true;
                    canvas.SetActive(true);
                    if (characterList[0])
                        characterList[0].SetActive(true);
                }
                if (Input.GetButtonDown("Left" + player))
                {
                    ToggleLeft();
                }
                else if (Input.GetButtonDown("Right" + player))
                {
                    ToggleRight();
                }
            }
        }

    }
    public void ready()
    {
        anim = button.animator;
        anim.SetBool("isReady", true);
        text.fontSize = 28;
        text.text = "Ready!!";
        text.color = Color.red;
    }


}