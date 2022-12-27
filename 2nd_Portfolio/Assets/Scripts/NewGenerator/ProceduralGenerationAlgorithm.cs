using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
