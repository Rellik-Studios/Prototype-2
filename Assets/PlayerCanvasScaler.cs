using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;

public class PlayerCanvasScaler : MonoBehaviour
{
    
    void Start()
    {
        if (gameSettings.Instance.gameMode == gameSettings.eGameModes.localMultiplayer)
        {
            transform.Find("HealthBar").GetComponent<RectTransform>().localPosition = new Vector3(-428, -710, 0);
            transform.Find("Timer").GetComponent<RectTransform>().localPosition = new Vector3(508, 659, 0);
        }
        else if (gameSettings.Instance.gameMode == gameSettings.eGameModes.timeTrial)
        {
            transform.Find("Timer").GetComponent<RectTransform>().localPosition = new Vector3(768, 426, 0);
            transform.Find("HealthBar").GetComponent<RectTransform>().localPosition = new Vector3(-717, -478, 0);
        }
    }
}
