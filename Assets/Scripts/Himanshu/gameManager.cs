using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Himanshu
{
    public class gameManager : MonoBehaviour
    {
        private static gameManager m_instance;

        public float gameTimer { get; private set; }
        public float gameTimerP2 { get; private set; }

        public bool playing = false;
        
        public static gameManager Instance
        {
            get => m_instance;
            private set => m_instance = value;
        }

        private void Awake()
        {
            gameSettings.Instance.Spawn();
            gameSettings.Instance.ChangeMode();
        }

        private IEnumerator Start()
        {
            if (Instance == null)
                Instance = this;

            else
                Destroy(this.gameObject);



            if (gameSettings.Instance.gameMode == gameSettings.eGameModes.localMultiplayer)
            {
                gameTimer = 0f;
                gameTimerP2 = 0f;
            }
            if (gameSettings.Instance.gameMode == gameSettings.eGameModes.timeTrial)
            {
                gameTimer = 0f;
            }
            
            
            yield return new WaitForSeconds(3f);
            playing = true;
        }

        private void Update()
        {
            if (playing)
            {
                gameTimerP2 += Time.deltaTime;
                gameTimer += Time.deltaTime;
            }
        }

        public void ResetTimer(int index = 1)
        {
            if (index == 1)
                gameTimer = 0f;
            else
                gameTimerP2 = 0f;
        }
    }
}