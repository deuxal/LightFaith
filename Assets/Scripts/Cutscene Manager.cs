using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public Canvas uiCanvas;
    public ObjectMovement playerController;

    void Start()
    {
        // Disable the UI Canvas at the beginning
        uiCanvas.gameObject.SetActive(false);

        // Play the Timeline once the level is loaded
        timeline.Play();
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
        // Resume player movement after the cutscene
        playerController.ResumePlayer();
    }
}