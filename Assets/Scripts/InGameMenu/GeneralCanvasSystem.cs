using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralCanvasSystem : MonoBehaviour
{
    [Header("Health Section")]
    public TextMeshProUGUI healthBoard;
    public float health = 100;

    [Header("Money Section")]
    public TextMeshProUGUI moneyBoard;
    public float money;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBoardUpdater();
        MoneyBoardUpdater();
    }

    void HealthBoardUpdater()                                                           //Burada UI olarak healthi gösteriyoruz.
    {
        healthBoard.text = ":" + health;
    }

    void MoneyBoardUpdater()
    {
        moneyBoard.text = ":" + money;
    }


}
