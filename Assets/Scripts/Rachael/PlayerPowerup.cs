using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.UI;

public class PlayerPowerup : MonoBehaviour
{
    PlayerRespawn[] cars;
    private PlayerInput m_playerInput;
    public Powertypes playerpower;
    public GameObject ammo;
    public Image PowerIcon;
    // Start is called before the first frame update
    void Start()
    {
        m_playerInput = GetComponent<PlayerInput>();
        playerpower = Powertypes.NONE;
        cars = GameObject.FindObjectsOfType<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerpower != Powertypes.NONE)
        {
            if (m_playerInput.powerUp)
            {
                Debug.Log("player used" + playerpower.ToString());
                UsePowerup(playerpower);
                playerpower = Powertypes.NONE;
                if (PowerIcon != null)
                    PowerIcon.sprite = null;



            }
        }
    }


    private void UsePowerup(Powertypes _powertype)
    {
        switch (_powertype)
        {
            case Powertypes.FAST:
                UseSpeed();
                break;
            case Powertypes.BULLET:
                UseBullet();
                break;
            case Powertypes.HEALTH:
                UseHealth();
                break;
            default:
                print("None");
                break;
        }

    }
    private void UseHealth()
    {
        GetComponent<CarHealth>().ReplenishHealth();
    }
    private void UseSpeed()
    {
        //Use speed booster
    }
    private void UseBullet()
    {
        //just for testing purposes
        foreach (var car in cars)
        {
            if (car.gameObject.name != gameObject.name)
            {
                //car.gameObject.GetComponent<CarHealth>().DamageFromPowerUp();
                GameObject bulletClone = (GameObject)Instantiate(ammo, gameObject.transform.position, gameObject.transform.rotation);
                bulletClone.GetComponent<BulletTracking>().target = car.gameObject;
                break;
            }

        }

    }
}
