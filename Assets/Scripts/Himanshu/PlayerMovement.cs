using System;
using UnityEngine;

namespace Himanshu
{
    //The class handles calculating a desired value based on a PID algorithm
//This code is not specific to this game and is instead how PID algorithms work
//in electronics, robotics, and controlling software

    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {

        public float speed;

        [Header("Drive")] 
        public float driveForce = 17f;
        public float slowingVelFactor = .99f;
        public float brakingVelFactor = .95f;
        public float handBrakeVelFactor = .80f;
        public float angleOfRoll = 30f;

        [Header("Hover")] 
        public float hoverHeight = 1.5f;
        public float maxGroundDistance = 5f;
        public float hoverForce = 300f;
        public LayerMask ground;
        public PIDController hoverPID;

        [Header("Physics")] 
        public Transform vehicleBody;
        public float terminalVelocity = 100f;
        public float hoverGravity = 20f;
        public float fallGravity = 80f;
        
        private PlayerInput m_playerInput;
        private Rigidbody m_rigidBody;
        private float drag = 1f;
        private bool isOnGround;
        
        private void Start()
        {

            if (vehicleBody == null)
            {
                throw new Exception($"{name} does not have a vehicle Body object");
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
            var throttle = m_playerInput.throttle;
        }

        private void CalculateHover()
        {
            Vector3 groundNormal;
            Ray ray = new Ray(transform.position, -transform.up);

            RaycastHit hitInfo;

            isOnGround = Physics.Raycast(ray, out hitInfo, maxGroundDistance, ground);
            //var onGround = Physics.Raycast(ray, out hitInfo, 0f, ground);

            if (isOnGround)
            {
                float height = hitInfo.distance;
                groundNormal = hitInfo.normal.normalized;
                float forcePercent = hoverPID.Seek(hoverHeight, height);

                Vector3 force = groundNormal * hoverForce * forcePercent;
                Vector3 gravity = -groundNormal * hoverGravity * hoverHeight;
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
                groundNormal = Vector3.up;
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
            m_rigidBody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);

            float sidewaysSpeed = Vector3.Dot(m_rigidBody.velocity, transform.right);

            Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime);

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
    }
}