using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallPlacement 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapAdds tileMapAdds)
    {
        var NeutralWallPositions = findsWalls(floorPositions, Directions2D.cardinalDirectionsList);
        foreach (var position in NeutralWallPositions)
        {
            tileMapAdds.PaintNeutralWall(position);
        }
    }
    public static HashSet<Vector2Int> findsWalls(HashSet<Vector2Int>floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions= new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var sidePosition = position + direction;
                if(!floorPositions.Contains(sidePosition))
                {
                    wallPositions.Add(sidePosition);
                }
            }
        }
        return wallPositions;
    }
}
