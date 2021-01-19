using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;


public class SeedBullet : MonoBehaviour
{
    private Transform target;

    public float SeedDamage;                         //burada kurşunların özellikleri gibi ayarları yapıyoruz.

    public float speed = 7f;

    public Gun BulletDamage;

    public int  Level;

    public EnemyHealthScript hadi;

    GameManager1 gameManager;

    bool otoCont;

    public void Seek (Transform _target , bool _otoCont)
    {
        target = _target;

        otoCont=_otoCont;
    }

    private void Start()
    {

        BulletDamage = GameObject.FindObjectOfType<Gun>();
        hadi = GameObject.FindObjectOfType<EnemyHealthScript>();

        gameManager = GameObject.FindObjectOfType<GameManager1>();

    }

    private void Update()
    {
        BulletSettings();

         if ( otoCont == false ) {

            if (target == null)
            {
                Destroy(gameObject,3f);
                return;
            }

            

             Vector3 dir = target.position - transform.position;
            
             float distanceThisFrame = speed * Time.deltaTime;

            if(dir.magnitude <= distanceThisFrame)
            {
                 HitTarget(target.gameObject);
                 Destroy(gameObject);                                                                        //Kurşun hedefe varınca yok ettik kurşunu.
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        }

        else
        {

        }
    }

    void BulletSettings()
    {
        Level = BulletDamage.Level;
        SeedDamage = BulletDamage.Damage[Level];
    }


    void HitTarget (GameObject  enemy)
    {
        hadi.TakeDamage(enemy,SeedDamage);                                            //EnemyHealthScriptinden düşmanın canını ve Damageini gönderdik.
    }


    public void OnTriggerEnter(Collider col)
    {
        for(int i = 0; i < gameManager.SpawnedEnemies.Count; i++) { 
            if (col.gameObject.name==gameManager.SpawnedEnemies[i].name)
            {
                HitTarget(col.gameObject);
                //Debug.Log(col.gameObject);
                Destroy(gameObject);
            }
            }
 }

}
