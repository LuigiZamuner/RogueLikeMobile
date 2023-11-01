using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("player Support")]
    private GameObject player;
    [SerializeField]
    public Transform playerPos;
    [SerializeField]
    public Health playerHealth;
    [SerializeField]
    public AnimatorController playerAnimator;

    [Header("Spawns Support")]
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> chestList = new List<GameObject>();

    [Header("Boss Support")]
    public int total = 0;

    [Header("Rooms Support")]

    public bool allEnemiesDied = false;
    public int roomCount = 0;

    [Header("Canvas Support")]

    public int fishPointCounter = 0;




    private void Awake()
    {
        EventManager.Initialize();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform newPlayerPos = player.transform;
        playerPos = newPlayerPos;

        playerHealth = player.GetComponent<Health>();
        player.GetComponent<Animator>().runtimeAnimatorController = playerAnimator ;

        Debug.Log(player.GetComponent<Animator>().runtimeAnimatorController);

        if(scene.name == "InitialPlace")
        {
            AudioManager.instance.PlayMusic("InitialMusic");
            DOTween.To(() => AudioManager.instance.musicSource.volume, x => AudioManager.instance.musicSource.volume = x, 0.2f, 4f);

        }
        else if (scene.name == "Rooms")
        {
            AudioManager.instance.PlayMusic("RoomsMusic");
            DOTween.To(() => AudioManager.instance.musicSource.volume, x => AudioManager.instance.musicSource.volume = x, 0.2f, 4f);
        }
        else if (scene.name == "BossBattle")
        {
            AudioManager.instance.PlayMusic("BossMusic");
        }
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
    //Player
    public void ChangePlayerAnimatorController(AnimatorController aC)
    {
        playerAnimator = aC;
        player.GetComponent<Animator>().runtimeAnimatorController = playerAnimator;
    }

    public void FreezePlayer()
    {
       Rigidbody2D playerRb =  player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}
