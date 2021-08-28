using System;
using UnityEngine;

namespace Himanshu
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerRespawn playerRespawn { get; private set; }        
        public PlayerPowerup playerPowerup { get; private set; }        
        public CarHealth  carHealth { get; private set; }

        [SerializeField] private int index;
        private GameObject m_manager;

        private void Awake()
        {          
            m_manager = GameObject.FindGameObjectWithTag($"Player{index}_Manager");
            carHealth = m_manager.GetComponent<CarHealth>();
            playerPowerup = m_manager.GetComponent<PlayerPowerup>();
            playerRespawn = m_manager.GetComponent<PlayerRespawn>();
        }


    }
}