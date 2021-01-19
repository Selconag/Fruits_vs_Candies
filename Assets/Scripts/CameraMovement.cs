using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    GameManager1 gameManager;

    [Header("Camera")]
    public GameObject CameraObject;
    private Transform CameraPosition;
    public float MinX;
    public float MinZ;

    [Header("")]    
    public float MaxX;
    public float MaxZ;

    [Header("Camera Movement Settings")]
    public float movementSpeed = 0.2f;
    public float movementTime,sensivity;
    private Vector3 dragOrigin;

    [Header("Camera Zoom Settings")]
    public float minFov;
    public float maxFov;
    public float Fov;
    

    // Start is called before the first frame update
    void Start()
    {        
        Fov = CameraObject.GetComponent<Camera>().fieldOfView;                        //Burada o erişilen gameobjectin camera sından fieldOf erişiyoruz.
        CameraPosition = CameraObject.GetComponent<Transform>();

        gameManager = GameObject.FindObjectOfType<GameManager1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.FpsGunModeAvailable == false)
        {
            CameraMouseMovement();
            MouseZoom();
        }
        
    }

    void CameraMouseMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;               //Şuanki mouse konumu.
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);              //Burada Son konum eksi ilk konum yaparak ScreenToWievportPoint (Sol Alt Köşe[(0,0,0)] , Ekranın Ortası[(0.5,0.5,0.5)] , Sağ Üst Köşe[(1,1,1)] e göre konum belirliyoruz
        Vector3 move = new Vector3(pos.x * movementSpeed, 0, pos.y * movementSpeed);                    //Burada move ile yeni hareket noktası beliliyoruz.

        transform.Translate(move, Space.World);                                                         //Translate ile konumunu değiştiriyoruz.

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), transform.position.y,
        Mathf.Clamp(transform.position.z, MinZ, MaxZ));
    }

    void MouseZoom()
    {
        Fov -= Input.GetAxis("Mouse ScrollWheel") *sensivity ;
        Fov = Mathf.Clamp(Fov, minFov, maxFov);
        Camera.main.fieldOfView = Fov;
    }


}

