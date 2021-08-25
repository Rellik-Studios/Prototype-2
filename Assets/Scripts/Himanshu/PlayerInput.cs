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
        private bool m_defaultBoost;
        
        
        [SerializeField] private int playerIndex;
        private bool m_powerUp;
        private bool m_powershotBoost;
        public bool powerUp => m_powerUp;

        public int index => playerIndex;
        
        //Returns Horizontal if left stick is moved, zero if not
        public float horizontal => Mathf.Abs(m_movement) > 0.1f ? m_movement : 0;
        
        //Returns true if HandBrake down that frame
        public bool handBrake => m_handBrake;

        
        public bool defaultBoost => m_defaultBoost;
        public bool powershotBoost => m_powershotBoost;

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
            m_defaultBoost = Input.GetButton($"Boost_P{playerIndex}");
            m_powershotBoost = Input.GetButtonDown($"Boost_P{playerIndex}");
            m_powerUp = Input.GetButtonDown($"PowerUp_P{playerIndex}");
        }
        private void OnDisable()
        {
            Reset();
        }

        public void Reset()
        {
            m_throttle = 0;
            m_brake = 0;
            m_movement = 0;
            m_handBrake = false;
        }
    }
    
}
