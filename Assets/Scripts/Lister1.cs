using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lister1 : MonoBehaviour
{
    [System.Serializable]
    public class WaveNumber
    {
        public GameObject EnemyPrefab;
        public int EnemyCount;
        public int SpawnedNumber;
        public float StartTime;
        public float EnemyCloningDelay;
        public float NextWaveDelay;
    }

    [SerializeField]public List<WaveNumber> wave = new List<WaveNumber>();              //Bu şekilde Wavenumber özelliğine sahip listeler oluşturduk. 

}