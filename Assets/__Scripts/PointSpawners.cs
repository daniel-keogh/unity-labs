using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

//        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);

public class PointSpawners : MonoBehaviour
{
    // == private fields ==

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";

    private IList<SpawnPoint> spawnPoints;

    private Stack<SpawnPoint> spawnStack;

    private GameObject enemyParent;

    // event for telling the system enemy spawned
    public delegate void EnemySpawned();
    public static event EnemySpawned EnemySpawnedEvent;

    //private ListUtils listUtils = new ListUtils();

    // == private methods ==
    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if (!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }
        // get the spawn points here
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnEnemyWaves();

        EnableSpawning();
    }

    private void SpawnEnemyWaves()
    {
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        //InvokeRepeating("SpawnOneEnemy", 0f, 0.25f);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    // stack version
    private void SpawnOneEnemy()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;

        PublishOnEnemySpawnedEvent();
    }

    //// use InvokeRepeating to spawn enemies
    //private void SpawnOneEnemy()
    //{  // list version
    //    var enemy = Instantiate(enemyPrefab, enemyParent.transform);
    //    // set the enemy position to one of the spawn points
    //    // get a random number between 0 and list.count
    //    var rIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
    //    var sp = spawnPoints[rIndex];   // actual element on list
    //    // set the new enemy position
    //    enemy.transform.position = sp.transform.position;
    //}

    public void PublishOnEnemySpawnedEvent()
    {
        EnemySpawnedEvent?.Invoke();
    }

    public void EnableSpawning()
    {
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    public void DisableSpawning()
    {
        CancelInvoke(SPAWN_ENEMY_METHOD);
    }
}
