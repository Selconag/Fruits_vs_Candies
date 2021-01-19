using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public string sceneX="SettingsMenu";
    public GameObject PanelMain;
    public GameObject PanelSettings;
    public GameObject PanelLevelSelect;
    int levelIndex;

    public void LevelSelect_Button()
    {
        //SceneManager.LoadScene("GameScene");
        PanelMain.SetActive(false);
        PanelLevelSelect.SetActive(true);
    }
    public void Settings_Button()
    {
        //SceneManager.LoadScene("SettingsMenu");
        PanelMain.SetActive(false);
        PanelSettings.SetActive(true);
        //SceneManager.LoadScene(sceneX);
    }
    public void Back_to_Menu()
    {
        if (PanelSettings.activeSelf)
        {
            PanelSettings.SetActive(false);
            PanelMain.SetActive(true);
        }
        else
        {
            PanelLevelSelect.SetActive(false);
            PanelMain.SetActive(true);
        }
    }
    public void Exit_Button()
    {
        Application.Quit();
    }


    public void LevelLoader()
    {
        levelIndex = Int16.Parse(EventSystem.current.currentSelectedGameObject.name);
        Application.LoadLevel(levelIndex) ;
    }

    private void Start()
    {
        PanelMain.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OpenCoderPages()
    {
        Application.OpenURL("https://daltstudyo.itch.io/");
        
    }


    public void OpenDesignerPages()
    {
        Application.OpenURL("https://selocanus-hopus.itch.io/");
    }

    

}