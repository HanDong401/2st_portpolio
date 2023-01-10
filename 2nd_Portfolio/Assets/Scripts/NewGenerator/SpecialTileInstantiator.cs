using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTileInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject TeleportPrefab, StartPrefab, TestChestItem;
    private GameObject FindHierachyTeleport,FindHierachyStart, FindHierachyChest;
    private Vector3 Pos = Vector3.zero;

    public void InstatntiateChestItem(HashSet<Vector2Int> _ChestPosition)
    {
        //Debug.Log("aaa");
        List<Vector2Int> ListChestPosition = new List<Vector2Int>(_ChestPosition);
        Pos.x = ListChestPosition[0].x;
        Pos.y = ListChestPosition[0].y;
        Instantiate(TestChestItem, Pos, Quaternion.identity);
    }
    
    public void InstatntiateTeleport(HashSet<Vector2Int> _TeleportPosition)
    {
        //Debug.Log("aaa");
        List<Vector2Int> ListTeleportPosition = new List<Vector2Int>(_TeleportPosition);
        Pos.x = ListTeleportPosition[0].x;
        Pos.y = ListTeleportPosition[0].y;
        Instantiate(TeleportPrefab, Pos, Quaternion.identity);
    }
    public void DeleteTeleport()
    {
        FindHierachyTeleport = GameObject.FindGameObjectWithTag("TELEPORT");
        Destroy(FindHierachyTeleport);
    }
    public void InstatntiateStart(HashSet<Vector2Int> _StartPosition)
    {
        //Debug.Log("aaa");
        List<Vector2Int> ListStartPosition = new List<Vector2Int>(_StartPosition);
        Pos.x = ListStartPosition[0].x;
        Pos.y = ListStartPosition[0].y;
        Instantiate(StartPrefab, Pos, Quaternion.identity);
    }
    public void DeleteStart()
    {
        FindHierachyStart = GameObject.FindGameObjectWithTag("START");
        Destroy(FindHierachyStart);
    }
    public void DeleteChestItem()
    {
        FindHierachyChest = GameObject.FindGameObjectWithTag("CHEST");
        Destroy(FindHierachyChest);
    }
}
