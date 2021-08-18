using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private int nextIndexCheckpoint;
    private int laps;
    private List<SingleCheckpoint> checkpointList;
    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointList = new List<SingleCheckpoint>();
        foreach (Transform singleCheckpointTransform in checkpointsTransform)
        {
            SingleCheckpoint singleCheckpoint = singleCheckpointTransform.GetComponent<SingleCheckpoint>();
            
            singleCheckpoint.SetTrackCheckpoints(this);

            checkpointList.Add(singleCheckpoint);
        }

        nextIndexCheckpoint = 0;
    }
    public void PlayerThroughCheckpoint(SingleCheckpoint singleCheckpoint)
    {
        if(checkpointList.IndexOf(singleCheckpoint) == nextIndexCheckpoint)
        {
            Debug.Log("Correct Checkpoint");
            nextIndexCheckpoint = (nextIndexCheckpoint +1) % checkpointList.Count;
            if(nextIndexCheckpoint == 0)
            {
                laps++;
            }
        }
        else
        {
            Debug.Log("Wrong Checkpoint");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
