using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    //When player collides to the Death area 
    private void OnTriggerEnter(Collider other)
    {
        //when the car enters a checkpoint
        if (other.TryGetComponent<PlayerRespawn>(out PlayerRespawn _player))
        {
            _player.RespawnCar();
        }
    }
}
