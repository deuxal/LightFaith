using UnityEngine;

public class TextMessageTrigger : MonoBehaviour
{
    public GameObject textPrefab; // Prefab of the text object to display
    public Transform triggerPosition; // Position where the text should appear
    public string message; // The message to display

    private GameObject textObject; // Reference to the instantiated text object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowTextMessage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideTextMessage();
        }
    }

    private void ShowTextMessage()
    {
        if (textObject == null)
        {
            textObject = Instantiate(textPrefab, triggerPosition.position, Quaternion.identity);
            TextMesh textMesh = textObject.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.text = message;
            }
        }
        else
        {
            textObject.SetActive(true);
        }
    }

    private void HideTextMessage()
    {
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }
}

