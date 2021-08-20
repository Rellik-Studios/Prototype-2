using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform m_carTransform;
    private Vector3 position;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        position = m_carTransform.position;
        rotation = m_carTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("RESET");
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
        }
    }

    public void SetRespawnPoint(Transform checkpoint)
    {
        position = checkpoint.position;
        rotation = checkpoint.rotation;
    }

    public void RespawnCar()
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        //respawning processing
        StartCoroutine(RespawningProcess(3.0f));
        gameObject.GetComponent<PlayerInput>().enabled = false;
        //gameObject.GetComponent<PlayerMovement>().enabled = false;
        print("Respawning please hold");
    }

    private IEnumerator RespawningProcess(float waitTime)
    {
        //apply animation here

        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<PlayerInput>().enabled = true;
        //gameObject.GetComponent<PlayerMovement>().enabled = true;
        print("Now you can move");


    }
}
