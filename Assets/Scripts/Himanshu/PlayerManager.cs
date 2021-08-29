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

        public int health;
        public int playerIndex
        {
            get => index;
            set => index = value;
        }

        private void Start()
        {
            m_manager = GameObject.FindGameObjectWithTag($"Player{index}_Manager");
            transform.position = m_manager.transform.position;
            transform.rotation = m_manager.transform.rotation;
            carHealth = m_manager.GetComponent<CarHealth>();
            playerPowerup = m_manager.GetComponent<PlayerPowerup>();
            playerRespawn = m_manager.GetComponent<PlayerRespawn>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("DeadZone"))
            {
                playerRespawn.RespawnCar();
            }
        }


    }
}