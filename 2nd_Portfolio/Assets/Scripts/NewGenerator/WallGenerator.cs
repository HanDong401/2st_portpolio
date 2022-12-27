using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2> _floorPos, MapTileVisualizer _tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(_floorPos,
            ProceduralGenerationAlgorithm.Direction2D.cardinalDirectionsList);//절차적맵생성 쪽에서 받아오는게 맞는지 확인 필요함..

        foreach(var position in basicWallPositions)
        {
            _tileMapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2> FindWallsInDirections(HashSet<Vector2> _floorPos, List<Vector2> _directionsList)
    {
        HashSet<Vector2> wallPositions = new HashSet<Vector2>();
        foreach (var position in _floorPos)
        {
            foreach(var direction in _directionsList)
            {
                var neighborPos = position + direction;
                if(_floorPos.Contains(neighborPos)==false)
                {
                    wallPositions.Add(neighborPos);
                }
            }
        }
        return wallPositions;
    }
}
