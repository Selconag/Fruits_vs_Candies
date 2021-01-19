using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    private int selectedTower;
    BuildManager select;
    int TurretValue;
   

    private void Start()
    {
        select= FindObjectOfType<BuildManager>();
       

    }

    public void Karpuz()
    {
        selectedTower = 0;
        TurretValue = 100;
        select.SelectTurret(selectedTower,TurretValue);
        
    }
    public void Elma()
    {
        selectedTower = 1;
        TurretValue = 120;
        select.SelectTurret(selectedTower,TurretValue);
      
    }
    public void Uzum()
    {
        selectedTower = 2;
        TurretValue = 200;
        select.SelectTurret(selectedTower, TurretValue);
       
    }

    public int getTower()
    {
        return selectedTower;
    }
}
