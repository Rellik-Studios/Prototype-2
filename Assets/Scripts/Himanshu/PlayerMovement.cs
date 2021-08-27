using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Himanshu
{

    public class PlayerMovement : MonoBehaviour
    {

        public float speed;

        enum eBoostType
        {
            PowerShot,
            Default,
            PS_Speed,
        }

        private bool m_canBoost;
        
        [Header("Drive")] 
        [SerializeField] private float driveForce = 17f;
        [SerializeField] private float boostFactor = 1.5f;
        [SerializeField] private float boostRegenFactor = 0.5f;
        [SerializeField] private float boostTimer = 3f;
        [SerializeField] private float slowingVelFactor = .99f;
        [SerializeField] private float brakingVelFactor = .95f;
        [SerializeField] private float handBrakeVelFactor = .80f;
        [SerializeField] private float angleOfRoll = 30f;
        [SerializeField] private eBoostType boostType;
        [SerializeField] private Image boostImage;
        
        
        [Header("Hover")] 
        [SerializeField] private float hoverHeight = 1.5f;
        [SerializeField] private float maxGroundDistance = 5f;
        [SerializeField] private float hoverForce = 300f;
        [SerializeField] private LayerMask ground;
        [SerializeField] private LayerMask groundAbove;
        [SerializeField] private PIDController hoverPID;
        [SerializeField] private bool reverse;
        
        [Header("Physics")] 
        [SerializeField] private Transform vehicleBody;
        [SerializeField] private float terminalVelocity = 100f;
        [SerializeField] private float hoverGravity = 20f;
        [SerializeField] private float fallGravity = 80f;
        
        private PlayerInput m_playerInput;
        private Rigidbody m_rigidBody;
        private float drag = 1f;
        private bool isOnGround;
        private float m_maxBoostTimer;
        private void boostSetter()
        {

            boostImage.fillAmount = boostTimer / m_maxBoostTimer;
        }

        private void Start()
        {
            m_maxBoostTimer = boostTimer;

            if (vehicleBody == null)
            {
                throw new Exception($"{name} does not have a vehicle Body object");
            }

            if (boostImage == null)
            {
                throw new Exception($"{name} does not have a boostImage Image");

            }
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

        private void FixedUpdate()
        {
            speed = Vector3.Dot(m_rigidBody.velocity, transform.forward);
            CalculateHover();
            CalculatePropulsion();
            CalculateBoost();
        }

        private void CalculateBoost()
        {
            switch (boostType)
            {
                case eBoostType.Default:
                {
                    if (m_playerInput.defaultBoost && m_canBoost && boostTimer > 0f)
                    {
                        boostTimer -= Time.deltaTime;
                        
                        float propulsion = boostFactor * m_playerInput.throttle;
                        //Debug.Log(m_playerInput.throttle);
                        m_rigidBody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);
                    }
                    else
                    {
                        if(boostTimer < m_maxBoostTimer)
                            boostTimer += Time.deltaTime * boostRegenFactor;

                        m_canBoost = boostTimer > 1f && !m_playerInput.defaultBoost;
                    }                    
                }
                    break;

                case eBoostType.PowerShot:
                {
                    if (!m_canBoost)
                        m_canBoost = m_playerInput.powershotBoost && Math.Abs(m_maxBoostTimer - boostTimer) < 0.1f;
                    
                    if (m_canBoost && boostTimer > 0f)
                    {
                        boostTimer -= Time.deltaTime;
                        float propulsion = boostFactor;
                        m_rigidBody.AddForce(transform.forward * propulsion, ForceMode.Impulse);
                    }
                    else
                    {
                        m_canBoost = false;
                        if(boostTimer < m_maxBoostTimer)
                            boostTimer += Time.deltaTime * boostRegenFactor;
                    }
                }
                    break;

                case eBoostType.PS_Speed:
                {
                    if (!m_canBoost)
                        m_canBoost = m_playerInput.powershotBoost && Math.Abs(m_maxBoostTimer - boostTimer) < 0.1f;

                    if (m_canBoost && boostTimer > 0f)
                    {
                        boostTimer -= Time.deltaTime;
                        float propulsion = boostFactor;
                        m_rigidBody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);
                    }
                    else
                    {
                        m_canBoost = false;
                        if (boostTimer < m_maxBoostTimer)
                            boostTimer += Time.deltaTime * boostRegenFactor;
                    }
                }
                    break;
            }
            
            boostSetter();
            

        }


        //Pass in any force value
        public void PowerUpBoost(float _force)
        {
            m_rigidBody.AddForce(transform.forward * _force, ForceMode.Impulse);
            
            //Apply some sick post processing here
        }

        private void CalculateHover()
        {
            Vector3 groundNormal;
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hitInfo;

            bool wasOnGround = isOnGround;
            isOnGround = Physics.Raycast(ray, out hitInfo, maxGroundDistance, (reverse?groundAbove:ground));
            
            //var onGround = Physics.Raycast(ray, out hitInfo, 0f, ground);

            if (isOnGround)
            {
                if (!wasOnGround)
                    m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0f, m_rigidBody.velocity.z);
                float height = hitInfo.distance;
                groundNormal = hitInfo.normal.normalized;
                float forcePercent = hoverPID.Seek(hoverHeight, height);

                Vector3 force =  groundNormal * hoverForce * forcePercent;
                Vector3 gravity =  -groundNormal * hoverGravity * hoverHeight;
                m_rigidBody.AddForce(force, ForceMode.Acceleration);
                m_rigidBody.AddForce(gravity, ForceMode.Acceleration);
            }
            
            // else if (onGround)
            // {
            //     float height = hitInfo.distance;
            //     groundNormal = hitInfo.normal.normalized;
            //     float forcePercent = hoverPID.Seek(hoverHeight, height);
            //
            //     Vector3 force = groundNormal * hoverForce * forcePercent;
            //     Vector3 gravity = -groundNormal * hoverGravity * hoverHeight;
            //     m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0f, m_rigidBody.velocity.y);
            //     m_rigidBody.AddForce(force, ForceMode.Acceleration);
            // }

            else
            {

                groundNormal = reverse ? -Vector3.up : Vector3.up;
                Vector3 gravity = -groundNormal * fallGravity;
                m_rigidBody.AddForce(gravity, ForceMode.Acceleration);
            }

            Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
            Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);
            
            m_rigidBody.MoveRotation(Quaternion.Lerp(m_rigidBody.rotation,rotation,Time.deltaTime * 10f));

            float angle = angleOfRoll * -m_playerInput.horizontal;

            Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
            
            vehicleBody.rotation = Quaternion.Lerp(vehicleBody.rotation, bodyRotation, Time.deltaTime * 10f);
        }

        private void CalculatePropulsion()
        {
            float rotationTorque = m_playerInput.horizontal - m_rigidBody.angularVelocity.y;
            rotationTorque *= reverse ? -1f : 1f;
            
            m_rigidBody.AddTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);
            
            if (m_playerInput.index == 1)
            {
  //              Debug.Log(rotationTorque);
//                Debug.Log(m_playerInput.horizontal);
            }

            float sidewaysSpeed = Vector3.Dot(m_rigidBody.velocity, reverse? -transform.right: transform.right);

            Vector3 sideFriction = (reverse ? -1 : 1) * -transform.right * (sidewaysSpeed / Time.fixedDeltaTime);

            m_rigidBody.AddForce(sideFriction, ForceMode.Acceleration);

            if (m_playerInput.throttle == 0f)
            {
                m_rigidBody.velocity *= slowingVelFactor;
            }

            if (!isOnGround) return;

            if(m_playerInput.handBrake)
            {   
                m_rigidBody.velocity *= handBrakeVelFactor;
                if (m_playerInput.horizontal != 0)
                {
                    
                }
            }

            else if (m_playerInput.brake > 0f)
            {
                if(speed > 1f)
                    m_rigidBody.velocity *= brakingVelFactor;
                else
                {
                    float propulsion = driveForce / 2  * m_playerInput.brake;
                    //Debug.Log(m_playerInput.throttle);
                    if(speed < terminalVelocity / 2)
                        m_rigidBody.AddForce(transform.forward * -propulsion, ForceMode.Acceleration);
                }
            }

            else
            {
                float propulsion = driveForce * m_playerInput.throttle;
                //Debug.Log(m_playerInput.throttle);
                if(speed < terminalVelocity)
                    m_rigidBody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);

            }
        }

        private void LateUpdate()
        {
            if (reverse)
            {
                if (m_rigidBody.angularVelocity.y < -1)
                    m_rigidBody.angularVelocity = new Vector3(0f, -0.999f, 0f);
                
                else if (m_rigidBody.angularVelocity.y > 1)
                    m_rigidBody.angularVelocity = new Vector3(0f, 0.999f, 0f);
            }
            
            if (m_playerInput.horizontal == 0)
            {
                m_rigidBody.angularVelocity = Vector3.zero;
            }
        }
    }
}