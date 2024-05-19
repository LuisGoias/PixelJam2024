using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTime = 4f;
    private float timeUntilObstacleSpawn;
    [SerializeField] private float obstacleSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
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
}
