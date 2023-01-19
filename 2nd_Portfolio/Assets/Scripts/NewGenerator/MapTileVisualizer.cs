using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapTileVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap, propsTileMap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight, wallInnerCornerUpLeft, wallInnerCornerUpRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;
    [SerializeField] private TileBase[] propTiles;

    public void PaintFloorTiles(IEnumerable<Vector2Int> _floorPositions)
    {
        PaintTiles(_floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> _positions, Tilemap _tilemap, TileBase _tile)
    {
        foreach (var position in _positions)
        {
            PaintSingleTile(_tilemap, _tile, position);
        }
    }
    public void PaintPropsTile(IEnumerable<Vector2Int> _positions)//소품 타일 페인트 하는 위치
    {
        //Debug.Log("페인트 프롭스 타일 진입");
        foreach (var position in _positions)
        {
            //Debug.Log("페인트 프롭스 타일 포 이치 문 진입");
            PaintSingleTile(propsTileMap, propTiles[Random.Range(0, propTiles.Length)], position);
        }
    }
    
    
    internal void PaintSingleBasicWall(Vector2Int _position, string _binaryType)
    {
        int typeAsInt = Convert.ToInt32(_binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if (tile != null) PaintSingleTile(wallTilemap, tile, _position);
    }

    private void PaintSingleTile(Tilemap _tilemap, TileBase _tile, Vector2Int _position)
    {
        var tilePosition = _tilemap.WorldToCell((Vector3Int)_position);
        _tilemap.SetTile(tilePosition, _tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        propsTileMap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int _position, string _binaryType)
    {
        int typeASInt = Convert.ToInt32(_binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeASInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeASInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallInnerCornerUpLeft.Contains(typeASInt))
        {
            tile = wallInnerCornerUpLeft;
        }
        else if (WallTypesHelper.wallInnerCornerUpRight.Contains(typeASInt))
        {
            tile = wallInnerCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeASInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeASInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, _position);
    }
}
