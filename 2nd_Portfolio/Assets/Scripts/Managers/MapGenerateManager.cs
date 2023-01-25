using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour
{
    public delegate void MapGenerateEvent();
    private MapGenerateEvent m_MapGenerateEvent = null;
    public delegate void LoadSceneEvent(string _sceneName);
    private LoadSceneEvent m_LoadsceneEvent = null;
    [SerializeField] RoomFirstDungeonGenerator roomFirstDungeonGenerator = null;
    [SerializeField] MapTileVisualizer MapTileVisualizer= null;
    [SerializeField] Props props = null;
    [SerializeField] SpecialTileInstantiator specialTileInstantiator = null;

    [SerializeField] int DungeonLevel = 0;

    public void MapGenerateManagerAwake()
    {
        if (roomFirstDungeonGenerator == null)
            roomFirstDungeonGenerator = GameObject.FindObjectOfType<RoomFirstDungeonGenerator>();
    }
    public void DungeonGenerate()
    {
        MapTileVisualizer.Clear();
        roomFirstDungeonGenerator.PlusDungeonLevel();
        DungeonLevel = roomFirstDungeonGenerator.ReturnDungeonLevel();
        //���⿡ �÷��̾� ��ġ ��ŸƮ ��ġ�� ���� �Լ� ȣ��
        if (DungeonLevel >= 3 && DungeonLevel<4)
        {
            //������ ���� 1.
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(20,15);
            roomFirstDungeonGenerator.SetDungeonWidthHeightBossRoom();
            //props.SetPropsCntFreely(0);
        }
        else if(DungeonLevel < 3)
        {
            roomFirstDungeonGenerator.SetDungeonWidthHeightFreely(Random.Range(25, 40), Random.Range(10, 20));//�ִ� ũ�� �����ϴ� �Լ�
            roomFirstDungeonGenerator.SetDungeonMinWidthHeight(5, 5);//�� �ּ�ũ�� �����ϴ� �Լ�
            //props.SetPropsCntFreely(15);
        }
        else if(DungeonLevel>=4)
        {
            roomFirstDungeonGenerator.SetDungeonLevelZero();

            //���⼭ �� ��ȯ ���ͼ� Ÿ�� ������ �̵�
            if (m_LoadsceneEvent != null)
                m_LoadsceneEvent("TownScene");
            return;
        }
        roomFirstDungeonGenerator.GenerateDungeon();
        m_MapGenerateEvent?.Invoke();
    }
    public Vector2 GetStartPos()
    {
        return roomFirstDungeonGenerator.GetStartPos();
    }
    
    public void AddMapGenerateEvent(MapGenerateEvent _callback)
    {
        m_MapGenerateEvent = _callback;
    }

    public void AddLoadSceneEvent(LoadSceneEvent _callback)
    {
        m_LoadsceneEvent = _callback;
    }
}
