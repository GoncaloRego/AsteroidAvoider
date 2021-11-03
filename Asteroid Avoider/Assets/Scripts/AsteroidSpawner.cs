using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float interval;
    [SerializeField] private float moveSpeed;

    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnAsteroid();
            timer += interval;
        }
    }

    void SpawnAsteroid()
    {
        int screenSide = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (screenSide)
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
            case 2:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 3:
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            default:
                break;
        }

        Vector2 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);

        GameObject randomAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject newAsteroid = Instantiate(randomAsteroid, worldSpawnPoint, Quaternion.Euler(0, 0f, Random.Range(0f, 360f)));
        Rigidbody2D asteroidRigidbody = newAsteroid.GetComponent<Rigidbody2D>();

        asteroidRigidbody.velocity = direction.normalized * moveSpeed;
    }
}
