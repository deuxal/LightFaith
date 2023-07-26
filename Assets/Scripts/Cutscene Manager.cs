using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public Canvas uiCanvas;
    public ObjectMovement playerController;

    private bool timelinePlaying = false; // Flag to track if the timeline is currently playing

    void Start()
    {
        // Disable the UI Canvas at the beginning
        uiCanvas.gameObject.SetActive(false);

        // Play the Timeline once the level is loaded
        timeline.Play();

        // Set the timelinePlaying flag to true
        timelinePlaying = true;

        // Deactivate the ObjectMovement script while the timeline is playing
        playerController.enabled = false;
    }

    public void EnableUIAfterCutscene()
    {
        uiCanvas.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        // Subscribe to the Timeline's signal that triggers when it ends
        timeline.stopped += OnCutsceneFinished;
    }

    void OnDisable()
    {
        // Unsubscribe from the signal when the script is disabled (to avoid memory leaks)
        timeline.stopped -= OnCutsceneFinished;
    }

    void OnCutsceneFinished(PlayableDirector director)
    {
        // The cutscene has finished playing, enable the UI Canvas
        EnableUIAfterCutscene();

        // Reset player walking state to idle
        playerController.SetWalking(false);

        // Activate the ObjectMovement script after the cutscene
        playerController.enabled = true;

        // Resume player movement after the cutscene
        playerController.ResumePlayer();

        // Set the timelinePlaying flag to false
        timelinePlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timeline is still playing
        if (timelinePlaying)
        {
            // If the timeline is playing, disable player movement
            playerController.StopPlayer();
        }
    }
}