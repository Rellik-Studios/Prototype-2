using System.Linq;
using UnityEngine;

namespace Himanshu
{
    public class gameSettings: MonoBehaviour
    {
        private static gameSettings m_instance;
        public static gameSettings Instance => m_instance;

        public enum eGameModes
        {
            localMultiplayer,
            networkMultiplayer,
            timeTrial,
        }

        public eGameModes gameMode; 

        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;

            else
                Destroy(this.gameObject);

            switch (gameMode)
            {
                case eGameModes.localMultiplayer:
                {
                    foreach (var playerInput in GameObject.FindObjectsOfType<PlayerInput>())
                    {
                        playerInput.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    }
                    
                } break;
                case eGameModes.networkMultiplayer:
                    
                    break;
                case eGameModes.timeTrial:
                    
                    break;
            }

        }
        
        
    }
}