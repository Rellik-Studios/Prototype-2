using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Himanshu
{
    public class PlayerInput : MonoBehaviour
    {
        private float m_throttle;
        private float m_movement;
        
        [SerializeField] private int playerIndex;

        //Returns Horizontal if left stick is moved, zero if not
        public float horizontal => Mathf.Abs(m_movement) > 0.1f ? m_movement : 0;
        
        //returns throttle +ve if r2 is pressed
        public float throttle => m_throttle > 0 ? m_throttle : 0;
        
        //returns brake +ve if l2 is pressed
        public float brake => m_throttle < 0 ? -m_throttle : 0;


        private void Start()
        {
            if (playerIndex == 0)
            {
                throw new Exception($"{name} is set to playerIndex 0");
            }
        }

        void Update()
        {
            m_throttle = Input.GetAxis($"Throttle_P{playerIndex}");
            //Debug.Log($"Throttle_P{playerIndex}");

            m_movement = Input.GetAxis($"Horizontal_P{playerIndex}");
        }
    }
}
