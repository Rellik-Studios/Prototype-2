using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

/// <summary>
/// Rachael Colaco
/// </summary>
public class SingleCheckpoint : MonoBehaviour
{
    private TrackCheckpoints m_trackCheckpoints;
    private void OnTriggerEnter(Collider other)
    {
        //when the car enters a checkpoint
        if(other.TryGetComponent<PlayerInput>(out PlayerInput _playerInput))
        {
            //calls the function to indicate this checkpoint has been through.
            m_trackCheckpoints.PlayerThroughCheckpoint(this);

            _playerInput.SetRespawnPoint(this.transform);
            //calls function to all cars
            //m_trackCheckpoints.CarThroughCheckpoint(this, other.transform);
        }
    }
    
    

    //calls the manager into this script
    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.m_trackCheckpoints = trackCheckpoints;
    }
}
