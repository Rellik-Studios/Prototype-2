using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Himanshu
{
 
    public class PositionChecker : MonoBehaviour
    {
        [SerializeField] private TrackCheckpoints player1;
        [SerializeField] private TrackCheckpoints player2;

        [SerializeField] private PlayerCanvas playerCanvas1;
        [SerializeField] private PlayerCanvas playerCanvas2;
        
        [SerializeField] private Transform playerTransform1;
        [SerializeField] private Transform playerTransform2;


        private void Update()
        {
            var winningPosition = 0;

            if (player1.currentLap != player2.currentLap && player1.currentLap < player2.currentLap)
                winningPosition = 2;
            else if (player1.currentLap != player2.currentLap)
                winningPosition = 1;
            else
            {
                if (player1.currentIndex != player2.currentIndex && player1.currentIndex < player2.currentIndex)
                    winningPosition = 2;
                else if (player1.currentIndex != player2.currentIndex)
                    winningPosition = 1;
                else
                {
                    Vector3 direction = playerTransform2.position - playerTransform1.position;
                    if (Vector3.Dot(playerTransform1.forward, direction) > 0)
                        winningPosition = 2;
                    else
                        winningPosition = 1;
                }
            }
            
            SetPosition(winningPosition);
        }

        //Takes the index of the player at number 1
        private void SetPosition(int index)
        {
            playerCanvas1.position = index;
            playerCanvas2.position = index == 1 ? 2 : 1;
        }
    }
}