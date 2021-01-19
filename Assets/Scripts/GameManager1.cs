using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager1 : MonoBehaviour
{
    float currentHealth;

    Lister1 deneme;

    int waveIndex=0;
    
    public int CurrentEnemyNumber;
    float Timer;
    float nextWaveTimer;
    float delayTime;
   
    public float GameHealth;
    GeneralCanvasSystem healthBoard;                                                      
    public List<GameObject> SpawnedEnemies = new List<GameObject>();
    public Quaternion SpawnRotation;

    public bool ClickIsAvailable;                                                   //Bu boolean menu veya herhangi bir anda oyunun mesela turret eklemesini engelliyor.
    public bool FpsGunModeAvailable = false;

    [Header("Spawn Loacation")]
    public Transform spawnPoint;
    

    private void Awake()
    {
        deneme = gameObject.GetComponent<Lister1>();
        deneme.wave = gameObject.GetComponent<Lister1>().wave;                          //Burada Listerın içerisindeki özellikleri kullanarak wave listesi ekliyoruz (panele).
        Debug.Log(Application.dataPath); 
    }

    void Start()
    {
        
        ClickIsAvailable = true;
    
        waveIndex = 0;                                                              //Hangi wave de olduğumuzu tutan integer.
        CurrentEnemyNumber = deneme.wave[waveIndex].EnemyCount;                     //burada waveden panelden ka düşman olması gerektiğini çekiyoruz.
        nextWaveTimer = deneme.wave[waveIndex].StartTime;                           //Burada her wave in bekleme süresini çekiyoruz.
        delayTime = nextWaveTimer;

        healthBoard = GameObject.FindWithTag("GeneralBoardMenuTag").GetComponent<GeneralCanvasSystem>();    //Canvas sisteminde can hanesi için kullanıyoruz.
        healthBoard.health = healthBoard.GetComponent<GeneralCanvasSystem>().health;                        //Burada Can kısmına erişiyoruz.

    }

    private void Update()
    {
        delayTime -= Time.deltaTime;
        
        Timer = Mathf.MoveTowards(Timer, 0, Time.deltaTime);                    //burası timer sistemi
        

        CountEnemiesSpawnerTimer();                                             //burada enemy eklemek için önce süre saydığımız yer.

        if (CurrentEnemyNumber <= 0 && waveIndex<deneme.wave.Count-1)           //burası next wave geçme kısmı.
        {
            NextWave();
            delayTime = nextWaveTimer;
        }

        if (healthBoard.health <= 0)                                            //Can bitince game over olması.
        {
            
            
            Time.timeScale = 0;
        }

    }

    void CountEnemiesSpawnerTimer()
    {
        if (Timer == 0)
        {
            int _spawned = deneme.wave[waveIndex].SpawnedNumber;                
            int _amount = deneme.wave[waveIndex].EnemyCount;
            nextWaveTimer = deneme.wave[waveIndex].EnemyCloningDelay;

            if (_spawned < _amount )
            {
                if(delayTime <= 0) {
                    SpawnEnemy();
                    delayTime = nextWaveTimer;
                }
                if (SpawnedEnemies.Count == 0 && delayTime <= 0)
                {
                    SpawnEnemy();
                    delayTime = nextWaveTimer;
                }
                
            }
            else if (SpawnedEnemies.Count == 0)
            {
                Timer = 5;

                if (waveIndex < (deneme.wave.Count - 1))
                {
                    waveIndex += 1;

                }
                else
                    YouWon();
            }
        }
    }

    void SpawnEnemy()                               //burada instantiate ile enemy spawn ediyoruz.
    {
        deneme.wave[waveIndex].SpawnedNumber += 1;
        GameObject _newEnemy = Instantiate(deneme.wave[waveIndex].EnemyPrefab, spawnPoint.position,spawnPoint.rotation);

        SpawnedEnemies.Add(_newEnemy);
        

    }


    void YouWon()
    {
        Debug.Log("You Win");
        int index = SceneManager.GetActiveScene().buildIndex;

        if (index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index + 1);

        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void NextWave()
    {
        deneme.wave[waveIndex].NextWaveDelay -= Time.deltaTime;
        if (deneme.wave[waveIndex].NextWaveDelay <= 0)
        {
            waveIndex += 1;
            CountEnemiesSpawnerTimer();

        }
    }

    public void MouseOnObject()
    {
         
        ClickIsAvailable= false;
    }

    public void MouseExitObject()
    {

        ClickIsAvailable = true; ;
    }
}
   



