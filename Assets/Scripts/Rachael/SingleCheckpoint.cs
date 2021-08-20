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
    [HideInInspector] public Transform carTransform;
    private void OnTriggerEnter(Collider other)
    {
        //when the car enters a checkpoint
        if(other.TryGetComponent<PlayerRespawn>(out PlayerRespawn _player))
        {
            
            //calls the function to indicate this checkpoint has been through.
            if (other.transform == carTransform)
            {
                m_trackCheckpoints.PlayerThroughCheckpoint(this);

                _player.SetRespawnPoint(this.transform);
            }
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
