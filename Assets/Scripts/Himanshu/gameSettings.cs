using UnityEngine;

namespace Himanshu
{
    public class gameSettings: MonoBehaviour
    {
        private static gameSettings m_instance;
        public static gameSettings Instance => m_instance;

        enum eGameModes
        {
            localMultiplayer,
            networkMultiplayer,
            
        }

        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;

            else
                Destroy(this.gameObject);
        }
        
        
    }
}