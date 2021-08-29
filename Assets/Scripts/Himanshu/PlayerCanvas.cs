using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Himanshu
{
    public class PlayerCanvas : MonoBehaviour
    {
        [SerializeField] private TrackCheckpoints Checkpoints;

        public int position
        {
            set
            {
                
                transform.Find("Position").gameObject.GetComponent<TMP_Text>().text = value.ToString();
            }
        }

        IEnumerator Start()
        {
            var index = name.Contains("1") ? 1 : 2;

            if(GameObject.FindWithTag($"Player{index}") != null)
                GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag($"Player{index}").transform.Find("Main Camera").GetComponent<Camera>();
            else 
                gameObject.SetActive(false);
            if (gameSettings.Instance.numberOfLaps > 1)
                transform.Find("TotalLaps").GetComponent<TMP_Text>().text = $"/{gameSettings.Instance.numberOfLaps}";
            else
            {
                transform.Find("TotalLaps").gameObject.SetActive(false);
                transform.Find("Laps").gameObject.SetActive(false);
            }

            yield return new WaitForEndOfFrame();
            
            if (index == 1 && gameSettings.Instance.gameMode == gameSettings.eGameModes.timeTrial)
            {
            
            //     transform.Find("TimerBorder").GetComponent<RectTransform>().localPosition = new Vector3(-198.88f,
            //         transform.Find("TimerBorder").GetComponent<RectTransform>().localPosition.y,
            //         transform.Find("TimerBorder").GetComponent<RectTransform>().localPosition.z);
            //
            //     transform.Find("LapBorder").GetComponent<RectTransform>().localPosition = new Vector3(-195.88f,
            //         transform.Find("LapBorder").GetComponent<RectTransform>().localPosition.y,
            //         transform.Find("LapBorder").GetComponent<RectTransform>().localPosition.z);
            }


        }

        private void Update()
        {

            if (!gameManager.Instance.playing) return;

            var index = name.Contains("1") ? 1 : 2;
            
            var lapText = transform.Find("Laps").GetComponent<TMP_Text>();
            if ((Checkpoints.currentLap + 1).ToString() != lapText.text)
            {
                if(int.Parse(lapText.text) < gameSettings.Instance.numberOfLaps)
                    lapText.text = (Checkpoints.currentLap + 1).ToString();
                gameManager.Instance.ResetTimer(index);
            }
        
            if(lapText.text == gameSettings.Instance.numberOfLaps.ToString() && lapText.text != "1")
                transform.Find("FinalLap").GetComponent<Animator>().SetBool("Stuff", true);
            var gameTimer = name.Contains("1")? gameManager.Instance.gameTimer.ToString() : gameManager.Instance.gameTimerP2.ToString();
            var tmpText = transform.Find("Timer").GetComponent<TMP_Text>();
            tmpText.text = (gameTimer.Length > gameTimer.LastIndexOf('.') + 3) ? gameTimer.Remove(gameTimer.LastIndexOf('.') + 3) : gameTimer;

            if (gameSettings.Instance.gameMode == gameSettings.eGameModes.timeTrial)
            {
                TMP_Text target = transform.Find("Target").GetComponent<TMP_Text>();
                if (gameManager.Instance.gameTimer < gameSettings.Instance.winTimers[0])
                {
                    tmpText.color = new Color(1f, 0.84f, 0.0f);
                    target.text = gameSettings.Instance.winTimers[0].ToString();
                }
                else if (gameManager.Instance.gameTimer < gameSettings.Instance.winTimers[1])
                {
                    tmpText.color = Color.gray;
                    target.text = gameSettings.Instance.winTimers[1].ToString();
                }
                else if (gameManager.Instance.gameTimer < gameSettings.Instance.winTimers[2])
                {
                    tmpText.color = new Color(0.69f, 0.55f, 0.34f);
                    target.text = gameSettings.Instance.winTimers[2].ToString();
                }
                else
                {
                    tmpText.color = Color.white;
                    target.text = "";
                }

                target.color = tmpText.color;
            }
        }

    }
}
