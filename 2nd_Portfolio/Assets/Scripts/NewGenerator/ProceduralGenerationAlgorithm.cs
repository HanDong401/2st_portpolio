using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithm
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int _startPosition, int _walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(_startPosition);
        var previousPosition = _startPosition;

        for (int i = 0; i < _walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int _startPosition, int _corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = _startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < _corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt _spaceToSplit, int _minWidth, int _minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(_spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= _minHeight && room.size.x >= _minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= _minHeight * 2)
                    {
                        SplitHorizontally(_minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= _minWidth * 2)
                    {
                        SplitVertically(_minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= _minWidth && room.size.y >= _minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= _minWidth * 2)
                    {
                        SplitVertically(_minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= _minHeight * 2)
                    {
                        SplitHorizontally(_minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= _minWidth && room.size.y >= _minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int _minWidth, Queue<BoundsInt> _roomsQueue, BoundsInt _room)
    {
        var xSplit = Random.Range(1, _room.size.x);
        BoundsInt room1 = new BoundsInt(_room.min, new Vector3Int(xSplit, _room.size.y, _room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(_room.min.x + xSplit, _room.min.y, _room.min.z),
            new Vector3Int(_room.size.x - xSplit, _room.size.y, _room.size.z));
        _roomsQueue.Enqueue(room1);
        _roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int _minHeight, Queue<BoundsInt> _roomsQueue, BoundsInt _room)
    {
        var ySplit = Random.Range(1, _room.size.y);
        BoundsInt room1 = new BoundsInt(_room.min, new Vector3Int(_room.size.x, ySplit, _room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(_room.min.x, _room.min.y + ySplit, _room.min.z),
            new Vector3Int(_room.size.x, _room.size.y - ySplit, _room.size.z));
        _roomsQueue.Enqueue(room1);
        _roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[UnityEngine.Random.Range(0, cardinalDirectionsList.Count)];
    }
}
