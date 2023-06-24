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

    public float healAmount = 1;
    [Header("Other Scripts")]
    public ObjectMovement om;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(item == 0)
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
    }

    public void UseItem(int item)
    {
        if (item == 0)
        {
            HPUI.SetActive(false);
            om.Heal(healAmount);
        }
        
    }
}