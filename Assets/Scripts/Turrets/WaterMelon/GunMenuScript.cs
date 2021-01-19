using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GunMenuScript : MonoBehaviour
{
    GameObject menu;
    private string level,range,damage,cost;
    public TextMeshProUGUI gunInfo;
    public Gun melon;
    private GameManager1 gameManager;
    public FpsController fps;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager1>();
        menu = GameObject.Find("GunMenu");
        melon = melon.GetComponent<Gun>();
        fps = fps.GetComponent<FpsController>();
        this.menu.SetActive(false);
        gameManager.ClickIsAvailable = false;
    }
    private void FixedUpdate()
    {
        level = "Level = "+(melon.Level+1);
        range = "Range = " + melon.Range[melon.Level];
        damage ="Damage = "+ melon.Damage[melon.Level];
        cost = "Level Up Cost = 50" ;
        gunInfo.text = level +Environment.NewLine
             + range + Environment.NewLine
             + damage + Environment.NewLine
             + cost;
    }

    private void Update()
    {
        
    }

    public void ClosePage()
    {
        Debug.Log("You Clicked ClosePage");
        this.menu.SetActive(false);
        melon.SideMenuIsOpen = false;
        gameManager.ClickIsAvailable = true;
        
    }


    public void OpenFpsMode()
    {
        this.menu.SetActive(false);
        melon.SideMenuIsOpen = false;
        fps.OpenFpsMode();
        
        
    }


    public void CloseFpsMode()
    {
        fps.CloseFpsMode();
        
    }
}
