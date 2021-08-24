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

        public bool playing = false;
        
        public static gameManager Instance
        {
            get => m_instance;
            private set => m_instance = value;
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
            if(playing)
                gameTimer += Time.deltaTime;
        }

        public void ResetTimer()
        {
            gameTimer = 0f;
        }
    }
}