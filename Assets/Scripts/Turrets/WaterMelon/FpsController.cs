using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    
    public Gun gun;
    public GunMenuScript Menu;
    private GameManager1 gameManager;
    float FireStartDelay;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager1>();
        
        gun = gun.GetComponent<Gun>();
        Menu = Menu.GetComponent<GunMenuScript>();

    }
   
   

    private void Update()
    {
        if (gun.control == true)
        {
            gun.FpsMouseMovement();

            if (Input.GetKeyDown(KeyCode.Z))                   //Fps modu kapatma tuşu.
            {
                Menu.CloseFpsMode();
            }
        }

        else
        {
            gun.AutoPilot();
        }
    }

    public void ClosePage()
    {
    

    }


    public void OpenFpsMode()
    {
        gun.FpsModeOpener();
        gun.FpsCameraOn();

    }


    public void CloseFpsMode()
    {
        gun.FpsModeCloser();
        gun.FpsCameraOff();
    }

}
