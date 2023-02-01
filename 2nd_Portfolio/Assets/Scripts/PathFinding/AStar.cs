using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� Ÿ�� �ϳ��� ������ �����ϰ��ִ� ���
[Serializable]
public class Node
{
    // ��� ������ ������ �ƴ��� bool���� ��ǥ���� �ʱ�ȭ ��
    public Node(bool _isWall, int _x, int _y)
    {
        mbIsWall = _isWall;
        x = _x;
        y = _y;
    }

    public bool mbIsWall;
    public int x, y, G, H;
    public Node m_ParentNode;
    // G�� �̵��Ÿ� H �� ������ġ���� ������������ �Ÿ� F �� G�� H�� ���Ѱ�
    public int F { get { return G + H; } }
}

// ��ã�� �˰���
public class AStar
{
    // ������ǥ�� ������ǥ�� ������ ����
    private Vector2Int m_StartPos, m_EndPos;
    // ���� ��� ����Ʈ
    private List<Node> m_FinalNodeList;
    public List<Node> FinalNodeList { get { return m_FinalNodeList; } }

    public Node[,] m_NodePos;
    private Node m_StartNode, m_EndNode, m_CurrNode;
    private List<Node> m_OpenList, m_EndList;

    private int m_xSize = 40;
    private int m_ySize = 20;

    // ����ü�� ��ǥ�� bool���� �ʱ�ȭ�ؼ� Node���·� ������
    public void InitNode()
    {
        m_NodePos = new Node[m_xSize, m_ySize];

        for (int i = 0; i < m_xSize; ++i)
        {
            for (int j = 0; j < m_ySize; ++j)
            {
                bool isWall = false;
                foreach (Collider2D coll in Physics2D.OverlapCircleAll(new Vector2(i, j), 0.4f))
                {
                    if (coll.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    {
                        isWall = true;
                        
                    }
                }

                m_NodePos[i, j] = new Node(isWall, i, j);
            }
        }
    }

    // ��ã�� �Լ�
    public List<Node> PathFinding(Vector2 _startPos, Vector2 _endPos)
    {
        // ������ġ�� ������ġ�� Vector2Int�������� ��ȯ
        m_StartPos = Vector2Int.RoundToInt(_startPos);
        m_EndPos = Vector2Int.RoundToInt(_endPos);

        // ��ġ�� ��������� ����
        m_StartNode = m_NodePos[m_StartPos.x, m_StartPos.y];
        m_EndNode = m_NodePos[m_EndPos.x, m_EndPos.y];

        // �� ����Ʋ�� �ʱ�ȭ
        m_OpenList = new List<Node> { m_StartNode };
        m_EndList = new List<Node>();
        m_FinalNodeList = new List<Node>();

        // ���� �������� ������ �˻��ϰ� ���ο� ������ �߰�
        while(m_OpenList.Count > 0)
        {
            m_CurrNode = m_OpenList[0];
            for (int i = 1; i < m_OpenList.Count; ++i)
            {
                if (m_OpenList[i].F <= m_CurrNode.F && m_OpenList[i].H < m_CurrNode.H)
                    m_CurrNode = m_OpenList[i];
            }

            m_OpenList.Remove(m_CurrNode);
            m_EndList.Add(m_CurrNode);

            if (m_CurrNode == m_EndNode)
            {
                Node CurrEndNode = m_EndNode;
                while(CurrEndNode != m_StartNode)
                {
                    m_FinalNodeList.Add(CurrEndNode);
                    CurrEndNode = CurrEndNode.m_ParentNode;
                }
                m_FinalNodeList.Add(m_StartNode);
                m_FinalNodeList.Reverse();
            }

            AddOpneList(m_CurrNode.x + 1, m_CurrNode.y + 1);
            AddOpneList(m_CurrNode.x - 1, m_CurrNode.y + 1);
            AddOpneList(m_CurrNode.x - 1, m_CurrNode.y - 1);
            AddOpneList(m_CurrNode.x + 1, m_CurrNode.y - 1);
            AddOpneList(m_CurrNode.x, m_CurrNode.y + 1);
            AddOpneList(m_CurrNode.x + 1, m_CurrNode.y);
            AddOpneList(m_CurrNode.x, m_CurrNode.y - 1);
            AddOpneList(m_CurrNode.x - 1, m_CurrNode.y);
        }
        return m_FinalNodeList;
    }
    
    // ���ο� ��� �ĺ��� �߰��ϴ� �Լ�
    private void AddOpneList(int _xPos, int _yPos)
    {
        if (_xPos >= 0 && _xPos < m_xSize && _yPos >= 0 && _yPos < m_ySize && !m_NodePos[_xPos, _yPos].mbIsWall && !m_EndList.Contains(m_NodePos[_xPos, _yPos]))
        {
            if (m_NodePos[m_CurrNode.x, _yPos].mbIsWall || m_NodePos[_xPos, m_CurrNode.y].mbIsWall) return;
            Node nextNode = m_NodePos[_xPos, _yPos];
            int moveCost = m_CurrNode.G + (m_CurrNode.x - _xPos == 0 || m_CurrNode.y - _yPos == 0 ? 10 : 14);

            if (moveCost < nextNode.G || !m_OpenList.Contains(nextNode))
            {
                nextNode.G = moveCost;
                nextNode.H = (Mathf.Abs(nextNode.x - m_EndNode.x) + Mathf.Abs(nextNode.y - m_EndNode.y)) * 10;
                nextNode.m_ParentNode = m_CurrNode;

                m_OpenList.Add(nextNode);
            }
        }
    }
}
