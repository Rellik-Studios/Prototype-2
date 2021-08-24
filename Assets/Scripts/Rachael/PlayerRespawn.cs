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
    PlayerRespawn[] cars;
    private PlayerInput m_playerInput;
    public Powertypes playerpower;

    //for now there is the sceneManager
    [SerializeField] private GameObject m_subMenu;

    // Start is called before the first frame update
    void Start()
    {
        m_playerInput = GetComponent<PlayerInput>();
        position = m_carTransform.position;
        rotation = m_carTransform.rotation;
        playerpower = Powertypes.NONE;
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
                RespawnCar();
            }
        }
        //FOR TESTING PURPOSES ON POWER UP
        //MIGHT NEED TO ACTIVATE IN THE PLAYER INPUT
        if (playerpower != Powertypes.NONE)
        {
            if (m_playerInput.powerUp)
            {
                Debug.Log("player used" + playerpower.ToString());
                playerpower = Powertypes.NONE;

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
        playerpower = Powertypes.NONE;

        //respawning processing
        StartCoroutine(RespawningProcess(3.0f));
        gameObject.GetComponent<PlayerInput>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
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
            if (!car.finished)
                return false;

        }
        return true;
    }
    //respawn process
    private IEnumerator RespawningProcess(float waitTime)
    {
        //apply animation  for car explosion here

        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<PlayerInput>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        //gameObject.GetComponent<PlayerMovement>().enabled = true;
        print("Now you can move");


    }
    //starting race process
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
    //finish race process
    private IEnumerator FinishProcedure()
    {

        gameObject.GetComponent<PlayerInput>().Reset();
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


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            RespawnCar();
        }
    }
}
