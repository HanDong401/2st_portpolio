using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData : MonoBehaviour
{
    public List<Room> Rooms { get; set; } = new List<Room>();
    public HashSet<Vector2Int> Path { get; set; } = new HashSet<Vector2Int>();

    public GameObject PlayerRef { get; set; }

    public void Reset()
    {
        foreach(Room room in Rooms)
        {
            foreach(var item in room.ProbObjectRef)
            {
                Destroy(item);
            }
            foreach(var item in room.EnemiesInTheRoom)
            {
                Destroy(item);
            }
        }
        Rooms = new();
        Path = new();
        Destroy(PlayerRef);
    }
    public IEnumerator TurotialCoroutine(Action _code)
    {
        yield return new WaitForSeconds(1);
        _code();
    }
}
public class Room
{ 
    public Vector2 RoomCenterPos { get; set; }
    public HashSet<Vector2Int> FloorTiles { get; private set; } = new HashSet<Vector2Int>();

    public HashSet<Vector2Int> NearWallTilesUp { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> NearWallTilesDown { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> NearWallTilesLeft { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> NearWallTilesRight { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> CornerTiles { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> InnerTiles { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> PropPos { get; set; } = new HashSet<Vector2Int>();
    public List<GameObject> ProbObjectRef { get; set; } = new List<GameObject>();
    public HashSet<Vector2Int> PosAccessableFromPath { get; set; } = new HashSet<Vector2Int>();
    public List<GameObject> EnemiesInTheRoom { get; set; } = new List<GameObject>();


    public Room(Vector2 _roomCenterPos, HashSet<Vector2Int> _floorTiles)
    {
        this.RoomCenterPos = _roomCenterPos;
        this.FloorTiles = _floorTiles;
    }

}
