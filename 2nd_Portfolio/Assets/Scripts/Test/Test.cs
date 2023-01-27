using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<BoxCollider2D> colls = new List<BoxCollider2D>();
    public Vector2 offset;
    public Vector2 size;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            BoxCollider2D coll = this.gameObject.AddComponent<BoxCollider2D>();
            coll.isTrigger = true;
            coll.offset = (Vector2)transform.position + new Vector2(i, 0f);
            coll.size = new Vector2(1, 1);
            colls.Add(coll);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach(BoxCollider2D coll in colls)
        {
            Gizmos.DrawWireCube(coll.offset, coll.size);
        }
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        foreach(var coll2 in colls)
        {
            if (coll2.IsTouching(coll).Equals(true))
            {
                Debug.Log(coll2.offset);
            }
        }
    }
}
