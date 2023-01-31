using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public DropItem coin = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(DropCoin());
        }
    }

    IEnumerator DropCoin()
    {
        int dropCount = Random.Range(1, 10);

        for (int i = 0; i < dropCount; ++i)
        {
            DropItem go = Instantiate(coin);

            go.transform.position = this.transform.position;
            go.DropObject();
            yield return new WaitForSeconds(0.1f);
            go.SetFollowing(true);
        }
    }

}
