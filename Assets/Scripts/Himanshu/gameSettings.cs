using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Himanshu
{
    public class gameSettings: MonoBehaviour
    {
        private static gameSettings m_instance;
        public static gameSettings Instance => m_instance;

        public GameObject player1;
        public GameObject player2;
        
        private int m_numberOfLaps;
        [SerializeField] private int maxLaps = 1;
        [SerializeField] private int minLaps = 5;

        public int numberOfLaps
        {
            get => m_numberOfLaps; 
            
            //Basically clamping from minLaps to maxLaps
            set => m_numberOfLaps = value <= maxLaps? value >= minLaps? value: minLaps : maxLaps;
        }

        public void Spawn()
        {
            
            var p1Pos = GameObject.FindGameObjectWithTag("Player1_Manager").transform.position;
            var p1 = Instantiate(player1, p1Pos, Quaternion.identity);
            p1.tag = "Player1";
            p1.GetComponent<PlayerInput>().index = 1;
            p1.GetComponent<PlayerManager>().playerIndex = 1;
            p1.transform.Find("Main Camera").GetComponent<Camera>().cullingMask = player1Cull;

            if (gameMode == eGameModes.timeTrial)   return;

            var p2Pos = GameObject.FindGameObjectWithTag("Player2_Manager").transform.position;
                var p2 = Instantiate(player2, p2Pos, Quaternion.identity);
                p2.tag = "Player2";
                p2.GetComponent<PlayerInput>().index = 2;
                p2.GetComponent<PlayerManager>().playerIndex = 2;
                p2.transform.Find("Main Camera").GetComponent<Camera>().cullingMask = player2Cull;
        }

        public int Modify
        {
            set => numberOfLaps += value;
        }
        

        public enum eGameModes
        {
            localMultiplayer,
            AIMultiplayer,
            timeTrial,
        }

        public eGameModes gameMode;
        public float[] winTimers;
        [SerializeField] private LayerMask player2Cull;
        [SerializeField] private LayerMask player1Cull;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            else
                Destroy(this.gameObject);




            m_numberOfLaps = 1;



        }

        private void Update()
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu 1"))
            {
                if(gameMode == eGameModes.timeTrial)
                    if (player1 != null && Input.GetKeyDown(KeyCode.JoystickButton7))
                    {
                        SceneManager.LoadScene(2);
                    }
                
                if(gameMode == eGameModes.localMultiplayer)
                    if (player1 != null && player2 != null && Input.GetKeyDown(KeyCode.JoystickButton7))
                    {
                        SceneManager.LoadScene(2);
                    }
            }
        }


        public void ChangeMode()
        {
            switch (gameMode)
            {
                case eGameModes.localMultiplayer:
                    {
                        foreach (var playerInput in GameObject.FindObjectsOfType<PlayerInput>())
                        {
                            playerInput.transform.Find("Main Camera").GetComponent<Camera>().rect =
                                new Rect(0.5f * (playerInput.index - 1), 0, 0.5f, 1);
                        }

                    }
                    break;
                case eGameModes.AIMultiplayer:

                    break;
                case eGameModes.timeTrial:
                    {
                        numberOfLaps = 1;
                        foreach (var playerInput in GameObject.FindObjectsOfType<PlayerInput>())
                        {
                            if (playerInput.index != 1)
                                playerInput.gameObject.GetComponent<PlayerMovement>().gameObject.SetActive(false);
                            else
                                playerInput.transform.Find("Main Camera").GetComponent<Camera>().rect =
                                    new Rect(0, 0, 1, 1);
                        }
                    }
                    break;
            }
        }
        
        
    }
    
}