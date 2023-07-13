using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Reference to the GameManager script
    private GameManager gameManager;

    public Transform InventoryUI;
    public GameObject HPUI;
    public GameObject BATTERYUI;
    public GameObject BULLETUI;

    public MouseControlledLight mouseControlledLight;
    public Projectile projectile;
    public HealthSystemAttribute healthSystem;
    public HealthBar healthBar;

    private void Start()
    {
        gameManager = GameManager.Instance;
        mouseControlledLight = FindObjectOfType<MouseControlledLight>();
        projectile = FindObjectOfType<Projectile>();
        healthSystem = FindObjectOfType<HealthSystemAttribute>();


        // Update the inventory UI on start
        UpdateInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUI.gameObject.SetActive(!InventoryUI.gameObject.activeInHierarchy);
        }
    }

    public void GetItem(int item)
    {
        Debug.Log(item);

        if (item == 0)
        {
            HPUI.SetActive(true);
            gameManager.AddInventoryItem("HP");
        }
        else if (item == 1)
        {
            BATTERYUI.SetActive(true);
            gameManager.AddInventoryItem("Battery");
        }
        else if (item == 2)
        {
            BULLETUI.SetActive(true);
            gameManager.AddInventoryItem("Bullet");
        }
    }

    public void UseItem(int item)
    {
        Debug.Log("use: " + item);
        if (item == 0)
        {
            HPUI.SetActive(false);
            healthSystem.ResetHealth();
            healthBar.UpdateHealthBars();
            gameManager.RemoveInventoryItem("HP");
        }
        else if (item == 1)
        {
            BATTERYUI.SetActive(false);
            mouseControlledLight.ResetBatteryDuration();
            gameManager.RemoveInventoryItem("Battery");
        }
        else if (item == 2)
        {
            BULLETUI.SetActive(false);
            ResetAmmoCount();
            gameManager.RemoveInventoryItem("Bullet");
        }
    }

    private void ResetAmmoCount()
    {
        projectile.ResetAmmoCount();
    }

    public void UpdateInventoryUI()
    {
        // Clear the UI first
        ClearInventoryUI();

        // Iterate through the inventory items and activate the corresponding UI elements
        foreach (string item in gameManager.inventoryItems)
        {
            
                Debug.Log("Activating item: " + item);
                if (item == "HP")
            {
                HPUI.SetActive(true);
            }
            else if (item == "Battery")
            {
                BATTERYUI.SetActive(true);
            }
            else if (item == "Bullet")
            {
                BULLETUI.SetActive(true);
            }
        }
    }

    private void ClearInventoryUI()
    {
        HPUI.SetActive(false);
        BATTERYUI.SetActive(false);
        BULLETUI.SetActive(false);
    }
}