using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    public Player player;
    public GameObject hitObj;
    public LayerMask layer;

    void Start()
    {
            
    }

    void Update()
    {
        Ray2D ray = new Ray2D(transform.position, (player.transform.position - transform.position));
        Debug.DrawRay(transform.position, (player.transform.position - transform.position));
    }
}
