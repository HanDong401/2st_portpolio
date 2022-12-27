using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomDungeonGenerator
{
    [SerializeField]private int corriorLength = 14, corridorCnt = 5;
    [SerializeField][Range(0.1f,1)]private float roomPercent = 0.8f;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }
    private void CorridorFirstGeneration()
    {
        HashSet<Vector2> floorPos = new HashSet<Vector2>();
        HashSet<Vector2> potentialRoomPos = new HashSet<Vector2>();

        CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2> roomPos = CreateRooms(potentialRoomPos);

        floorPos.UnionWith(roomPos);

        tileMapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizer);
    }

    private HashSet<Vector2> CreateRooms(HashSet<Vector2> _potentialRoomPos)
    {
        HashSet<Vector2> roomPositions = new HashSet<Vector2>();
        int roomToCreateCount = Mathf.RoundToInt(_potentialRoomPos.Count*roomPercent);

        List<Vector2> roomToCreate = _potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach(var roomPos in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPos);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2> _floorPos, HashSet<Vector2> _potentialRoomPos)
    {
        var currentPos = startPos;
        _potentialRoomPos.Add(currentPos);
        for (int i = 0; i < corridorCnt; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorrider(currentPos, corriorLength);
            currentPos = corridor[corridor.Count-1];
            _potentialRoomPos.Add(currentPos);

            _floorPos.UnionWith(corridor);
        }
    }
}
