using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{//1230 김준우 이 스크립트 안쓸수도 있음
    private static List<Vector2Int> neighbour4Directions = new List<Vector2Int>
    {
        new Vector2Int(0,1),//up
        new Vector2Int(1,0),//right
        new Vector2Int(0,-1),//down
        new Vector2Int(-1,0)//left
    };
    private static List<Vector2Int> neighbour8Directions = new List<Vector2Int>
    {
        new Vector2Int(0,1),//up
        new Vector2Int(1,0),//right
        new Vector2Int(0,-1),//down
        new Vector2Int(-1,0),//left
        new Vector2Int(1,1),//diagonal
        new Vector2Int(1,-1),
        new Vector2Int(-1,1),
        new Vector2Int(-1,-1)
    };
    List<Vector2Int> graph;

    public Graph(IEnumerable<Vector2Int> _vertices)
    {
        graph = new List<Vector2Int>(_vertices);
    }
    public List<Vector2Int> GetNeighbours4Direction(Vector2Int _startPos)
    {
        return GetNeighbours(_startPos, neighbour4Directions);
    }
    public List<Vector2Int> GetNeighbours8Direction(Vector2Int _startPos)
    {
        return GetNeighbours(_startPos, neighbour8Directions);
    }
    private List<Vector2Int> GetNeighbours(Vector2Int _startPos, List<Vector2Int> _neighbourOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        foreach(var neighbourDir in _neighbourOffsetList)
        {
            Vector2Int potentialNeighbour = _startPos + neighbourDir;
            if(graph.Contains(potentialNeighbour))
            {
                neighbours.Add(potentialNeighbour);
            }
        }
        return neighbours;
    }
}
