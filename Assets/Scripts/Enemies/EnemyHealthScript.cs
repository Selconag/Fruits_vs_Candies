using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class EnemyHealthScript : MonoBehaviour
{
    GameManager1 list;
    GeneralCanvasSystem Para;

    EnemyScript enemy;

    GeneralCanvasSystem healthBoard;

    int indexGained;
    float enemyHealth;
    

    XmlDocument itemDataXml;                                                                 //Burada XmlDocument oluşturuyoruz xmldeki verileri çekmek için.

    public bool Alive = true;

    private void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("CrashAnimation");                //Buradan ilgili xml dosyamızı yüklüyoruz.
        itemDataXml = new XmlDocument();                                                    //Burada bir boş bir XmlDosyası oluşturuyoruz.
        itemDataXml.LoadXml(xmlTextAsset.text);                                             //Burada ItemData daki yazıları itemDataXml boş dosyamıza yüklüyoruz.
        
    }

    public void Start()
    {
        
        
        list = GameObject.FindWithTag("GameManager").GetComponent<GameManager1>();
        list.SpawnedEnemies = list.gameObject.GetComponent<GameManager1>().SpawnedEnemies;
        healthBoard = GameObject.FindWithTag("GeneralBoardMenuTag").GetComponent<GeneralCanvasSystem>();
        Para = healthBoard;
        Para.money = Para.GetComponent<GeneralCanvasSystem>().money;                                           ///////////////////


        healthBoard.health = healthBoard.GetComponent<GeneralCanvasSystem>().health;


    }

    private void Update()
    {

    }

    public void TakeDamage(GameObject enemy, float damage)
    {
        foreach (var spawn in list.SpawnedEnemies)
        {
          
            XmlNodeList items = itemDataXml.SelectNodes("/CrushEnemy/Enemy");               //Burada select nodes ile ilgili nodes a gidiyoruz ve Listeliyoruz. !!!!!!!!!!!!

            foreach (XmlNode item in items)
            {
                if (enemy == spawn && enemy.name == item["EnemyName"].InnerText)        //eğer enemy spawndaki listede ise vede enemy xml içindeki herhangi bir enemy name ile denk ise döngüye gir.
                {

                    enemy.gameObject.GetComponent<EnemyScript>().health = enemy.gameObject.GetComponent<EnemyScript>().health - 1 * damage;

                    enemyHealth = enemy.gameObject.GetComponent<EnemyScript>().health;

                    if (enemy.gameObject.GetComponent<EnemyScript>().health <= 0) { 
                        enemy.gameObject.GetComponent<EnemyScript>().Alive = false;             ///fazla para eklemeyi kaldırdık.

                    }
                    break;
                }
            }


        }


        if (enemyHealth <= 0)
        {
            
            if (enemy.gameObject.GetComponent<EnemyScript>().Alive == false)
            {
                DeathEnemy(enemy, list.SpawnedEnemies);
                MoneyAdder();
                enemy.gameObject.GetComponent<EnemyScript>().Alive = true ;
            }
        }



    }

    void DeathEnemy(GameObject enemy, List<GameObject> listing)
    {
        listing.Remove(enemy);
        
        Destroy(enemy);
        CrushAnimations(enemy);
    }

    public void EnemyDestroyer(GameObject enemy)
    {
        healthBoard.health -= 1;
        DeathEnemy(enemy, list.SpawnedEnemies);
    }

    void MoneyAdder()
    {
        Para.money += 100;

    }

    void CrushAnimations(GameObject enemy)
    {
        XmlNodeList items = itemDataXml.SelectNodes("/CrushEnemy/Enemy");               //Burada select nodes ile ilgili nodes a gidiyoruz ve Listeliyoruz. !!!!!!!!!!!!

        foreach (XmlNode item in items)
        {
            if (enemy.name == item["EnemyName"].InnerText)
            {
                //Debug.Log(enemy.name);
                GameObject enemyCrushPrefab = Resources.Load(item["prefabLocation"].InnerText) as GameObject;
                GameObject anim = Instantiate(enemyCrushPrefab, enemy.transform.position, enemy.transform.rotation);
                Destroy(anim, 3f);
               // Debug.Log("Instantiated");
                break;
            }
        }

    }

}
