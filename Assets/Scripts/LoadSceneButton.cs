using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string Level1;

    public void LoadScene()
    {
        SceneManager.LoadScene(Level1);
    }
}

