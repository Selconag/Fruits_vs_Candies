using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayingMenu : MonoBehaviour
{
    public bool menuIsActive;
    GameManager1 Manager;
    public GameObject MenuGameObject;
    public GameObject Panel;
    int c = 1;                                                                      //case kontroller

    private void Start()
    {
        Manager = GameObject.FindObjectOfType<GameManager1>();
        menuIsActive = false;
       
        
    }

    public void ShowMenu()
    {
        if (menuIsActive == false) { 
        menuIsActive = true;
             Manager.ClickIsAvailable = false;
             MenuGameObject.SetActive(true);
             Panel.SetActive(false);
             Time.timeScale = 0;
        

             c = 4;      
        }
        else
        {
            c = 1;
            CloseMenu();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel() {
        
        Application.LoadLevel(Application.loadedLevel);
    }

    public void CloseMenu()
    {
        menuIsActive = false;
        Manager.ClickIsAvailable = true;
        Time.timeScale = 1f;
        MenuGameObject.SetActive(false);
        Panel.SetActive(true);


    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    public void SpeedUp()
    {
        c = c + 1;
        switch (c)
        {
            
            case 1:
                Time.timeScale = 1f;
                break;
            case 2:
                Time.timeScale = 2.5f;
                break;
            case 3:
                Time.timeScale = 4f;
                break;
            case 4:
                Time.timeScale = 5.5f;
                c = 0;
                break;
            case 5:
                break;
        }
        
    }



}
