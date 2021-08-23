using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Himanshu
{
    public class PlayerInput : MonoBehaviour
    {
        private float m_throttle;
        private float m_brake;
        private float m_movement;
        private bool m_handBrake;
        private bool m_boost;
        
        
        [SerializeField] private int playerIndex;

        public int index => playerIndex;
        
        //Returns Horizontal if left stick is moved, zero if not
        public float horizontal => Mathf.Abs(m_movement) > 0.1f ? m_movement : 0;
        
        //Returns true if HandBrake down that frame
        public bool handBrake => m_handBrake;

        
        public bool boost => m_boost;

        //returns throttle +ve if r2 is pressed
        public float throttle => m_throttle > 0 ? m_throttle : 0;
        
        //returns brake +ve if l2 is pressed
        public float brake => m_brake > 0 ? m_brake : 0;



        private void Start()
        {
            if (playerIndex == 0)
            {
                throw new Exception($"{name} is set to playerIndex 0");
                Application.Quit();
            }
        }

        void Update()
        {
            m_throttle = Input.GetAxis($"Throttle_P{playerIndex}");
            m_brake = Input.GetAxis($"Brake_P{playerIndex}");
            m_movement = Input.GetAxis($"Horizontal_P{playerIndex}");
            m_handBrake = Input.GetButtonDown($"HandBrake_P{playerIndex}");
            m_boost = Input.GetButton($"Boost_P{playerIndex}");
        }
        private void OnDisable()
        {
            m_throttle = 0;
            m_brake = 0;
            m_movement = 0;
            m_handBrake = false;
        }

    }
}
