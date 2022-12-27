using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTempletes : MonoBehaviour
{
    [SerializeField] private GameObject[] topRooms;
    [SerializeField] private GameObject[] bottomRooms;
    [SerializeField] private GameObject[] leftRooms;
    [SerializeField] private GameObject[] rightRooms;
    private int rand = 0;
    public void InstantiateBottomRoom(Transform _position)
    {
        rand = Random.Range(0, bottomRooms.Length);
        Instantiate(bottomRooms[rand], _position.position, _position.rotation);
    }
    public void InstantiateTopRoom(Transform _position)
    {
        rand = Random.Range(0, topRooms.Length);
        Instantiate(topRooms[rand], _position.position, _position.rotation);
    }
    public void InstantiateLeftRoom(Transform _position)
    {
        rand = Random.Range(0, leftRooms.Length);
        Instantiate(leftRooms[rand], _position.position, _position.rotation);
    }
    public void InstantiateRightRoom(Transform _position)
    {
        rand = Random.Range(0, rightRooms.Length);
        Instantiate(rightRooms[rand], _position.position, _position.rotation);
    }
    private void OnTriggerEnter2D(Collider2D _other)
    {
        Debug.Log("aaaaa");
        if (_other.CompareTag("SPAWNPOINT") && _other.GetComponent<MapSpawner>().IsSpawn == true)
        {
            Debug.Log("bbbbb");
            Destroy(gameObject);
        }
    }
}
