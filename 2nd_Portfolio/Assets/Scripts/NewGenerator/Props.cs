using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Props : AbstractProps
{
    private HashSet<Vector2Int> propPos;
    [SerializeField] private int maxCount = 15;
    Vector2Int indexPosition = Vector2Int.zero;
    protected override void SetPropsPosition()
    {

    }

    public HashSet<Vector2Int> SetPropRandomVector2Pos(HashSet<Vector2Int> _placeablePosition)
    {
        //Debug.Log("���ӽ� ��ũ��Ʈ ����");
        List<Vector2Int> positionList = new List<Vector2Int>(_placeablePosition);
        int index;
        propPos = _placeablePosition;
        while (propPos.Count > maxCount)
        {
            index = Random.Range(0, _placeablePosition.Count);
            /*Debug.Log("index + " +index);
            Debug.Log("positionList[index] = " + positionList[index]);
            Debug.Log("propPos.Count = "+propPos.Count);*/
            propPos.Remove(positionList[index]);
            positionList.RemoveAt(index);
            //Debug.Log("�̰� �� �ȵ�???");
        }
        return propPos;
    }
    public void SetPropsCntFreely(int _PropsCnt)//�̰� �ȸԳ� ����
    {
        maxCount = _PropsCnt;
    }
    public HashSet<Vector2Int> SetTeleportTilePos(HashSet<Vector2Int> _PlaceablePosition, HashSet<Vector2Int> _NowPlacingTile)
    {
        propPos.Clear();
        //1.���⿡ ������ ���� ���� ��ġ �����ϰ� ������
        //2.�׷��� �ϴ� Ÿ���� ���� �޾ƿͼ� 0,0���� ���� �ָ� �ִ� Ÿ���� �˻��ؼ� �ű�ٰ� �ڷ���Ʈ �������� ��ġ�ϰ�,
        //3.�� �ڷ���Ʈ �����ǿ� -1�� ���� ���� ���� �ű⿡ ���� ����� Ÿ���� �˻��ؼ� ��ŸƮŸ���� ��ġ�ϰ�,
        //4.�ڷ���Ʈ ������ ��ó�� ��ġ�� �� �ִ� ���� ��ġ�� ���� ���ڸ� ��ġ�ؾߵ�
        //_PlaceablePosition = ��ġ�� �� �ִ� ��� �ٴ� Ÿ��
        //_NowPlacingTile = ���� ��ġ�Ǿ� �ִ� �ٴ� Ÿ��

        List<Vector2Int> ListPlacingTile = new List<Vector2Int>(_NowPlacingTile);//��ġ���ɿ���Ÿ���� ����Ʈ�� �����
        //1
        int index = 0;
        while(index < ListPlacingTile.Count)
        {
            _PlaceablePosition.Remove(ListPlacingTile[index]);
            ++index;
        }//��������� ��ġ�� �� �ִ� Ÿ���� �ľ��ϴ� �뵵, _PlaceablePosition�� ������
        List<Vector2Int> ListPlaceablePos = new List<Vector2Int>(_PlaceablePosition);

        //2
        Vector2Int originZeroPosition = Vector2Int.zero;//���� ����
        Vector2Int originFarPosition = Vector2Int.zero;//������ �Ÿ����ؼ� ������ ����
        float distance = 0;
        float compareDistance = 0;

        //�Ÿ� �� ���� �����
        for (index =0; index<ListPlaceablePos.Count;index++)
        {
            compareDistance = Vector2Int.Distance(ListPlaceablePos[index], originZeroPosition);//���� �Ÿ� ���
            if(distance<compareDistance)//����Ǿ� �ִ� �Ÿ����� ������ ����� �Ÿ��񱳰�꺸�� �۴ٸ�
            {
                originFarPosition = ListPlaceablePos[index];//�������������ǿ� ���� �����ϰ�
                distance = compareDistance;//�Ÿ��� �񱳰Ÿ��� ����
            }
        }//���� ����� ���� �ָ� �ִ� ��ġ�� originFarPosition�� ����� ����
        originFarPosition += Vector2Int.left;//Ÿ�� ũ�� ������ ��ġ ����
        propPos.Add(originFarPosition);//��ġ�� Ÿ�Ͽ� �ڷ���Ʈ ������ �߰�
        return propPos;
    }

    public HashSet<Vector2Int> SetStartTilePosition(
        HashSet<Vector2Int> _TeleportPosition, HashSet<Vector2Int> _PlaceablePosition)
    {
        //Debug.Log("�Լ� ����");
        //Debug.Log("������ ���� ���� Ȯ�� "+_TeleportPosition.Count);
        //Debug.Log("_PlaceablePosition ������ ���� ���� Ȯ�� " + _PlaceablePosition.Count);

        //�ڷ���Ʈ �����ǿ� -1,-1�� ���� ���ؼ� �ݴ� ��ġ�� ã��,
        //�ű⿡ ���� ����� PlaceablePosition�� ã��,
        //ã�� ��ġ�� ��ŸƮ ���������� ����
        List<Vector2Int> ListTeleportPos = new List<Vector2Int>(_TeleportPosition);//�ؽø� ����Ʈ�� ��ȯ
        List<Vector2Int> ListPlaceablePos = new List<Vector2Int>(_PlaceablePosition);

        propPos.Remove(ListTeleportPos[0]);//�Ƹ��� �ʱ�ȭ

        Vector2Int MultipleMinusTeleportPosition=Vector2Int.zero;//-1 ���Ѱ� ������ ����
        for(int i=0; i< ListTeleportPos.Count;i++)
        {
            //Debug.Log("-1 �� �� ����");
            MultipleMinusTeleportPosition.x = ListTeleportPos[i].y * -1;
            MultipleMinusTeleportPosition.y = ListTeleportPos[i].x * -1;
        }
        //Debug.Log("ListTeleportPos.x" + ListTeleportPos[0].x);
        //Debug.Log("ListTeleportPos.y" + ListTeleportPos[0].y);
        //Debug.Log("MultipleMinusTeleportPosition.x" + MultipleMinusTeleportPosition.x);
        //Debug.Log("MultipleMinusTeleportPosition.y" + MultipleMinusTeleportPosition.y);
        Vector2Int FarPosition = MultipleMinusTeleportPosition;//�Ÿ����ؼ� ������ ����
        
        float distance = 10000;
        float compareDistance = 0;

        //�Ÿ� �� ���� �����
        for (int index = 0; index < ListPlaceablePos.Count; index++)
        {
            //Debug.Log("�Ÿ��� �Լ� ����");
            compareDistance = Vector2Int.Distance(ListPlaceablePos[index], FarPosition);//���� �Ÿ� ���
            if (distance > compareDistance)//����Ǿ� �ִ� �Ÿ����� ������ ����� �Ÿ��񱳰�꺸�� �۴ٸ�
            {
               indexPosition = ListPlaceablePos[index];//�ε��������ǿ� ���� �����ϰ�
               distance = compareDistance;//�Ÿ��� �񱳰Ÿ��� ����
            }
        }
        indexPosition += Vector2Int.up;
        indexPosition += Vector2Int.right;
        propPos.Add(indexPosition);
        //Debug.Log("���� �Ϸ�");
        return propPos;
    }
    public Vector2 GetStartPos()
    {
        Vector2 startPos = indexPosition;
        return startPos;
    }
    public HashSet<Vector2Int> SetItemChestTile(
        HashSet<Vector2Int> _TeleportPos, HashSet<Vector2Int> _StartPos, HashSet<Vector2Int> _PlaceablePosition)
    {
        List<Vector2Int> ListStartPos = new List<Vector2Int>(_StartPos);
        List<Vector2Int> ListTeleportPos = new List<Vector2Int>(_TeleportPos);
        List<Vector2Int> ListPlaceablePos = new List<Vector2Int>(_PlaceablePosition);
        propPos.Remove(ListStartPos[0]);
        propPos.Remove(ListTeleportPos[0]);

        //Debug.Log("ListTeleportPos =" + ListTeleportPos[0]);

        Vector2Int chestTile = ListTeleportPos[0];
        Vector2Int downChestTile = chestTile + (Vector2Int.down*2);
        int index = 0;
        while(index<ListPlaceablePos.Count)
        {
            if (downChestTile == ListPlaceablePos[index])
            {
                //Debug.Log("aaaaa");
                chestTile += Vector2Int.left; 
                break;
            }
            else 
            {
                chestTile = downChestTile;
            }
            index++;
        }
        //chestTile += Vector2Int.right;//��ġ����
        propPos.Add(chestTile);
        return propPos;
    }
}