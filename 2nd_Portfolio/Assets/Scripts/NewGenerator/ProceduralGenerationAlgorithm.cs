using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithm
{
    public static HashSet<Vector2> SimpleRandomWalk(Vector2 _startPosition, int _walkLength)
    {
        HashSet<Vector2> path = new HashSet<Vector2>();

        path.Add(_startPosition);
        var previousPosition = _startPosition;

        for(int i = 0; i < _walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    public static List<Vector2> RandomWalkCorrider(Vector2 _startPos, int _corridorLength)
    {
        List<Vector2> corridor = new List<Vector2>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currPos = _startPos;
        corridor.Add(currPos);

        for(int i=0; i< _corridorLength; i++)
        {
            currPos += direction;
            corridor.Add(currPos);
        }
        return corridor;
    }

    public static List<Bounds> BinarySpacePartitioning(Bounds _spaceToSplit, float _minWidth, float _minHeight)
    {
        Queue<Bounds> roomsQueue = new Queue<Bounds>();
        List<Bounds> roomList = new List<Bounds>();
        roomsQueue.Enqueue(_spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room =roomsQueue.Dequeue();
            if(room.size.y>=_minHeight&&room.size.x>=_minWidth)
            {
                if (Random.value<0.5f)//생성 알고리즘 수정할 때 여기 조질것
                {
                    if(room.size.y >= _minHeight * 2)
                    {
                        SplitHorizontally(_minHeight, roomsQueue, room);
                    }
                    else if(room.size.x >= _minWidth*2)
                    {
                        SplitVertically(_minWidth,roomsQueue,room);
                    }
                    else if(room.size.x>=_minWidth&&room.size.y>_minHeight)
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= _minWidth * 2)
                    {
                        SplitVertically(_minWidth, roomsQueue, room);
                    }
                    else if(room.size.y >= _minHeight * 2)
                    {
                        SplitHorizontally(_minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= _minWidth && room.size.y > _minHeight)
                    {
                        roomList.Add(room);
                    }
                }
            }
        }
        return roomList;
    }

    private static void SplitVertically(float _minWidth, Queue<Bounds> _roomsQueue, Bounds _room)
    {
        var xSplit = Random.Range(1, _room.size.x);
        Bounds room1 = new Bounds(_room.min, new Vector3(xSplit, _room.min.y, _room.min.z));
        Bounds room2 = new Bounds(new Vector3(_room.min.x+xSplit, _room.min.y, _room.min.z),
            new Vector3(_room.size.x-xSplit,_room.size.y,_room.size.z));
        _roomsQueue.Enqueue(room1);
        _roomsQueue.Enqueue(room2);

    }

    private static void SplitHorizontally(float _minHeight, Queue<Bounds> _roomsQueue, Bounds _room)
    {
        var ySplit = Random.Range(1, _room.size.y);
        Bounds room1 = new Bounds(_room.min, new Vector3(_room.size.x, ySplit, _room.size.z));
        Bounds room2 = new Bounds(new Vector3(_room.min.x, _room.min.y+ ySplit, _room.min.z),
            new Vector3(_room.size.x, _room.size.y-ySplit, _room.size.z));
        _roomsQueue.Enqueue(room1);
        _roomsQueue.Enqueue(room2);
    }

    public static class Direction2D
    {
        public static List<Vector2> cardinalDirectionsList = new List<Vector2>
        {
            new Vector2(0,0.16f),//up
            new Vector2(0.16f,0),//right
            new Vector2(0,-0.16f),//down
            new Vector2(-0.16f,0),//left
        };

        public static Vector2 GetRandomCardinalDirection()
        {
            return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];
        }
    }
}
