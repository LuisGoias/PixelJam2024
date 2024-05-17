using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    //Boss variables
    [SerializeField] private GameObject bossObstaclePrefab;
    [SerializeField] private bool bossSpawn = false;
    private float lastBossSpawnScore = 0;
    [SerializeField] private float bossPauseDuration = 2f;

    //Obstacle variables
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTime = 3f;
    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    private float timeUntilObstacleSpawn;
    [SerializeField] private float obstacleSpeed = 5f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;


    //Obstacle variables that are changing overtime
    private float _obstacleSpawnTime;
    private float _obstacleSpeed;

    //Survival
    private float timeAlive;


    private void Start()
    {
        GameManager.instance.onPlay.AddListener(ResetFactors);
    }

    private void Update()
    {
        if(GameManager.instance.isPlaying)
        {
            timeAlive += Time.deltaTime;

            if(GameManager.instance.currentScore - lastBossSpawnScore >= 20)
            {
                bossSpawn = true;
                timeUntilObstacleSpawn = 0f;
                StartCoroutine(SpawnBossWithBehavior());
                lastBossSpawnScore = GameManager.instance.currentScore;
            } else
            {
                CalculateFactors();
                SpawnLoop();
            }
        }
        
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if(timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0;
        }
    }

    private void Spawn()
    {
       GameObject obstacleSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

       GameObject spawnableObstacle = Instantiate(obstacleSpawn, transform.position, Quaternion.identity);

       Rigidbody2D obstacleRB = spawnableObstacle.GetComponent<Rigidbody2D>();
       obstacleRB.velocity = Vector2.left * _obstacleSpeed;
    }

    private IEnumerator SpawnBossWithBehavior()
    {
        GameObject spawnableObstacle = Instantiate(bossObstaclePrefab, transform.position, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnableObstacle.GetComponent<Rigidbody2D>();
        // Move a little bit to the left
        obstacleRB.position = Vector2.left * -6.5f;

        yield return new WaitForSeconds(bossPauseDuration);

        // Move left again after the pause
        obstacleRB.velocity = Vector2.left * (_obstacleSpeed * 2f);

        bossSpawn = false;
    }

    private void CalculateFactors()
    {
        _obstacleSpawnTime = obstacleSpawnTime * Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }
    private void ResetFactors()
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    }
}
