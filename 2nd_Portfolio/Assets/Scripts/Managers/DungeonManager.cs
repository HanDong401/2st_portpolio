using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    List<Vector2Int> m_Centers;
    [SerializeField] float m_AreaRange = 0f;

    public void GetCenterPos(Vector2Int[] _pos)
    {
        m_Centers = _pos.ToList<Vector2Int>();
    }

    public void SerchStartPos(Vector2 _targetPos)
    {
        foreach(var center in m_Centers)
        {
            if (Vector2.Distance(center, _targetPos) < m_AreaRange)
                m_Centers.Remove(center);
        }
    }

    IEnumerator CompareDistance(Vector2 _targetPos)
    {
        Vector2 inPos;

        while(true)
        {
            foreach (var center in m_Centers)
            {
                if (Vector2.Distance(center, _targetPos) <= m_AreaRange)
                {
                    inPos = center;
                }
            }

            // ���� ��ġ�� ���� �Լ� ����
            // ���� ��ȯ ��ȯ�� 1~2���� ������ȯ
            // 

            yield return new WaitForSeconds(1f);
        }
    }
}
