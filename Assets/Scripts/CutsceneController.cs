using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CutsceneController : MonoBehaviour
{
    public Transform playerStartPosition; // The position where the player should start in Scene1
    public Transform cameraStartPosition; // The position where the camera should start in Scene1
    public float panDuration = 5f; // The duration of the camera pan

    private CinemachineVirtualCamera virtualCamera;
    private ObjectMovement playerController;

    private void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        playerController = FindObjectOfType<ObjectMovement>();

        StartCoroutine(StartCutscene());
    }

    private IEnumerator StartCutscene()
    {
        // Disable player controls during the cutscene
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Move the player to the starting position in Scene1
        if (playerStartPosition != null)
        {
            playerController.transform.position = playerStartPosition.position;
        }

        // Move the camera to the starting position in Scene1
        if (virtualCamera != null && cameraStartPosition != null)
        {
            virtualCamera.transform.position = cameraStartPosition.position;
        }

        // Start the camera pan animation
        if (virtualCamera != null)
        {
            // Assuming you have set up a Cinemachine composer for the virtual camera
            CinemachineComposer composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
            if (composer != null)
            {
                float elapsedTime = 0f;
                Vector3 startingPosition = virtualCamera.transform.position;
                Vector3 targetPosition = playerController.transform.position; // Or any other position you want the camera to pan to

                while (elapsedTime < panDuration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsedTime / panDuration);
                    virtualCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
                    yield return null;
                }
            }
        }

        // Enable player controls after the cutscene
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Load Scene1 after the cutscene finishes
        SceneManager.LoadScene("Scene1");
    }
}