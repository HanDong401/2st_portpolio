using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    [SerializeField] GameObject TestPrefab = null;
    Vector2 MoveDir;
    void Update()
    {
        MoveDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(TestPrefab, MoveDir, Quaternion.identity);
        }
    }

}
