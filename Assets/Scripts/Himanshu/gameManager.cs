using System;
using UnityEngine;

namespace Himanshu
{
    public class gameManager : MonoBehaviour
    {
        private static gameManager m_instance;

        public static gameManager Instance
        {
            get;
            private set;
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