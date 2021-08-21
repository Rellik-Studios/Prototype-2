using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(1);
    }
    public void LocalGamePlay()
    {
        SceneManager.LoadScene(2);
    }
    public void AIGamePlay()
    {
        SceneManager.LoadScene(3);
    }
}
