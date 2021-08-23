using System;
using UnityEngine;

namespace Himanshu
{
    public class gameManager : MonoBehaviour
    {
        private static gameManager m_instance;

        public static gameManager Instance
        {
            get => m_instance;
            private set => m_instance = value;
        }


        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            else
                Destroy(this.gameObject);
        }
        
        
    }
}