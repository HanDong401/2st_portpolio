using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomDungeonGenerator
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField] private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField,Range(0, 10)] private int offset = 1;
    [SerializeField] private bool randomWalkRooms = false;
    public delegate void RoomEvent();
    private RoomEvent roomEvent = null;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        Debug.Log("크리에이트 룸 잘 들어옴");
        var roomsList = ProceduralGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt
            ((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        HashSet<Vector2Int> propsTile = new HashSet<Vector2Int>(floor);
        HashSet<Vector2Int>  UsingTeleportTilePos = new HashSet<Vector2Int>(propsTile);
        HashSet<Vector2Int>  UsingStartTilePos = new HashSet<Vector2Int>(propsTile);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        #region 생성 관련 코드
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualizer);

        //Debug.Log("소품타일 배치 진입");
        HashSet<Vector2Int> propsTiles = new HashSet<Vector2Int>();
        propsTiles = props.SetPropRandomVector2Pos(propsTile);
        HashSet<Vector2Int> UsingChestTilePos = new HashSet<Vector2Int>(propsTiles);//chest에서 쓸거
        tileMapVisualizer.PaintPropsTile(propsTiles);

        //여기다가 텔레포트위치, 층이동 등등 할 함수 호출
        HashSet<Vector2Int> teleportTilePos = new HashSet<Vector2Int>();//텔레포트 위치 잡는 함수 호출
        teleportTilePos = props.SetTeleportTilePos(UsingTeleportTilePos, propsTile);
        HashSet<Vector2Int> forChestTiletoTeleport = new HashSet<Vector2Int>(teleportTilePos);//chest에서 쓸거
                                                                                              //tileMapVisualizer.PaintTeleportTile(teleportTilePos);
        specialTileInstantiator.DeleteTeleport();
        specialTileInstantiator.InstatntiateTeleport(teleportTilePos);

        //스타트 위치 함수 호출
        HashSet<Vector2Int> startTilePos = new HashSet<Vector2Int>();//위치 잡는 함수 호출
        startTilePos = props.SetStartTilePosition(teleportTilePos, UsingStartTilePos);
        specialTileInstantiator.DeleteStart();//기존 위치 삭제
        specialTileInstantiator.InstatntiateStart(startTilePos);

        //아이템 상자 위치 함수
        HashSet<Vector2Int> chestTiles = new HashSet<Vector2Int>();
        chestTiles = props.SetItemChestTile(forChestTiletoTeleport, startTilePos, UsingChestTilePos);
        //tileMapVisualizer.PaintChestTile(chestTiles);//테스트 때문에 일단 주석처리
        specialTileInstantiator.DeleteChestItem();
        specialTileInstantiator.InstatntiateChestItem(chestTiles);
        #endregion
        //여기서 길찾기 델리게이트 호출
        if (roomEvent != null)
            roomEvent();
    }

    public void AddRoomEvent(RoomEvent _callback)
    {
        roomEvent = _callback;
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> _roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < _roomsList.Count; i++)
        {
            var roomBounds = _roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> _roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = _roomCenters[Random.Range(0, _roomCenters.Count)];
        _roomCenters.Remove(currentRoomCenter);

        while (_roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, _roomCenters);
            _roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int _currentRoomCenter, Vector2Int _destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = _currentRoomCenter;
        corridor.Add(position);
        while (position.y != _destination.y)
        {
            if (_destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (_destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != _destination.x)
        {
            if (_destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (_destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int _currentRoomCenter, List<Vector2Int> _roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in _roomCenters)
        {
            float currentDistance = Vector2.Distance(position, _currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> _roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in _roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
