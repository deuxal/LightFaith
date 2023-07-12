using UnityEngine;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;

    public void ActivatePanelA()
    {
        panelA.SetActive(true);
        panelB.SetActive(false);
    }

    public void ActivatePanelB()
    {
        panelA.SetActive(false);
        panelB.SetActive(true);
    }
}
