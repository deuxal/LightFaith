using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject[] items; // Array of item game objects in the drawer
    public GameObject drawerUI; // Reference to the DrawerUI canvas image
    public Transform inventoryUI; // Reference to the inventory UI

    private bool isOpen = false; // Flag to track if the drawer is open

    private void Start()
    {
        drawerUI.SetActive(false); // Hide the DrawerUI at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleDrawer();
        }

        if (isOpen && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                GameObject item = hit.collider.gameObject;

                // Check if the clicked object is one of the items in the drawer
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == item)
                    {
                        // Add the item to the inventory
                        InventorySystem inventorySystem = inventoryUI.GetComponent<InventorySystem>();
                        if (inventorySystem != null)
                        {
                            inventorySystem.GetItem(i);
                        }

                        // Remove the item from the drawer
                        Destroy(item);
                        break;
                    }
                }
            }
        }
    }

    // Open or close the drawer
    public void ToggleDrawer()
    {
        isOpen = !isOpen;

        drawerUI.SetActive(isOpen); // Show/hide the DrawerUI

        // Handle drawer visual changes (e.g., play animations, change sprite)
        // ...
    }
}

