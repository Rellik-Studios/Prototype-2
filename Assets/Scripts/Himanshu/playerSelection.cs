using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    [RequireComponent(typeof(PlayerInput))]
    public class playerSelection: MonoBehaviour
    {
        [SerializeField] private List<GameObject> cars;
        [SerializeField] private List<GameObject> placeHolderCars;
        private PlayerInput m_playerInput;
        [SerializeField] private List<CarData> CarDatas;
        [SerializeField] private List<GameObject> deets;
        
        private float m_movY = 3f;
        private float m_rotY = 0f;

        private float m_x = 0f;
        private int m_index;

        public int index
        {
            get => m_index;
            set
            {
                m_index = value >= 0 ? value < 3 ? value : value - 3 : 3 + value;
            }
        }

        private IEnumerator eMove(int position, int dir)
        {
            if (position == 0 && dir == -1)
            {
                cam.transform.position = new Vector3(cam.transform.position.x - 42f, cam.transform.position.y,
                    cam.transform.position.z);
            }

            if (position == 2 && dir == 1)
            {
                cam.transform.position = new Vector3(cam.transform.position.x + 42f, cam.transform.position.y,
                    cam.transform.position.z);
            }

            while (m_x < 14f)
            {
                m_moving = true;
                m_x += Time.deltaTime * 5f;
                cam.transform.position = new Vector3(cam.transform.position.x - Time.deltaTime * 5f * dir, cam.transform.position.y,
                    cam.transform.position.z);
                yield return null;
            }

            SetDeets();
            m_moving = false;
            m_x = 0f;
        }

        public bool active { get; set; }

        [SerializeField] private GameObject cam;
        private bool m_moving = false;
        private bool m_selected;

        public float movY
        {
            get => m_movY;
            set => m_movY = value > 3f ? value < 6f ? value : 6f : 3f;
        }
        public float rotY
        {
            get => m_rotY;
            set => m_rotY = value < 360f ? value > 0f ? value : 360f + value : value - 360f;
        }

        private void Start()
        {
            active = false;
            m_playerInput = GetComponent<PlayerInput>();
            SetDeets();
        }

        private void Update()
        {

            
            if (!m_moving && !m_selected)
            {
                int mul = m_playerInput.index == 1 ? 1 : -1;
                if (m_playerInput.horizontal * mul > 0f)
                {
                    StartCoroutine(eMove(index--, -1));
                }
                else if(m_playerInput.horizontal * mul < 0)
                {
                    StartCoroutine(eMove(index++, 1));
                }
            }
            if (!active) return;
            rotY += m_playerInput.rightStick;
            movY += m_playerInput.rightStickVert * Time.deltaTime * 10f;
            
            Debug.Log(rotY);
            foreach (var car in cars)
            {
                car.transform.localRotation = Quaternion.Euler(0f, rotY, 0f);
            }

            foreach (var car in placeHolderCars)
            {
                car.transform.localRotation = Quaternion.Euler(0f, rotY, 0f);
            }
            cam.transform.position =
                new Vector3(cam.transform.position.x, m_movY, cam.transform.position.z);


            Select();
            
        }

        private void Select()
        {
            if (m_playerInput.powershotBoost && !m_selected)
            {
                m_selected = true;
                if(m_playerInput.index == 1)
                    gameSettings.Instance.player1 = CarDatas[index].car;

                else
                    gameSettings.Instance.player2 = CarDatas[index].car;
            }

            if (m_playerInput.powerUp)
            {
                m_selected = false;
                if(m_playerInput.index == 1)
                    gameSettings.Instance.player1 = null;

                else
                    gameSettings.Instance.player2 = null;
            }
            
            
        }

        void SetData(Transform _transform, string _name, float _data)
        {
            if (_transform.Find(_name).gameObject.TryGetComponent<Slider>(out Slider _slider))
                _slider.value = _data;
            else
                Debug.Log("Component not found wtff?");
        }
        private void SetDeets()
        {
            
            foreach (var deet in deets)
            {
                //if(!deet.gameObject.activeInHierarchy) continue;
                var t = deet.transform;
                SetData(t, "SliderSpeed", CarDatas[index].speed);
                SetData(t, "SliderAccelaration", CarDatas[index].accelaration);
                SetData(t, "SliderHandling", CarDatas[index].handling);
                SetData(t, "SliderBoost", CarDatas[index].boostDur);
                SetData(t, "SliderHealth", CarDatas[index].health);

                t.Find("TextBoost").GetComponent<TMP_Text>().text = CarDatas[index].boostType;

            }
        }
    }
}