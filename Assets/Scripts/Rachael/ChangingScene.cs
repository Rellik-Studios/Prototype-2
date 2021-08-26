using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Himanshu;

public class ChangingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void TimerGamePlay()
    {
        gameSettings.Instance.gameMode = gameSettings.eGameModes.timeTrial;
        SceneManager.LoadScene(2);
    }
    public void LocalGamePlay()
    {
        gameSettings.Instance.gameMode = gameSettings.eGameModes.localMultiplayer;
        SceneManager.LoadScene(2);
    }
    public void AIGamePlay()
    {
        gameSettings.Instance.gameMode = gameSettings.eGameModes.AIMultiplayer;
        SceneManager.LoadScene(2);
    }
}
