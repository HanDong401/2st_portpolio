using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private int openDirection;
    //��,�Ʒ�,��,�� ������� 1,2,3,4

    private RoomTempletes templetes;
    private bool isSpawn=false;
    public bool IsSpawn { get { return isSpawn; } }
    private void Start()
    {
        templetes = GameObject.FindGameObjectWithTag("ROOMS").GetComponent<RoomTempletes>();
        Invoke("Spawn",0.1f);
    }

    private void Spawn()
    {
        if(isSpawn==false)
        {
            if (openDirection == 1)
            {
                templetes.InstantiateTopRoom(transform);
            }
            else if (openDirection == 2)
            {
                templetes.InstantiateBottomRoom(transform);
            }
            else if (openDirection == 3)
            {
                templetes.InstantiateLeftRoom(transform);
            }
            else if (openDirection == 4)
            {
                templetes.InstantiateRightRoom(transform);
            }
        }
        isSpawn=true;
    }
    
}
