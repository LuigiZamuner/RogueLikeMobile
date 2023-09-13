using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapAdds : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private TileBase floorTileBase;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private TileBase neutralWallTile;

    public List<Vector2Int> paintedWallPositions = new List<Vector2Int>();

    public void PaintFloor(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTileBase);
    }
    private void PaintTiles(IEnumerable<Vector2Int> positions,Tilemap tilemap,TileBase tileBase)
    {
        foreach(var position in positions)
        {
            PaintSingleTile(tilemap, tileBase, position);
        }
    }
    internal void PaintNeutralWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, neutralWallTile, position);
        paintedWallPositions.Add(position);
    }
    private void PaintSingleTile(Tilemap tilemap, TileBase tileBase, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tileBase);
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
    public bool IsWallPosition(Vector2Int position)
    {
        return paintedWallPositions.Contains(position);
    }
}
