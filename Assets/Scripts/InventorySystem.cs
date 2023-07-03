using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    private void Start()
    {
        mouseControlledLight = FindObjectOfType<MouseControlledLight>(); // Find the MouseControlledLight script in the scene
        projectile = FindObjectOfType<Projectile>();
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
            KEY1UI.SetActive(true);
        }
        if (item == 3)
        {
            KEY2UI.SetActive(true);
        }
        if (item == 4)
        {
            BULLETUI.SetActive(true);
        }
    }

    public void UseItem(int item)
    {
        if (item == 0)
        {
            HPUI.SetActive(false);
        }
        else if (item == 1)
        {
            BATTERYUI.SetActive(false);
            mouseControlledLight.ResetBatteryDuration();
        }
        else if (item == 2)
        {
            KEY1UI.SetActive(false);
        }
        else if (item == 3)
        {
            KEY2UI.SetActive(false);
        }
        else if (item == 4)
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

