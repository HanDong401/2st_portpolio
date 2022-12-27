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

        List<Vector2> deadEnds = FindAllDeadEnds(floorPos);
        CreateRoomsAtDeadEnds(deadEnds, roomPos);

        floorPos.UnionWith(roomPos);

        tileMapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizer);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2> _deadEnds, HashSet<Vector2> _roomFloors)
    {
        foreach(var pos in _deadEnds)
        {
            if(_roomFloors.Contains(pos)==false)
            {
                var room = RunRandomWalk(randomWalkParameters, pos);
                _roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2> FindAllDeadEnds(HashSet<Vector2> _floorPos)
    {
        List<Vector2> deadEnds = new List<Vector2>();
        foreach(var position in _floorPos)
        {
            int neighborsCount = 0;
            foreach(var direction in ProceduralGenerationAlgorithm.Direction2D.cardinalDirectionsList)
            {
                if(_floorPos.Contains(position+direction))
                {
                    neighborsCount++;
                }
            }
            if(neighborsCount==1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
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
