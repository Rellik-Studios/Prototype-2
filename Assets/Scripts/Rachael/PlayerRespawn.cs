using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform m_carTransform;
    [SerializeField] private Text m_ReadySetGo;
    private Vector3 position;
    private Quaternion rotation;
    private bool finished;
    PlayerRespawn[] components;

    //for now there is the sceneManager
    [SerializeField] private GameObject m_subMenu;

    // Start is called before the first frame update
    void Start()
    {
        position = m_carTransform.position;
        rotation = m_carTransform.rotation;
        //starting the race
        StartCoroutine(StartProcess());

        components = GameObject.FindObjectsOfType<PlayerRespawn>();
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
        if(GetComponent<CarHealth>() !=null)
        {
            if(GetComponent<CarHealth>().GetHealth() <=0)
            {
                RespawnCar();
            }
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
        gameObject.GetComponent<BoxCollider>().enabled = false;
        if (gameObject.GetComponent<CarHealth>() !=null)
        {
            GetComponent<CarHealth>().ResetHealth();
        }
        //gameObject.GetComponent<PlayerMovement>().enabled = false;
        print("Respawning please hold");
    }

    public void FinishCar()
    {
        StartCoroutine(FinishProcedure());
    }
    public bool GetStatus()
    {
        return finished;
    }
    public bool CheckAllCars()
    {
        if(components ==null)
        {
            return false;
        }

        foreach (PlayerRespawn comp in components)
        {
            if (!comp.finished)
                return false;

        }
        return true;
    }
    private IEnumerator RespawningProcess(float waitTime)
    {
        //apply animation here

        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<PlayerInput>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        //gameObject.GetComponent<PlayerMovement>().enabled = true;
        print("Now you can move");


    }

    private IEnumerator StartProcess()
    {

        gameObject.GetComponent<PlayerInput>().enabled = false;
        //apply animation here
        
        print("3");
        m_ReadySetGo.text = "3";
        yield return new WaitForSeconds(1);
        print("2");
        m_ReadySetGo.text = "2";
        yield return new WaitForSeconds(1);
        print("1");
        m_ReadySetGo.text = "1";
        yield return new WaitForSeconds(1);
        print("GO");
        m_ReadySetGo.text = "GO!";
        gameObject.GetComponent<PlayerInput>().enabled = true;
        yield return new WaitForSeconds(1);
        m_ReadySetGo.gameObject.SetActive(false);
    }

    private IEnumerator FinishProcedure()
    {

        gameObject.GetComponent<PlayerInput>().enabled = false;
        //gameObject.GetComponent<PlayerRespawn>().enabled = false;
        //apply animation here

        m_ReadySetGo.gameObject.SetActive(true);
        m_ReadySetGo.text = "Finished!";
        yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
        finished = true;

        if (CheckAllCars())
            m_subMenu.SetActive(true);
        //for now there is a out scene thing

    }
}
