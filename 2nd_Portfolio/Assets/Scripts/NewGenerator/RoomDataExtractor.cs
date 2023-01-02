using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDataExtractor : MonoBehaviour
{
    private DungeonData dungeonData;

    [SerializeField] private bool showGizmo = false;
    public UnityEvent OnFinishedRoomProcessing;

    private void Awake()
    {
        dungeonData = FindObjectOfType<DungeonData>();
    }

    public void ProcessRooms()
    {
        if(dungeonData==null)
        {
            return;
        }
        foreach(Room room in dungeonData.Rooms)
        {
            foreach (Vector2Int tilePosition in room.FloorTiles)
            {
                int neighboursCnt = 4;
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.up) == false)
                {
                    room.NearWallTilesUp.Add(tilePosition);
                    neighboursCnt--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.down) == false)
                {
                    room.NearWallTilesDown.Add(tilePosition);
                    neighboursCnt--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.right) == false)
                {
                    room.NearWallTilesRight.Add(tilePosition);
                    neighboursCnt--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.left) == false)
                {
                    room.NearWallTilesLeft.Add(tilePosition);
                    neighboursCnt--;
                }

                if(neighboursCnt<=2)
                {
                    room.CornerTiles.Add(tilePosition);
                }
                else if(neighboursCnt==4)
                {
                    room.InnerTiles.Add(tilePosition);
                }
                room.NearWallTilesUp.ExceptWith(room.CornerTiles);
                room.NearWallTilesDown.ExceptWith(room.CornerTiles);
                room.NearWallTilesRight.ExceptWith(room.CornerTiles);
                room.NearWallTilesLeft.ExceptWith(room.CornerTiles);
            }
            Invoke("RunEvent",1);
        }
    }
    public void RunEvent()
    {
        OnFinishedRoomProcessing?.Invoke();
    }
    private void OnDrawGizmosSelected()
    {
        if(dungeonData==null||showGizmo==true)
        {
            return;
        }

        foreach(Room room in dungeonData.Rooms)
        {
            Gizmos.color = Color.yellow;
            foreach(Vector2Int floorPosition in room.InnerTiles)
            {
                if (dungeonData.Path.Contains(floorPosition))
                {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }

            Gizmos.color = Color.blue;
            foreach (Vector2Int floorPosition in room.NearWallTilesUp)
            {
                if (dungeonData.Path.Contains(floorPosition))
                {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }

            Gizmos.color = Color.green;
            foreach (Vector2Int floorPosition in room.NearWallTilesDown)
            {
                if (dungeonData.Path.Contains(floorPosition))
                {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }

            Gizmos.color = Color.red;
            foreach (Vector2Int floorPosition in room.NearWallTilesRight)
            {
                if (dungeonData.Path.Contains(floorPosition))
                {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }

            Gizmos.color = Color.white;
            foreach (Vector2Int floorPosition in room.NearWallTilesLeft)
            {
                if (dungeonData.Path.Contains(floorPosition))
                {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }

            Gizmos.color = Color.cyan;
            foreach (Vector2Int floorPosition in room.CornerTiles)
            {
                if (dungeonData.Path.Contains(floorPosition))
                {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
        }
    }
}
