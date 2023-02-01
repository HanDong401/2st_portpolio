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
            // �Ϲݻ��� ��ȯ
            Chest chest = Instantiate(m_NormalChestPrefab);
            chest.transform.position = _pos;
            Debug.Log("�Ϲݻ���");
        }
        else if (randomIndex < 90)
        {
            // �нú���� ��ȯ
            Chest chest = Instantiate(m_PassiveChestPrefab);
            chest.transform.position = _pos;
            Debug.Log("�нú����");
        }
        else
        {
            //��Ƽ����� ��ȯ
            Chest chest = Instantiate(m_ActiveChestPrefab);
            chest.transform.position = _pos;
            Debug.Log("��Ƽ�����");
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
