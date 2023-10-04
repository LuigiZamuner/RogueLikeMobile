using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    public Transform playerPos;

    //Spawns support
    private List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> chestList = new List<GameObject>();

    //boss second form support
    public int total = 0;

    //rooms support
    public bool allEnemiesDied = false;
    [SerializeField]
     GameObject door;

    private void Awake()
    {
        EventManager.Initialize();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {

    }
    //SPAWNS
    public void SpawnsRandomPositionEnemies(HashSet<Vector2Int> floorPosition, GameObject enemy, int numberOfEnemies)
    {
        DeleteEnemies();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var randomSquare = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            Vector2 spawnPosition = new Vector3(randomSquare.x + 0.5f, randomSquare.y + 0.6f);
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
    public void SpawnsRandomPositionChest(HashSet<Vector2Int> floorPosition, GameObject chest, int numberOfChests)
    {
        DeleteChests();
        for (int i = 0; i < numberOfChests; i++)
        {
            var randomSquare = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            Vector2 spawnPosition = new Vector3(randomSquare.x + 0.8f, randomSquare.y +0.6f);
            GameObject spawnedChest = Instantiate(chest, spawnPosition, Quaternion.identity);
            chestList.Add(spawnedChest);
        }
    }

    public void DeleteChests()
    {
        foreach (GameObject chest in chestList)
        {
            Destroy(chest);
        }
        chestList.Clear();
    }
    //boss
    public void SecondFormBossspawner(int pillarDestroyed)
    {
         total += pillarDestroyed;
       
    }
    public bool AllEnemiesDiedDoorApear()
    {
        if(enemyList.Count == 0)
        {
            allEnemiesDied= true;
            return allEnemiesDied;
        }
        else
        {
            allEnemiesDied = false;
            return allEnemiesDied;
        }
       
    }
}
