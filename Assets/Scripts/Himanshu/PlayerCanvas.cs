using System;
using TMPro;
using UnityEngine;

namespace Himanshu
{
    public class PlayerCanvas : MonoBehaviour
    {
        [SerializeField] private TrackCheckpoints Checkpoints;

        public int position
        {
            set => transform.Find("Position").gameObject.GetComponent<TMP_Text>().text = value.ToString();
        }
        void Start()
        {
            if (gameSettings.Instance.numberOfLaps > 1)
                transform.Find("TotalLaps").GetComponent<TMP_Text>().text = $"/{gameSettings.Instance.numberOfLaps}";
            else
            {
                transform.Find("TotalLaps").gameObject.SetActive(false);
                transform.Find("Laps").gameObject.SetActive(false);
            }
        }

        private void Update()
        {

            if (!gameManager.Instance.playing) return;

            
            var lapText = transform.Find("Laps").GetComponent<TMP_Text>();
            if ((Checkpoints.currentLap + 1).ToString() != lapText.text)
            {
                if(int.Parse(lapText.text) < gameSettings.Instance.numberOfLaps)
                    lapText.text = (Checkpoints.currentLap + 1).ToString();
                gameManager.Instance.ResetTimer();
            }
        
            if(lapText.text == gameSettings.Instance.numberOfLaps.ToString() && lapText.text != "1")
                transform.Find("FinalLap").GetComponent<Animator>().SetBool("Stuff", true);
            var gameTimer = gameManager.Instance.gameTimer.ToString();
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
