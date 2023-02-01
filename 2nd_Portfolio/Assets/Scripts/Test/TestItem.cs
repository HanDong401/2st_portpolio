using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public DropItem coin = null;
    [SerializeField] private Chest m_NormalChestPrefab = null;
    [SerializeField] private Chest m_PassiveChestPrefab = null;
    [SerializeField] private Chest m_ActiveChestPrefab = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //StartCoroutine(DropCoin());
            RandomPopChest(this.transform.position);
        }
    }

    public void RandomPopChest(Vector2 _pos)
    {
        int randomIndex = Random.Range(1, 101);

        if (randomIndex < 60)
        {
            // 일반상자 소환
            Chest chest = Instantiate(m_NormalChestPrefab);
            chest.transform.position = _pos;
            Debug.Log("일반상자");
        }
        else if (randomIndex < 90)
        {
            // 패시브상자 소환
            Chest chest = Instantiate(m_PassiveChestPrefab);
            chest.transform.position = _pos;
            Debug.Log("패시브상자");
        }
        else
        {
            //액티브상자 소환
            Chest chest = Instantiate(m_ActiveChestPrefab);
            chest.transform.position = _pos;
            Debug.Log("액티브상자");
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
        }
    }

}
