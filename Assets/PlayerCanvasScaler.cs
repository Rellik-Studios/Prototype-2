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
            transform.Find("Timer").GetComponent<RectTransform>().localPosition = new Vector3(409, 493, 0);
        }
        else if (gameSettings.Instance.gameMode == gameSettings.eGameModes.timeTrial)
        {
            transform.Find("Timer").GetComponent<RectTransform>().localPosition = new Vector3(573, 317, 0);
        }
    }
}
