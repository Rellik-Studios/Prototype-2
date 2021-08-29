using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    private Transform m_carTransform;
    [SerializeField] private Text m_ReadySetGo;
    [SerializeField] private Text m_Finished;
    private Vector3 position;
    private Quaternion rotation;
    private bool finished;
    PlayerRespawn[] cars;

    private GameObject m_player;

    [SerializeField] private int index;
    
    //for now there is the sceneManager
    [SerializeField] private GameObject m_subMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindWithTag($"Player{index}"))
            m_player = GameObject.FindGameObjectWithTag($"Player{index}");
        else
        {
            gameObject.SetActive(false);
            return;
        }
        m_carTransform = m_player.transform;
        position = m_carTransform.position;
        rotation = m_carTransform.rotation;
        GetComponent<PlayerPowerup>().playerPower = Powertypes.NONE;
        //starting the race
        StartCoroutine(StartProcess());

        cars = GameObject.FindObjectsOfType<PlayerRespawn>();
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
        //Checking if the CarHealth script is present
        if(GetComponent<CarHealth>() !=null)
        {
            //if health is 0 or negative then respawn
            if(GetComponent<CarHealth>().GetHealth() <=0)
            {
                m_player.GetComponent<BoxCollider>().enabled = false;
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
        GameObject explosion = Resources.Load<GameObject>("Exploding_Star");
        Instantiate(explosion, m_player.transform.position, m_player.transform.rotation);

        m_player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<PlayerPowerup>().playerPower = Powertypes.NONE;
        
        //respawning processing
        StartCoroutine(RespawningProcess(3.0f));
        m_player.GetComponent<PlayerInput>().enabled = false;

        if (gameObject.GetComponent<CarHealth>() != null)
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
    //checking if this car finished the race
    public bool GetStatus()
    {
        return finished;
    }
    //checking if all the cars have finished the race
    public bool CheckAllCars()
    {
        if(cars ==null)
        {
            return false;
        }

        foreach (PlayerRespawn car in cars)
        {
            if (car.gameObject.activeInHierarchy)
            {
                if (!car.finished)
                    return false;
            }

        }
        return true;
    }
    //respawn process
    private IEnumerator RespawningProcess(float waitTime)
    {

        //apply animation  for car explosion here
        GameObject explosion = Resources.Load<GameObject>("Exploding_Star");
        yield return new WaitForSeconds(explosion.GetComponent<ParticleSystem>().main.duration);

        m_player.transform.position = position;
        m_player.transform.rotation = rotation;
        if (!GetStatus())
        {
            m_player.GetComponent<PlayerInput>().enabled = true;
            m_player.GetComponent<BoxCollider>().enabled = true;
        }
        //gameObject.GetComponent<PlayerMovement>().enabled = true;
        print("Now you can move");


    }
    //starting race process
    private IEnumerator StartProcess()
    {

        m_player.GetComponent<PlayerInput>().enabled = false;
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
        m_player.GetComponent<PlayerInput>().enabled = true;
        yield return new WaitForSeconds(1);
        m_ReadySetGo.gameObject.SetActive(false);
    }
    //finish race process
    private IEnumerator FinishProcedure()
    {

        m_player.GetComponent<PlayerInput>().Reset();
        m_player.GetComponent<PlayerInput>().enabled = false;
        //gameObject.GetComponent<PlayerRespawn>().enabled = false;
        //apply animation here

        m_Finished.gameObject.SetActive(true);
        m_Finished.text = "Finished!";
        yield return new WaitForSeconds(3);

        //gameObject.SetActive(false);
        finished = true;

        if (CheckAllCars())
            m_subMenu.SetActive(true);
        //for now there is a out scene thing

    }
    



}
