using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<string> inventoryItems = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the newly loaded scene is a gameplay scene
        if (scene.name.StartsWith("Level"))
        {
            // Update the inventory UI in the new scene with the stored inventory items
            InventorySystem inventorySystem = FindObjectOfType<InventorySystem>();
            if (inventorySystem != null)
            {
                inventorySystem.UpdateInventoryUI();
            }
        }
    }

    public void AddInventoryItem(string item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveInventoryItem(string item)
    {
        inventoryItems.Remove(item);
    }

    // Other methods and functionality for managing game state
}

