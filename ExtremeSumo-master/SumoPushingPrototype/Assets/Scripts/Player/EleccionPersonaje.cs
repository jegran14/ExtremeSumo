using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EleccionPersonaje : MonoBehaviour
{
    private Animator anim;
    private Animator playerAnim;
    private int index;
    private bool isActive;

    public GameObject canvas;
    public bool isReady;
    public int player;
    public Text text;
    public Button button;
    public GameObject[] leftButton;
    public GameObject[] rightButton;
    
    [HideInInspector]
    public GameObject[] characterList;

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

    public void ActivarBotones(int left, int right)
    {

        leftButton[left].SetActive(true);
      
        rightButton[right].SetActive(true);

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

    public int GetCharacter()
    {
        return index;
    }

    // Update is called once per frame
    void Update()
    {
        if (player > 0 && !isReady)
        {
            if (isActive && (Input.GetButtonDown("Jump" + player)))
            {
                isReady = true;
                Ready();
                
            }
            else
            {
                isActive = true;
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
    public void Ready()
    {
        anim = characterList[index].GetComponent<Animator>();
        anim.SetTrigger("Falling");
        anim = button.animator;
        anim.SetBool("isReady", true);
        text.fontSize = 41;
        text.text = "Ready!!";
        text.color = Color.red;
    }


}