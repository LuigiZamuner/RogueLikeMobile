using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public static SpawnEnemys instance;
    private List<GameObject> enemyList = new List<GameObject>();

    private void Start()
    {
        instance = this;
    }

    public void SpawnsRandomPositionEnemies(HashSet<Vector2Int> floorPosition, GameObject enemy, int numberOfEnemies)
    {
        DeleteEnemies();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var randomSquare = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            Vector2 spawnPosition = new Vector3(randomSquare.x, randomSquare.y);
            GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            enemyList.Add(spawnedEnemy);
        }
    }

    public void DeleteEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
        enemyList.Clear();
    }
}
