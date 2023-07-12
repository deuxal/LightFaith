using UnityEngine;
using TMPro;

public class TextMessageTrigger : MonoBehaviour
{
    public Transform triggerPosition;
    public TMP_Text textElement;
    public bool toggleOnTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (toggleOnTrigger)
                ToggleTextElement(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (toggleOnTrigger)
                ToggleTextElement(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!toggleOnTrigger)
                ToggleTextElement(true);
        }
    }

    private void ToggleTextElement(bool isActive)
    {
        textElement.gameObject.SetActive(isActive);
    }
}

