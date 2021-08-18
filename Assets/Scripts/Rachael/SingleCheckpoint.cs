using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
public class SingleCheckpoint : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerInput>(out PlayerInput _playerInput))
        {
            trackCheckpoints.PlayerThroughCheckpoint(this);
        }
    }
    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
}
