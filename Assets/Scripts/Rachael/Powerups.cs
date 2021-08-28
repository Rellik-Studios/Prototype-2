using System.Collections;
using System.Collections.Generic;
using Himanshu;
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
    [SerializeField] private int defaultWaitTime = 3;

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
        if (other.TryGetComponent<PlayerManager>(out PlayerManager _player))
        {
            if(_player.playerPowerup.playerPower == Powertypes.NONE)
            {
                _player.playerPowerup.playerPower= RandomDraw();
                StartCoroutine(Dissapear());
                //Give them power up

            }
        }
    }

    private IEnumerator Dissapear()
    {
        transform.parent.GetComponent<Animator>().SetBool("dissappear", true);
        transform.parent.GetComponent<Animator>().SetBool("appear", false);

        yield return new WaitForSeconds(defaultWaitTime);

        transform.parent.GetComponent<Animator>().SetBool("dissappear", false);
        transform.parent.GetComponent<Animator>().SetBool("appear", true);
    }

    //randomly picking a power up
    Powertypes RandomDraw()
    {
        Powertypes power = (Powertypes)Random.Range(1, 4);
        return power;
    }
}
