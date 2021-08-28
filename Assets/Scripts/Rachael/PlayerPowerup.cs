using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Himanshu;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerPowerup : MonoBehaviour
{
    private Dictionary<Powertypes, Sprite> m_powerDictionary = new Dictionary<Powertypes, Sprite>();
    PlayerRespawn[] cars;
    private PlayerInput m_playerInput;
    private Powertypes m_playerPower;
    public Image PowerIcon;
    private GameObject m_player;
    [SerializeField] private int index;
    public Powertypes playerPower
    {
        get => m_playerPower;
        set
        {
            m_playerPower = value;
            if(m_powerDictionary.TryGetValue(value, out Sprite powerImage))
                PowerIcon.sprite = powerImage;
        }
    }

    public GameObject ammo;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag($"Player{index}");
        var powerups = Resources.LoadAll<PowerUpImage>("Sprites");

        foreach (var powerup in powerups)
        {
            m_powerDictionary.Add(powerup.powerType, powerup.powerUpImage);
        }
        m_playerInput = m_player.GetComponent<PlayerInput>();
        playerPower = Powertypes.FAST;
        cars = GameObject.FindObjectsOfType<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPower != Powertypes.NONE)
        {
            if (m_playerInput.powerUp)
            {
                Debug.Log("player used" + playerPower.ToString());
                UsePowerup(playerPower);
                playerPower = Powertypes.NONE;
                if (PowerIcon != null)
                    PowerIcon.sprite = null;
                
                


            }
        }
    }

    //checking on which power up and enabling it
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
    //appling their health
    private void UseHealth()
    {
        GetComponent<CarHealth>().ReplenishHealth();
    }
    //applying speed
    private void UseSpeed()
    {
        m_player.GetComponent<PlayerMovement>().PowerUpBoost(100.0f);
        
    }
    //spawning the bullet
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
