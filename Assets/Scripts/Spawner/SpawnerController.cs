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
    private float timeUntilObstacleSpawn;
    [SerializeField] private float obstacleSpeed = 1f;

    private void Update()
    {
        if(GameManager.instance.isPlaying)
        {
            if(GameManager.instance.currentScore - lastBossSpawnScore >= 20)
            {
                bossSpawn = true;
                timeUntilObstacleSpawn = 0f;
                StartCoroutine(SpawnBossWithBehavior());
                lastBossSpawnScore = GameManager.instance.currentScore;
            } else
            {
                SpawnLoop();
            }
        }
        
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if(timeUntilObstacleSpawn >= obstacleSpawnTime)
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
       obstacleRB.velocity = Vector2.left * obstacleSpeed;
    }

    private IEnumerator SpawnBossWithBehavior()
    {
        GameObject spawnableObstacle = Instantiate(bossObstaclePrefab, transform.position, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnableObstacle.GetComponent<Rigidbody2D>();
        // Move a little bit to the left
        obstacleRB.position = Vector2.left * -6.5f;

        yield return new WaitForSeconds(bossPauseDuration);

        // Move left again after the pause
        obstacleRB.velocity = Vector2.left * (obstacleSpeed * 2f);

        bossSpawn = false;
    }
}
