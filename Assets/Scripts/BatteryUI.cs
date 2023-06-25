using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public MouseControlledLight mouseControlledLight;
    public Image[] batteryBars;
    public float[] percentageThresholds;

    private float batteryDuration;

    private void Start()
    {
        batteryDuration = mouseControlledLight.batteryDuration;
    }

    private void Update()
    {
        float batteryPercentage = mouseControlledLight.BatteryPercentage();

        for (int i = 0; i < batteryBars.Length; i++)
        {
            if (percentageThresholds[i] <= batteryPercentage)
                batteryBars[i].enabled = true;
            else
                batteryBars[i].enabled = false;
        }
    }
}
