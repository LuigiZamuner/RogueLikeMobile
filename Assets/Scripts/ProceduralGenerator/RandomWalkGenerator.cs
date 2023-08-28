using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int startPosition = Vector2Int.zero;
    [SerializeField] private int iterations = 10;
    [SerializeField] private int walkLength = 10;
    [SerializeField] private bool startRandomIterations = true;

    [SerializeField] private TileMapAdds tileMapAdds;
    private HashSet<Vector2Int> floorPositions;

    [SerializeField] private GameObject enemyPrefab;
    public void RunProceduralGenerator()
    {
        floorPositions = RunRandomWalk();
        tileMapAdds.Clear();
        tileMapAdds.PaintFloor(floorPositions);
        WallPlacement.CreateWalls(floorPositions, tileMapAdds);
        SpawnEnemys.instance.SpawnsRandomPositionEnemies(floorPositions, enemyPrefab, 5);


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
}

