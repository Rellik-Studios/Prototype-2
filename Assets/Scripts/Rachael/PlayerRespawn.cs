using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
