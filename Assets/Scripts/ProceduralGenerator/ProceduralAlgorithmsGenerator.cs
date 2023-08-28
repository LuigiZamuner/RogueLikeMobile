using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralAlgorithmsGenerator
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLenght; i++)
        {
            var newPosition = previousPosition + Directions2D.GetARandomDirection();
            path.Add(newPosition);
            previousPosition= newPosition;
        }
        return path;
    }
}

public static class Directions2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //cima
        new Vector2Int(1,0), //direita
        new Vector2Int(0,-1),  //baixo
        new Vector2Int(-1,0)  //esquerda
    };

    public static Vector2Int GetARandomDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}

