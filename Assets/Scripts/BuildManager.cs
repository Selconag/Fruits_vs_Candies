using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildManager : MonoBehaviour
{

    GameManager1 Manager;



    public static BuildManager instance;
        
        private void Awake()
    {
        
        if (instance != null)                           //Burada Tek bir tane build manager olmasını sağladık sahnede hata oluşmasın diye.
        {
            Debug.LogError("More than one BuildManager in scene !");
            return;
        }
        instance = this;                               // Burada Her Gameobjecti bir tane BuildManager'a bağlı olmasını sağladık.
    }

    //public GameObject standartTurretPrefab;
    
    public List<GameObject> standartTurretPrefab;
    private GameObject turretToBuild;

    private void Start()
    {
        turretToBuild = standartTurretPrefab[0];
        Manager = GameObject.FindObjectOfType<GameManager1>();
    }

    public GameObject GetTurretToBuild()                            //Burada İstantiate Edilecek Turret'ı return ile ilgili scripte gönderdik. 
    {
        return turretToBuild;
    }

    public void SelectTurret(int selectedTower,int towerValue)
    {
        var setValue = FindObjectsOfType<Obstacle>();                           //burada Obstacledaki değer kısmına erişmiş olduk.

        if (selectedTower==0)
        {
            turretToBuild=standartTurretPrefab[0];
            
            foreach (var item in setValue)
            {
                item.SetValue(towerValue);
            }
        }
        else if (selectedTower== 1)
        {
            turretToBuild = standartTurretPrefab[1];

            foreach (var item in setValue)
            {
                item.SetValue(towerValue);
            }
        }

        else if (selectedTower == 2)
        {
            turretToBuild = standartTurretPrefab[2];

            foreach (var item in setValue)
            {
                item.SetValue(towerValue);
            }
        }
    }

  
}
