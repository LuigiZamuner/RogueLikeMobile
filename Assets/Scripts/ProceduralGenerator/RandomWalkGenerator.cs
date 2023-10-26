using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RandomWalkGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int startPosition = Vector2Int.zero;
    [SerializeField] private int iterations = 10;
    [SerializeField] private int walkLength = 10;
    [SerializeField] private bool startRandomIterations = true;

    [SerializeField] private TileMapAdds tileMapAdds;
    private HashSet<Vector2Int> floorPositions;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject bossDoorPrefab;
    private bool readyToBoss = false;

    private void Start()
    {
        EventManager.AddBoolListener(EventName.AllEnemiesDied, SpawnsDoorToNextRoom);
        RunProceduralGenerator();
    }
    public void RunProceduralGenerator()
    {
        GameManager.instance.roomCount++;
        floorPositions = RunRandomWalk();
        tileMapAdds.Clear();
        tileMapAdds.PaintFloor(floorPositions);
        WallPlacement.CreateWalls(floorPositions, tileMapAdds);
        Vector2Int spawnPosition = WallPlacement.floorPositionsToSave;


        floorPositions.ExceptWith(tileMapAdds.paintedWallPositions);
        GameManager.instance.SpawnsRandomPositionEnemies(floorPositions, enemyPrefab, 3);
        GameManager.instance.SpawnsRandomPositionChest(floorPositions, chestPrefab, 1);
        GameManager.instance.playerPos.position = new Vector3(spawnPosition.x + 0.5f, spawnPosition.y + 0.6f, 0);

        if(GameManager.instance.roomCount == 3)
        {
            readyToBoss = true;

        }
          
        
    }
    private HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        var floorPosition = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralAlgorithmsGenerator.SimpleRandomWalk(currentPosition, walkLength);
            floorPosition.UnionWith(path);

            if (startRandomIterations)
            {
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }

        return floorPosition;
    }

    public void SpawnsDoorToNextRoom(bool allEnemiesDied)
    {
        if(allEnemiesDied == true)
        {
            if(readyToBoss)
            {
                var randomSquare = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
                Vector2 spawnPosition = new Vector3(randomSquare.x + 0.6f, randomSquare.y + 0.6f);
                Instantiate(bossDoorPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                var randomSquare = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
                Vector2 spawnPosition = new Vector3(randomSquare.x + 0.6f, randomSquare.y + 0.6f);
                Instantiate(doorPrefab, spawnPosition, Quaternion.identity);
            }
        }

    }
}

