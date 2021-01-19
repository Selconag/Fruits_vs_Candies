using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Obstacle : MonoBehaviour 
{

    public Color mouseOverColor;
    Color realColor;
    public bool mouseOver =false;
    bool ObsClick;

    private GameObject turret;
    BuildManager buildManager;
    GeneralCanvasSystem Cash;
    public int value;
    public Vector3 GunOfset;
    private GameManager1 gameManager;
    PlayingMenu playMenu;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager1>();
        playMenu = GameObject.FindObjectOfType<PlayingMenu>();
        realColor =GetComponent<Renderer>().material.color;
        Cash = GameObject.FindWithTag("GeneralBoardMenuTag").GetComponent<GeneralCanvasSystem>();                       /////////////////
        Cash.money = Cash.GetComponent<GeneralCanvasSystem>().money;                                           /////////////////
        buildManager = BuildManager.instance;
        value = 100;
    }

    private void FixedUpdate()
    {
        Cash.money = Cash.GetComponent<GeneralCanvasSystem>().money;                                           /////////////////            
        ObsClick =gameManager.ClickIsAvailable ;
    }

   private void OnMouseDown()                                                      //MOUSE TIKLANDIĞI ANDA
    {
        Debug.Log("Tespit Var");
        if (buildManager.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build there - TODO: Display on screen .");
            return;
        }

        if (Cash.money >= value && ObsClick == true && playMenu.menuIsActive == false) {                                          //Obstacle a tıkladığımzda para 100 den fazla ise turret ekliyor
            GameObject turretToBuild = buildManager.GetTurretToBuild();                                                 //Burada BuildManager aracılığıyla turret prefabını aldık.                                 
            turret = (GameObject)Instantiate(turretToBuild, transform.position + GunOfset, transform.rotation);     //Instantiate ile turret prefabını istenilen konuma eklendi;
            Cash.money -= value;
            gameManager.ClickIsAvailable = true;
        }
    }


     private void OnMouseEnter()                                                                                    //Mouse üzerine geldiğinde olan aktiviteler.
     {
         
         if(ObsClick == true) { 
             mouseOver = true;
             GetComponent<Renderer>().material.color = mouseOverColor;                                               //mouse üzerine gelince rengi değiştir (panelde renk).
         }
     }
    
     private void OnMouseExit()                                                                                  //Mouse üzerinden kalkınca olan aktiviteler.
     {
         if (ObsClick == true)
         {
             mouseOver = false;
             GetComponent<Renderer>().material.color = realColor;                                                    //mouse üzerinden gidince rengi değiştir tutuğumuz asıl renge.
         }
     }

 

   

    public void SetValue(int x=100)
    {
        value = x;
    }

}



