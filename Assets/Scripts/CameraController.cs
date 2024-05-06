using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject coliseoMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject gymMenu;
    [SerializeField] private GameObject digimonStatus;
    
    public Transform coliseo;
    public Transform coliseoCamara;
    public Transform shop;
    public Transform shopCamara;
    public Transform gym;
    public Transform gymCamara;
    public Transform islandCamara;

    private void Start()
    {
        this.transform.position = islandCamara.position;
        this.transform.rotation = islandCamara.rotation;
    }

    public void Shop()
    {
        CerrarMenu();
        this.transform.position = new Vector3(0, 0, 0);
    
        shopMenu.SetActive(true);
        transform.LookAt(shop);
        transform.position = shopCamara.position;
    }

    public void Coliseo()
    {  
        CerrarMenu();
        this.transform.position = new Vector3(0, 0, 0);
        
        coliseoMenu.SetActive(true);
        //transform.LookAt(coliseo);
        transform.rotation = coliseoCamara.rotation;
        transform.position = coliseoCamara.position;
    }

    public void Gym()
    {
        CerrarMenu();
        this.transform.position = new Vector3(0, 0, 0);
        
        gymMenu.SetActive(true);
        transform.LookAt(gym);
        transform.position = gymCamara.position;
        transform.rotation = gymCamara.rotation;
    }
    public void DigimonStatus()
    {
        CerrarMenu();
        this.transform.position = new Vector3(0, 0, 0);
        this.transform.position = islandCamara.position;
        this.transform.rotation = islandCamara.rotation;
        
        digimonStatus.SetActive(true);
       
    }

    public void MenuExit(GameObject menu)
    {
        menu.SetActive(false);
        this.transform.position = islandCamara.position;
        this.transform.rotation = islandCamara.rotation;
    }

    public void CerrarMenu()
    {
        gymMenu.SetActive(false);
        shopMenu.SetActive(false);
        coliseoMenu.SetActive(false);
        digimonStatus.SetActive(false);
    }
    
    
    
}
