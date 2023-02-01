using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 타일 하나의 정보를 저장하고있는 노드
[Serializable]
public class Node
{
    // 노드 생성자 벽인지 아닌지 bool값과 좌표값을 초기화 함
    public Node(bool _isWall, int _x, int _y)
    {
        mbIsWall = _isWall;
        x = _x;
        y = _y;
    }

    public bool mbIsWall;
    public int x, y, G, H;
    public Node m_ParentNode;
    // G는 이동거리 H 는 현재위치에서 목적지까지의 거리 F 는 G와 H를 합한값
    public int F { get { return G + H; } }
}

// 길찾기 알고리즘
public class AStar
{
    // 시작좌표와 도착좌표를 저장할 변수
    private Vector2Int m_StartPos, m_EndPos;
    // 최종 경로 리스트
    private List<Node> m_FinalNodeList;
    public List<Node> FinalNodeList { get { return m_FinalNodeList; } }

    public Node[,] m_NodePos;
    private Node m_StartNode, m_EndNode, m_CurrNode;
    private List<Node> m_OpenList, m_EndList;

    private int m_xSize = 40;
    private int m_ySize = 20;

    // 맵전체의 좌표와 bool값을 초기화해서 Node형태로 저장함
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

    // 길찾기 함수
    public List<Node> PathFinding(Vector2 _startPos, Vector2 _endPos)
    {
        // 시작위치와 도착위치를 Vector2Int형식으로 변환
        m_StartPos = Vector2Int.RoundToInt(_startPos);
        m_EndPos = Vector2Int.RoundToInt(_endPos);

        // 위치를 노드방식으로 저장
        m_StartNode = m_NodePos[m_StartPos.x, m_StartPos.y];
        m_EndNode = m_NodePos[m_EndPos.x, m_EndPos.y];

        // 각 리스틀을 초기화
        m_OpenList = new List<Node> { m_StartNode };
        m_EndList = new List<Node>();
        m_FinalNodeList = new List<Node>();

        // 현재 저장중인 노드들을 검사하고 새로운 노드들을 추가
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
    
    // 새로운 노드 후보를 추가하는 함수
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
