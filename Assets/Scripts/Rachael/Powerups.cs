using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Powertypes
{
    NONE,
    FAST,
    HEALTH,
    BULLET

};
public class Powerups : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //when the player collides with the powerup box
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerPowerup>(out PlayerPowerup _player))
        {
            if(_player.playerpower == Powertypes.NONE)
            {
                _player.playerpower= RandomDraw();
                //Give them power up

            }
        }
    }
    //randomly picking a power up
    Powertypes RandomDraw()
    {
        
        Powertypes power = (Powertypes)Random.Range(1, 3);
        return power;
    }
}
