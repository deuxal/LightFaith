using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerController : MonoBehaviour
{
    public GameObject drawerUI;
    public string playerTag = "Player";
    public string drawerTag = "Drawer";

    private bool isPlayerNearby = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && other.CompareTag(drawerTag))
        {
            isPlayerNearby = true;
            drawerUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && other.CompareTag(drawerTag))
        {
            isPlayerNearby = false;
            drawerUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Q))
        {
            ToggleDrawerUI();
        }
    }

    private void ToggleDrawerUI()
    {
        drawerUI.SetActive(!drawerUI.activeSelf);
    }
}
