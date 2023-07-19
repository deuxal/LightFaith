using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Animator fadeAnimator;
    public GameObject fadePanel;

    private static readonly int FadeInParam = Animator.StringToHash("FadeIn");
    private static readonly int FadeOutParam = Animator.StringToHash("FadeOut");

    public void StartFadeIn()
    {
        fadeAnimator.SetBool(FadeInParam, true);
    }

    public void StartFadeOut()
    {
        fadeAnimator.SetBool(FadeOutParam, true);
    }

    public void DeactivateFadePanel()
    {
        fadePanel.SetActive(false);
    }
}
