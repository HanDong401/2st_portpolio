using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkSo randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2> floorPos = RunRandomWalk(randomWalkParameters,startPos);
        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizer);
    }
    protected HashSet<Vector2> RunRandomWalk(SimpleRandomWalkSo _parameters, Vector2 _position)
    {
        var currPos = _position;
        HashSet<Vector2> floorPos = new HashSet<Vector2>();
        for(int i=0; i< _parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currPos, _parameters.walkLength);
            floorPos.UnionWith(path);
            if(_parameters.startRandomlyEachIteration)
            {
                currPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }
}
