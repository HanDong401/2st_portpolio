using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> _floorPositions, MapTileVisualizer _tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(_floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(_floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWall(_tileMapVisualizer, basicWallPositions, _floorPositions);
        CreateCornerWalls(_tileMapVisualizer, cornerWallPositions, _floorPositions);
    }

    private static void CreateCornerWalls(MapTileVisualizer _tileMapVisualizer,
        HashSet<Vector2Int> _cornerWallPositions, HashSet<Vector2Int> _floorPositions)
    {
        foreach (var position in _cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (_floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            _tileMapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(MapTileVisualizer _tileMapVisualizer,
        HashSet<Vector2Int> _basicWallPositions, HashSet<Vector2Int> _floorPositions)
    {
        foreach (var position in _basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (_floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            _tileMapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> _floorPositions,
        List<Vector2Int> _directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in _floorPositions)
        {
            foreach (var direction in _directionList)
            {
                var neighbourPosition = position + direction;
                if (_floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }
        }
        return wallPositions;
    }
}
