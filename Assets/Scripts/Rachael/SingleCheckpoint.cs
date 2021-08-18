using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

/// <summary>
/// Rachael Colaco
/// </summary>
public class SingleCheckpoint : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private void OnTriggerEnter(Collider other)
    {
        //when the car enters a checkpoint
        if(other.TryGetComponent<PlayerInput>(out PlayerInput _playerInput))
        {
            //calls the function to indicate this checkpoint has been through.
            trackCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    //calls the manager into this script
    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
}
