using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTileVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap, wallTileMap;
    [SerializeField] private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2> _floorPos)
    {
        PaintTiles(_floorPos, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2> _floorPos, Tilemap _tileMap, TileBase _tileBase)
    {
        foreach(var position in _floorPos)
        {
            PaintSingleTile(_tileMap, _tileBase, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2 _position)
    {
        PaintSingleTile(wallTileMap, wallTop, _position);
    }

    private void PaintSingleTile(Tilemap _tileMap, TileBase _tileBase, Vector2 _pos)
    {
        var tilePos = _tileMap.WorldToCell((Vector3)_pos);
        _tileMap.SetTile(tilePos, _tileBase);
    }
    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }
}
