using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rachael Colaco
/// </summary>
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
            singleCheckpoint.gameObject.SetActive(false);
            
            checkpointList.Add(singleCheckpoint);
        }

        for (int i = 0; i < 1; i++)
        {
            checkpointList[i].gameObject.SetActive(true);
        }
        
        nextIndexCheckpoint = 0;
    }

    //checks for each checkpoint and its index
    public void PlayerThroughCheckpoint(SingleCheckpoint singleCheckpoint)
    {
        //checks if the next checkpoint index is the same within the list of checkpoints
        if(checkpointList.IndexOf(singleCheckpoint) == nextIndexCheckpoint)
        {
            Debug.Log("Correct Checkpoint");
            checkpointList[nextIndexCheckpoint].gameObject.SetActive(false);
            nextIndexCheckpoint = (nextIndexCheckpoint +1) % checkpointList.Count;

            //when the player goes to all the checkpoints
            if(nextIndexCheckpoint == 0)
            {
                laps++;
                Debug.Log("lap "+ laps.ToString());

            }
            checkpointList[(nextIndexCheckpoint) % checkpointList.Count].gameObject.SetActive(true);
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
