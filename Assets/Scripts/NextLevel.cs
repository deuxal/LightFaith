using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;

    public string nextLevel = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            Invoke("LoadNextLevel", 1f);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void OnLevelWasLoaded(int level)
    {
        instantiatedObject = Instantiate(prefabToInstantiate, new Vector3(0, 0, 0), Quaternion.identity);
        Invoke("DeactivateObject", 1f);
    }

    void DeactivateObject()
    {
        instantiatedObject.SetActive(false);
    }
}