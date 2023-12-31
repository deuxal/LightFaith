using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerToggle : MonoBehaviour
{
    public GameObject drawerUI;
    public Transform player;
    public float proximityDistance = 2f;

    private bool isPlayerClose = false;

    public int itemsToGenerate = 1;
    public List<int> items;

    public void Start()
    {
        for (int i = 0; i < itemsToGenerate; i++)
        {
            items.Add(Random.Range(0,3));
        }
    }

    private void Update()
    {
        // Check the distance between the player and the Drawer sprite
        float distance = Vector3.Distance(player.position, transform.position);

        // If the player is close enough to the Drawer sprite and it's not already toggled, toggle the DrawerUI
        if (distance <= proximityDistance && !isPlayerClose)
        {
            ToggleDrawerUI(true);
            isPlayerClose = true;
        }
        // If the player is not close enough to the Drawer sprite and it's already toggled, untoggle the DrawerUI
        else if (distance > proximityDistance && isPlayerClose)
        {
            ToggleDrawerUI(false);
            isPlayerClose = false;
        }
    }

    private void ToggleDrawerUI(bool isActive)
    {
        if (isActive == true) {
            DrawerUI dui = drawerUI.GetComponent<DrawerUI>();
            dui.Setup(items, (id) => { OnItemUsed(id); });

        }
        drawerUI.SetActive(isActive);
    }

    public void OnItemUsed(int id)
    {
        Debug.Log(id);
        items[id] = -1;
    }
}
