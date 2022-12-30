using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random=UnityEngine.Random;

public class ItemPlaceHelper
{
    //1230 김준우 이 스크립트 안 쓸 수도 있고 나중에 다시 적겠음
    Dictionary<PlacementType, HashSet<Vector2Int>> tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();


    HashSet<Vector2Int> roomFloorNoCorridor;


    public ItemPlaceHelper(HashSet<Vector2Int> _roomFloor,
        HashSet<Vector2Int> _roomFloorNoCorridor)
    {
        Graph graph = new Graph(_roomFloor);
        this.roomFloorNoCorridor = _roomFloorNoCorridor;

        foreach (var pos in _roomFloorNoCorridor)
        {
            int neighboursCnt8Dir = graph.GetNeighbours8Direction(pos).Count;
            PlacementType type = neighboursCnt8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

            if (tileByType.ContainsKey(type) == false) tileByType[type] = new HashSet<Vector2Int>();

            if (type == PlacementType.NearWall && graph.GetNeighbours4Direction(pos).Count > 0) continue;

            tileByType[type].Add(pos);
        }
    }


    public Vector2? GetItemPlacementPosition(PlacementType _placementType, int _iterationsMax, Vector2Int _size, bool _addOffset)
    {
        int itemArea = _size.x * _size.y;
        if (tileByType[_placementType].Count < itemArea) return null;

        int iteration = 0;
        while(iteration < _iterationsMax)
        {
            iteration++;
            int index = Random.Range(0,tileByType[_placementType].Count);
            Vector2Int position = tileByType[_placementType].ElementAt(index);

            if (itemArea > 1)
            {
                var (result, placementPositions) = PlaceBigItem(position, _size, _addOffset);

                if (result == false) continue;

                tileByType[_placementType].ExceptWith(placementPositions);
                tileByType[PlacementType.NearWall].ExceptWith(placementPositions);
            }
            else
            {
                tileByType[_placementType].Remove(position);
            }
            return position;
        }
        return null;
    }

    private (bool, List<Vector2Int>) PlaceBigItem(Vector2Int _originPos, Vector2Int _size, bool _addOffset)
    {
        List<Vector2Int> positions = new List<Vector2Int>() { _originPos };
        int maxX = _addOffset ? _size.x + 1 : _size.x;
        int maxY = _addOffset ? _size.y + 1 : _size.y;
        int minX = _addOffset ? -1 : 0;
        int minY = _addOffset ? -1 : 0;

        for(int row = minX; row <=maxX; row++)
        {
            for(int col = minY; col <= maxY; col++)
            {
                if (col == 0 && row == 0) continue;

                Vector2Int newPosToCheck = new Vector2Int(_originPos.x + row, _originPos.y + col);
                if(roomFloorNoCorridor.Contains(newPosToCheck)==false)
                {
                    return (false, positions);
                }
                positions.Add(newPosToCheck);
            }
        }
        return (true, positions);
    }

    public enum PlacementType
    { 
        OpenSpace,
        NearWall
    }
}
