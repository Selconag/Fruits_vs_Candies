using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool control;

    public GameObject CurrentTarget;

    public GameObject SeedObject;

    public GameObject SideMenu;

    public Transform DetectCircle;

    public Transform firePoint;

    public Transform RotateSection;

    public ParticleSystem areaPartical;

    GeneralCanvasSystem Para;                                                                          /////////////////

    public bool areaControl = false;                                                                //Bu range dairesinin kontrolünü sağlıyor.

    public bool SideMenuIsOpen;                                                                     //Bu turret geliştime menüsünün menüsünü açıyor.

    public int Level;

    private GameManager1 gameManager;

    public float FireStartDelay;

    public float fireRate;

    float lastClickTime;

    public float[]
            Range,
            Damage;

    float _detectRange;

    [Header("FpsMode")]



    bool crosshair;
    Canvas crosshairCanvas;

    float mouseY;
    float mouseX;
    float mouseSensivity = 3f;                            //Yapmazsan hareket etmez

   
    Transform cams;
    Camera TurretCam;

    Transform mainCam;
    Camera OpenCloseMainCam;

    public float maxAngleY;
    public float maxAngleX;

    [Header("FpsGunSettings")]
    
    float FpsNextFire = 0;
    public float FpsfireRate = 0.25f;
    public float FpsFireSpeed = 10f;
    public float FpsWeaponRange = 250f;
    public Transform GunEnd;
    public LineRenderer laserLine;
    public GameObject bullet;
    GameObject bul;


    void Start()
    {

        Para = GameObject.FindWithTag("GeneralBoardMenuTag").GetComponent<GeneralCanvasSystem>();                   ///////////////////
        Para.money = Para.GetComponent<GeneralCanvasSystem>().money;                                                /////////////////
        this.SideMenuIsOpen = false;
        this.SideMenu.SetActive(false);

        ParticleSystem.MainModule psMain = areaPartical.main;                                           //Burada particle system ile range dairesinini yapıyoruz.   
        ParticleSystem.ShapeModule psShape = areaPartical.shape;

        areaPartical.Stop();                                                                            //başlangıçta oyuna range dairesi kapalı olsun diye yapıyoruz.

        Level = 0;

        gameManager = GameObject.FindObjectOfType<GameManager1>();


        if (gameManager == null)
        {
            Debug.LogError("GameManager Not Found");
        }

        gameManager.ClickIsAvailable = true;

        FpsModeSettings();
    }

    private void LateUpdate()
    {
        AreaVFX();                                                                                      //Burada belirli aralıklarla turret dairesini çalıştırıyoruz.
    }



    // Update is called once per frame
    void Update()
    {
        if (gameManager.FpsGunModeAvailable == false && this.control == false)
        {
            AutoPilot();

        }

   

    }

        void SearchTarget()
        {
            try
            {
                foreach (var enemy in gameManager.SpawnedEnemies)                                           //game manager her enemyi tutar o listenin iindeki her enemy için.
                {
                    Vector3 ProjectionOnGround = new Vector3(enemy.transform.position.x, this.transform.position.y, enemy.transform.position.z);

                    if (Vector3.Distance(transform.position, ProjectionOnGround) < Range[Level])                //enemy uzaklığı range uzaklığından kısa ise düşman olarak belirle.
                    {
                        CurrentTarget = enemy;
                    }
                }
            }
            catch (MissingReferenceException) {                                                                 //burada olaki düşman yoksa hata vermesin diye.
                CurrentTarget = null;
            }
        }

        void TurretRotation()
        {
            if (Vector3.Distance(gameObject.transform.position, CurrentTarget.transform.position) < Range[Level])
            {
                Vector3 dir = CurrentTarget.transform.position - gameObject.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                RotateSection.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }

        void AreaVFX()
        {
            ParticleSystem.MainModule psMain = areaPartical.main;
            ParticleSystem.ShapeModule psShape = areaPartical.shape;

            psShape.radius = Range[Level];                                                          //Range levele göre daire efektinin yarıçapı artıyor.
        }

        private void OnMouseDown()
        {
            float timeSinceLastClicked = Time.time - lastClickTime;
        if (timeSinceLastClicked <= 0.7f && gameManager.FpsGunModeAvailable == false)                                                      //Burada double click yapmaya çalıştım.
            {
                this.SideMenuIsOpen = true;
                this.SideMenu.SetActive(true);

            }
            else
            {
                if (areaControl == false && gameManager.FpsGunModeAvailable == false)
                {

                    this.areaPartical.Play();
                    this.areaControl = true;
                }
                else
                {
                    this.areaPartical.Stop();
                    this.areaControl = false;
                }
                lastClickTime = Time.time;
            }

        }

        void AttackEnenmies()
        {

            if (Vector3.Distance(gameObject.transform.position, CurrentTarget.transform.position) < Range[Level])
            {

                GameObject seedGo = (GameObject)Instantiate(SeedObject, firePoint.position, SeedObject.transform.rotation);
                SeedBullet seed = seedGo.GetComponent<SeedBullet>();


                if (seed != null)
                {
                
                    seed.Seek(CurrentTarget.transform,control);
                }

            }

            else
            {
                CurrentTarget = null;
            }
            FireStartDelay = fireRate;

        }

        void EnemyListClearer(GameObject target) {                                                  //Burası ölen düşmanların üşman listesinden silinmesini sağlıyor game managerdaki.

            for (int a = 0; a < gameManager.SpawnedEnemies.Count - 1; a++) {
                if (gameManager.SpawnedEnemies[a].name == target.name)
                {
                    gameManager.SpawnedEnemies.Remove(target);

                }
            }
        }

        public void GunLevelUpper()                                                               //Şimdilik buraya ekledim burada turretin level up olayını ekledim.
        {
            if (this.Level < Range.Length - 1 && Para.money >= 50)
            {
                this.Level += 1;
                Para.money -= 50;
                Debug.Log("New Level is = " + Level);
            }
        }

        public void AutoPilot()
        {
            _detectRange = Range[Level];                                                                   //Burada rangein levelini listeden tespit ediyoruz (liste panelde).

            if (CurrentTarget == null)
            {
                SearchTarget();                                                                           //Burası enemy arama kısmı
            }

            if (CurrentTarget != null)
            {
                TurretRotation();                                                                        //Bu turretın düşman takip fonksiyonu

                Vector3 ProjectionOnGround = new Vector3(CurrentTarget.transform.position.x, transform.position.y, CurrentTarget.transform.position.z);
                if (FireStartDelay <= 0f)                                                               //Burada ateş etmeyi delayli yaptık.
                {
                    AttackEnenmies();

                }

                FireStartDelay -= Time.deltaTime;
            }
        }

























    /// <summary>
    /// ///////////////////FPS MODU //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

        void FpsModeSettings()
    {
        this.crosshairCanvas = gameObject.transform.GetChild(4).GetComponent<Canvas>();
                                    //Oyunun crosshairini kapattık.

        gameManager.FpsGunModeAvailable = false;
        this.cams = gameObject.transform.GetChild(0).transform;                        //MainCameranın indexi sonra değiştir.
        this.TurretCam = cams.gameObject.GetComponent<Camera>();
        

        this.GunEnd = this.gameObject.transform.GetChild(2).transform;                      //FirePoint  indexi sonra değiştir.

        this.mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        this.OpenCloseMainCam = mainCam.GetComponent<Camera>();

        this.laserLine = GetComponent<LineRenderer>();
        this.laserLine.enabled = false;   //Burada componenti kapattık
    }


    public void FpsCameraOn()
    {
        crosshairCanvas.enabled = true; 

        gameManager.ClickIsAvailable = false;

        TurretCam.enabled = true;
        OpenCloseMainCam.gameObject.active = false;
        

        this.gameManager.FpsGunModeAvailable = true;
        

        areaPartical.Stop();
        areaControl = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void FpsCameraOff()
    {
        this.crosshairCanvas.enabled = false;

        this.gameManager.ClickIsAvailable = true;                                //bunu yaparak fps modunda durduk yere turret eklemesini engelledik.
        OpenCloseMainCam.gameObject.active = true;
        OffAllCameras();
        TurretCam.enabled = false;

        gameManager.FpsGunModeAvailable = false;

        laserLine.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void FpsMouseMovement()
    {
        
        

        mouseY += Input.GetAxis("Mouse Y") * mouseSensivity;
        mouseY = Mathf.Clamp(mouseY, -maxAngleY, +maxAngleY);

        mouseX += Input.GetAxis("Mouse X") * mouseSensivity;
        

        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

        if (Input.GetMouseButtonDown(0) && gameManager.FpsGunModeAvailable == true && Time.time > FpsNextFire)
        {
            this.FpsNextFire = Time.time + FpsfireRate;


            FpsFireSystem();
        }

    }

    public void FpsFireSystem()
    {
        Vector3 rayOrigin = TurretCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        laserLine.enabled = true;
        RaycastHit hit;

        laserLine.SetPosition(0, this.GunEnd.position);

        if (Physics.Raycast(rayOrigin, TurretCam.transform.forward, out hit, FpsWeaponRange))
        {
            laserLine.SetPosition(1, hit.point);

        }

        else
        {
            laserLine.SetPosition(1, rayOrigin + (TurretCam.transform.forward * FpsWeaponRange));


        }
        laserLine.forceRenderingOff = true;

        bul = Instantiate(SeedObject, this.GunEnd.transform.position, laserLine.transform.rotation);

        bul.GetComponent<Rigidbody>().AddForce(laserLine.transform.forward * FpsFireSpeed, ForceMode.Impulse);       //Bu kod ile ateş ediyoruz.
        
        //Destroy(bul, 100f * Time.deltaTime);
        Debug.Log("Destroyed bullet");
    }

    void OffAllCameras()
    {
        object[] Cams = GameObject.FindObjectsOfType(typeof(Camera));
        foreach (Camera C in Cams)
        {
            if (C.name != OpenCloseMainCam.name)
            {
                C.enabled = false;
            }
        }
    }

    public void FpsModeOpener()
    {
        this.control = true;
    }

    public void FpsModeCloser()
    {
        this.control = false; ;
    }


}
