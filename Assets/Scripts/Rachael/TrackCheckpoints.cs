using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;
/// <summary>
/// Rachael Colaco
/// </summary>
public class TrackCheckpoints : MonoBehaviour
{
    //SINGLE
    private int m_nextIndexCheckpoint;
    private int m_laps;
    private List<SingleCheckpoint> m_checkpointList;
    [SerializeField] private GameObject warningText;
    //MULTIPLAYER
    [SerializeField] private List<Transform> carTransformList;
    private List<int> m_nextIndexCheckpointList;
    private int numberOfLaps;

    public int currentLap => m_laps;
    public int currentIndex => m_nextIndexCheckpoint;
    
    private void Awake()
    {
        Transform checkpointsTransform = transform;

        m_checkpointList = new List<SingleCheckpoint>();
        foreach (Transform singleCheckpointTransform in checkpointsTransform)
        {
            SingleCheckpoint singleCheckpoint = singleCheckpointTransform.GetComponent<SingleCheckpoint>();

            singleCheckpoint.carTransform = carTransformList[0];
            singleCheckpoint.SetTrackCheckpoints(this);
            singleCheckpoint.gameObject.SetActive(false);
            
            m_checkpointList.Add(singleCheckpoint);
        }

        numberOfLaps = gameSettings.Instance.numberOfLaps;

        //FOR MULTIPLAYER---------------------------------
        m_nextIndexCheckpointList = new List<int>();

        foreach(Transform carTransform in carTransformList)
        {
            m_nextIndexCheckpointList.Add(0);
        }
        //-------------------------------------------------
        for (int i = 0; i < 2; i++)
        {
            m_checkpointList[i].gameObject.SetActive(true);
        }
        
        
        m_nextIndexCheckpoint = 0;
    }

    //checks for each checkpoint and its index for one car
    public void PlayerThroughCheckpoint(SingleCheckpoint singleCheckpoint, PlayerRespawn _player)
    {
        //checks if the next checkpoint index is the same within the list of checkpoints
        if(m_checkpointList.IndexOf(singleCheckpoint) == m_nextIndexCheckpoint)
        {
            Debug.Log("Correct Checkpoint");
            if (warningText.activeSelf)
                warningText.SetActive(false);
            _player.SetRespawnPoint(m_checkpointList[m_nextIndexCheckpoint].transform);
            m_checkpointList[m_nextIndexCheckpoint].gameObject.SetActive(false);
            m_nextIndexCheckpoint = (m_nextIndexCheckpoint +1) % m_checkpointList.Count;


            //when the player goes to all the checkpoints
            if(m_nextIndexCheckpoint == transform.childCount - 1)
            {
                m_laps++;
                if (m_laps != numberOfLaps)

                    Debug.Log("lap " + m_laps.ToString() + "/3");

                else
                {
                    gameManager.Instance.playing = false;
                    _player.FinishCar();
                }
                    
   
            }
            m_checkpointList[(m_nextIndexCheckpoint+1) % m_checkpointList.Count].gameObject.SetActive(true);
        }
        else
        {
            warningText.SetActive(true);
            Debug.Log("Wrong Checkpoint");
        }
    }

    /*
    ////checks for each checkpoint and its index for multiple cars
    public void CarThroughCheckpoint(SingleCheckpoint singleCheckpoint, Transform carTransform)
    {
        int nextCheckpoint = m_nextIndexCheckpointList[carTransformList.IndexOf(carTransform)];

        //checks if the next checkpoint index is the same within the list of checkpoints
        if (m_checkpointList.IndexOf(singleCheckpoint) == nextCheckpoint)
        {
            Debug.Log("Correct Checkpoint");
          
            m_checkpointList[m_nextIndexCheckpointList[carTransformList.IndexOf(carTransform)]].gameObject.SetActive(false);
            m_nextIndexCheckpointList[carTransformList.IndexOf(carTransform)] = (nextCheckpoint + 1) % m_checkpointList.Count;

            //when the player goes to all the checkpoints
            if (m_nextIndexCheckpointList[carTransformList.IndexOf(carTransform)] == 0)
            {
                m_laps++;
                Debug.Log("lap " + m_laps.ToString());

            }
            m_checkpointList[(m_nextIndexCheckpointList[carTransformList.IndexOf(carTransform)]) % m_checkpointList.Count].gameObject.SetActive(true);
        }
        else
        {
            
            Debug.Log("Wrong Checkpoint");
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
