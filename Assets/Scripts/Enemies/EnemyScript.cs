using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    MovementPointScript movePoints1;

    [Header("Abilities")]
    public float health = 8;
    public bool Alive;
    public float speed = 12;
    float waitTime;
    public float startWaitTime;
    int currentIndex = 0;
    List<string> GetMoveNames = new List<string>();
    Transform Enemy;
    Vector3 pos;

    public Image HealthImage;                                           //EnemyHEalth Ratio simgeleri çin genel can degil (En içteki Kırmızı can simgesi)
    float healthRatio;

    EnemyHealthScript enemyDestroyer;

    private void Awake()
    {
        movePoints1 = GameObject.FindObjectOfType<MovementPointScript>();
        enemyDestroyer = GameObject.FindObjectOfType<EnemyHealthScript>();
        enemyDestroyer = enemyDestroyer.GetComponent<EnemyHealthScript>();
    }
    private void Start()
    {
        Alive = true;
        this.movePoints1.movePoints = movePoints1.MoveListChecker();
        this.healthRatio = health;                                                                           //100delik oran yapacağımız iin canı ilk canı tutuyoruz.
        
    }

    private void FixedUpdate()
    {
        EnemyPathMovement(this.movePoints1.movePoints);
        this.HealthImage.fillAmount = health / this.healthRatio;                       //burada health scriptinin ui üzerindeki can oranını ayarlıyoruz. 
    }

    void EnemyPathMovement(List<Transform> movePoints3)                     //Burada liste halinde verilen yıldızlar sırasıyla gidilmesi gereken yol noktalarına dönüştürülüyor
    {
        if (movePoints3.Count > 0)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, movePoints3[currentIndex].position, speed * Time.deltaTime);
            
           // EnemyRotation(movePoints3[currentIndex].position);
            

            if (Vector3.Distance(transform.position, movePoints3[currentIndex].position) < 0.05f)   //0.05 mden kısa ise yeni bir hareket noktasına geç.
            {

                if (waitTime <= 0)
                {
                    if (currentIndex < movePoints3.Count - 1)
                    {
                        currentIndex = currentIndex + 1;
                    }

                    else
                    {
                        
                        enemyDestroyer.EnemyDestroyer(gameObject);

                    }

                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

/*
    void EnemyRotation(Vector3 point)
    {
        pos = new Vector3(Enemy.transform.position.x - point.x, Enemy.transform.position.y - point.y, Enemy.transform.position.z - point.z);

        if (pos.z > 10)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y +90, this.transform.rotation.z);
        }

        if (pos.z < 10)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y - 90, this.transform.rotation.z);
        }

        if (pos.x > 10)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y + 90, this.transform.rotation.z);
        }

        if (pos.x < 10)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y - 90, this.transform.rotation.z);
        }

    }*/

}
