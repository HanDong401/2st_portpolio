using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomDungeonGenerator
{
    [SerializeField] private float minRoomWidth = 0.64f, minRoomHeight = 0.64f;
    [SerializeField] private float dungeonWidth = 3.2f, dungeonHeight = 3.2f;
    [SerializeField, Range(0, 10f)] private int offset = 1;
    [SerializeField] private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProceduralGenerationAlgorithm.BinarySpacePartitioning
            (new Bounds((Vector3)startPos,new Vector3(dungeonWidth,dungeonHeight,0)),minRoomWidth,minRoomHeight);

        HashSet<Vector2> floor = new HashSet<Vector2>();
        floor = CreateSimpleRooms(roomList);

        List<Vector2> roomCenters = new List<Vector2>();
        foreach(var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        HashSet<Vector2> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor,tileMapVisualizer);
    }

    private HashSet<Vector2> ConnectRooms(List<Vector2> _roomCenters)
    {
        HashSet<Vector2> corridors = new HashSet<Vector2>();
        var currRoomCenter = _roomCenters[Random.Range(0,_roomCenters.Count)];
        _roomCenters.Remove(currRoomCenter);

        while(_roomCenters.Count>0)
        {
            Vector2 closet = FindClosetPointTo(currRoomCenter, _roomCenters);
            _roomCenters.Remove(closet);
            HashSet<Vector2> newCorridor = CreateCorridor(currRoomCenter, closet);
            currRoomCenter = closet;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2> CreateCorridor(Vector2 _currRoomCenter, Vector2 _destination)
    {
        HashSet<Vector2> corridor = new HashSet<Vector2>();
        var position = _currRoomCenter;
        corridor.Add(position);
        while(position.y != _destination.y)
        {
            if(_destination.y>position.y)
            {
                position.y += 0.16f;
            }
            else if(_destination.y<position.y)
            {
                position.y += -0.16f;
            }
            corridor.Add(position);
        }
        while(position.x != _destination.x)
        {
            if (_destination.x > position.x)
            {
                position.x += 0.16f;
            }
            else if (_destination.x < position.x)
            {
                position.x += -0.16f;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2 FindClosetPointTo(Vector2 _currRoomCenter, List<Vector2> _roomCenters)
    {
        Vector2 closet = Vector2.zero;
        float length = float.MaxValue;
        foreach(var position in _roomCenters)
        {
            float currDistance = Vector2.Distance(position, _currRoomCenter);
            if(currDistance < length)
            {
                length = currDistance;
                closet = position;
            }
        }
        return closet;
    }

    private HashSet<Vector2> CreateSimpleRooms(List<Bounds> _roomList)
    {
        HashSet<Vector2> floor = new HashSet<Vector2>();
        foreach(var room in _roomList)
        {
            for(float col=(offset*0.16f);col<room.size.x-(offset*0.16f);col+=0.16f)
            {
                for(float row = (offset*0.16f);row<room.size.y-(offset*0.16f);row+=0.16f)
                {
                    Vector2 position = (Vector2)room.min + new Vector2(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
