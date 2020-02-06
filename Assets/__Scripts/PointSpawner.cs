using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";

    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject enemyParent;

    private void Start()
    {
        // if on the top enable falling script, else enable EnemyBehaiour script
        // ...

        enemyParent = GameObject.Find("EnemyParent");

        if (!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }

        // get the spawn points
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnEnemyWaves();
    }

    private void SpawnEnemyWaves()
    {
        // create a stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);

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
    }

    // list version
    // private void SpawnOneEnemy()
    // {
    //     var enemy = Instantiate(enemyPrefab, enemyParent.transform);

    //     // set the enemy position to one of the spawn points
    //     // get a random number between 0 and list.count
    //     var rIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
    //     var sp = spawnPoints[rIndex];

    //     // set the new enemy position
    //     enemy.transform.position = sp.transform.position;
    // }
}
