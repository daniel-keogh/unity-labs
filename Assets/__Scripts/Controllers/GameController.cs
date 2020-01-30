using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int playerScore = 0;

    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // Add the score value for the enemy to the player score
        playerScore += enemy.ScoreValue;

        UpdateScore();
    }

    private void UpdateScore()
    {
        Debug.Log("The score is: "+ playerScore);
    }
}
