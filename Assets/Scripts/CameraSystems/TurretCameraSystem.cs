using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCameraSystem : MonoBehaviour
{
    float mouseY;
    float mouseX;
    float mouseSensivity=3f;                            //Yapmazsan hareket etmez

    float lastClickTime;
    Transform cams;
    Camera TurretCam;
    public bool fpsOpen;

    Transform mainCam;
    Camera OpenCloseMainCam;

    public float maxAngleY;
    public float maxAngleX;

    [Header("FpsGunSettings")]
    
    float FpsNextFire=0;
    public float FpsfireRate = 0.25f;
    public float FpsFireSpeed =10f;
    public float FpsWeaponRange = 250f;
    public Transform GunEnd;
    public LineRenderer laserLine;
    public GameObject bullet;
    GameObject bul;

    void Start()
    {
        this.fpsOpen = false;
        this.cams = this.gameObject.transform.GetChild(0).transform;                        //MainCameranın indexi sonra değiştir.
        TurretCam = this.cams.gameObject.GetComponent<Camera>();
        this.GunEnd = this.gameObject.transform.GetChild(2).transform;                      //FirePoint  indexi sonra değiştir.

        this.TurretCam.gameObject.active = false;

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        OpenCloseMainCam = mainCam.GetComponent<Camera>();

        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled=false;   //Burada componenti kapattık
    

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
            FpsCameraOn();

        if (Input.GetKeyDown(KeyCode.X))
            FpsCameraOff();

        if (fpsOpen == true)
        {
            FpsMouseMovement();
        }

    }

    private void FpsCameraOn()
    {

        this.OpenCloseMainCam.gameObject.active = false;
        
        this.TurretCam.gameObject.active = true;

        fpsOpen = true;
  
    }

    private void FpsCameraOff()
    {
        this.OpenCloseMainCam.gameObject.active = true;
  
        this.TurretCam.gameObject.active = false;

        fpsOpen = false;

        laserLine.enabled = false;

    }

    void FpsMouseMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mouseY += Input.GetAxis("Mouse Y") * mouseSensivity;
        mouseY = Mathf.Clamp(mouseY, -maxAngleY, +maxAngleY);

        mouseX += Input.GetAxis("Mouse X") * mouseSensivity;
        mouseX = Mathf.Clamp(mouseX, -maxAngleX, +maxAngleX);

        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

        if (Input.GetMouseButtonDown(0) && fpsOpen == true && Time.time > FpsNextFire)
        {
            FpsNextFire = Time.time + FpsfireRate;


            FpsFireSystem();
        }

    }

   void FpsFireSystem()
    {
        Vector3 rayOrigin = TurretCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        laserLine.enabled = true;
        RaycastHit hit;

        laserLine.SetPosition(0,this.GunEnd.position);

        if(Physics.Raycast(rayOrigin,TurretCam.transform.forward, out hit , FpsWeaponRange))
        {
            laserLine.SetPosition(1, hit.point);
 
        }

        else
        {
            laserLine.SetPosition(1, rayOrigin + (TurretCam.transform.forward * FpsWeaponRange));
            
            
        }
        laserLine.forceRenderingOff = true;
        
        bul = Instantiate(bullet, this.GunEnd.transform.position, laserLine.transform.rotation);

        bul.GetComponent<Rigidbody>().AddForce(laserLine.transform.forward * FpsFireSpeed, ForceMode.Impulse);       //Bu kod ile ateş ediyoruz.

        Destroy(bul, 3f);
    }


}
