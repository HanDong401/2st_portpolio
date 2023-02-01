using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorFirstDungeonGenerator : SimpleRandomDungeonGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField, Range(0.1f, 1)] private float roomPercent = 0.8f;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    private List<Color> roomColors =new List<Color>();
    [SerializeField] private bool showRoomGizmo = false, showCorridorsGizmo;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }
    private void CreateRoomsAtDeadEnd(List<Vector2Int> _deadEnds, HashSet<Vector2Int> _roomFloors)
    {
        foreach (var position in _deadEnds)
        {
            if (_roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                _roomFloors.UnionWith(room);
            }
        }
    }
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> _floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in _floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (_floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> _potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(_potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = _potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        ClearRoomData();
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);

            SaveRoomData(roomPosition, roomFloor);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }
    private void ClearRoomData()
    {
        roomDictionary.Clear();
        roomColors.Clear();
    }
    private void SaveRoomData(Vector2Int _roomPosition, HashSet<Vector2Int> _roomFloor)
    {
        roomDictionary[_roomPosition] = _roomFloor;
        roomColors.Add(Random.ColorHSV());
    }
    private void CreateCorridors(HashSet<Vector2Int> _floorPositions, HashSet<Vector2Int> _potentialRoomPositions)
    {
        var currentPosition = startPosition;
        _potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            _potentialRoomPositions.Add(currentPosition);
            _floorPositions.UnionWith(corridor);
        }
    }
}