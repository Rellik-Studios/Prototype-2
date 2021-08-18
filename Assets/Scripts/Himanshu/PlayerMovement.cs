using System;
using UnityEngine;

namespace Himanshu
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        private Rigidbody m_rigidBody;
        [SerializeField] private float m_accelaration;
        [SerializeField] private float m_topSpeed;
        [SerializeField] private float m_turnMultiplier;
        
        private void Start()
        {
            if (TryGetComponent(out Rigidbody _rigidBody))
            {
                m_rigidBody = _rigidBody;
            }
            else
            {
                throw new Exception($"{name} does not have a rigid body component");
            }
            
            m_playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            var throttle = m_playerInput.throttle;
           // Debug.Log(throttle);
            if (throttle > 0)
            {
                if (m_rigidBody.velocity.magnitude < m_topSpeed)
                {
                    m_rigidBody.AddForce(transform.forward * (m_accelaration * throttle), ForceMode.Acceleration);
                }
            }

            if (Mathf.Abs(m_playerInput.horizontal) > 0)
            {
                var eulerAngles = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + m_playerInput.horizontal * m_turnMultiplier * m_rigidBody.velocity.magnitude / m_topSpeed, eulerAngles.z);
            }
        }
    }
}