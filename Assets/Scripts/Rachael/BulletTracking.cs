using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracking : MonoBehaviour
{
    public float bulletSpeed = 1.0f;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            Vector3 direction = target.transform.position - transform.position;
            GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //checking when the bullet hits a enemy
        if (other.GetComponent<CarHealth>() !=null && other.name == target.name)
        {
            Debug.Log("Bullet hit");
            other.GetComponent<CarHealth>().DamageFromPowerUp();
            Destroy(gameObject);

        }
    }
}
