using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySystem : MonoBehaviour
{

    public Transform InventoryUI;
    public GameObject HPUI;
    public GameObject KEY1UI;
    public GameObject KEY2UI;
    public GameObject BATTERYUI;
    public GameObject BULLETUI;

    public MouseControlledLight mouseControlledLight;
    public Projectile projectile;
    public HealthSystemAttribute healthSystem;
    public HealthBar healthBar;

    private void Start()
    {
        mouseControlledLight = FindObjectOfType<MouseControlledLight>(); // Find the MouseControlledLight script in the scene
        projectile = FindObjectOfType<Projectile>();
        healthSystem = FindObjectOfType<HealthSystemAttribute>();
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
        }
        if (item == 1)
        {
            BATTERYUI.SetActive(true);
        }
        if (item == 2)
        {
            BULLETUI.SetActive(true);
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
        }
        else if (item == 1)
        {
            BATTERYUI.SetActive(false);
            mouseControlledLight.ResetBatteryDuration();
        }
        else if (item == 2)
        {
            BULLETUI.SetActive(false);
            ResetAmmoCount();
        }
    }
    private void ResetAmmoCount()
    {
        projectile.ResetAmmoCount();
    }
}

