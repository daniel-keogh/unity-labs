using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // == public fields ==
    public int StartingLives
    {
        get { return startingLives; }
    }

    public int RemainingLives
    {
        get { return remainingLives; }
    }

    //== private fields ==
    private int playerScore = 0;
    private int remainingLives = 0;

    // for the enemy waves
    [SerializeField] private int enemyCountPerWave = 20;
    [SerializeField] private TextMeshProUGUI remainingEnemyText;
    private int remainingEnemies;
    private int waveNumber = 1;
    // audio sound to indicate "between wave" moment
    [SerializeField] private AudioClip waveReadySound;
    private SoundController sc;

    [SerializeField] private int startingLives = 3;

    [SerializeField] private TextMeshProUGUI scoreText;

    // == private methods ==
    void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        remainingLives = startingLives;
        UpdateScore();

        remainingEnemies = enemyCountPerWave;
        sc = SoundController.FindSoundController();
    }

    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent += OnEnemySpawnedEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent -= OnEnemySpawnedEvent;
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add the score value for the enemy to the player score
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    private void OnEnemySpawnedEvent()
    {
        remainingEnemies--;
        UpdateEnemyRemainingText();

        if (remainingEnemies == 0)
        {
            DisableSpawning();
            // start the next wave using a coroutine
            StartCoroutine(SetupNextWave());
        }
    }

    private IEnumerator SetupNextWave()
    {
        yield return new WaitForSeconds(5.0f);

        sc.PlayOneShot(waveReadySound);
        waveNumber++; // not displayed
        remainingEnemies = enemyCountPerWave;
        EnableSpawning();
    }

    private void UpdateScore()
    {
        // display on screen
        scoreText.text = playerScore.ToString();
    }

    private void UpdateEnemyRemainingText()
    {
        remainingEnemyText.text = remainingEnemies.ToString();
    }

    public void DisableSpawning()
    {
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.DisableSpawning();
        }
    }

    public void EnableSpawning()
    {
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.EnableSpawning();
        }
    }

    // add a method to setup as a singleton
    private void SetupSingleton()
    {
        // this method gets called on creation
        // check for any other objects of the same type
        // if there is one, use that one
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject); // destroy the current object
        }
        else
        {
            // keep the new one
            DontDestroyOnLoad(gameObject); // persist across scenes
        }
    }

    public void LoseOneLife()
    {
        remainingLives--;
    }
}
