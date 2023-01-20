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
        //Debug.Log("프롭스 스크립트 진입");
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
            //Debug.Log("이건 왜 안됨???");
        }
        return propPos;
    }
    public void SetPropsCntFreely(int _PropsCnt)//이거 안먹네 뭐지
    {
        maxCount = _PropsCnt;
    }
    public HashSet<Vector2Int> SetTeleportTilePos(HashSet<Vector2Int> _PlaceablePosition, HashSet<Vector2Int> _NowPlacingTile)
    {
        propPos.Clear();
        //1.여기에 위에서 계산된 저거 위치 제외하고 들어가야함
        //2.그러면 일단 타일을 먼저 받아와서 0,0에서 가장 멀리 있는 타일을 검사해서 거기다가 텔레포트 포지션을 설치하고,
        //3.그 텔레포트 포지션에 -1을 각각 곱한 다음 거기에 가장 가까운 타일을 검사해서 스타트타일을 설치하고,
        //4.텔레포트 포지션 근처에 설치할 수 있는 랜덤 위치에 보상 상자를 설치해야됨
        //_PlaceablePosition = 설치할 수 있는 모든 바닥 타일
        //_NowPlacingTile = 현재 설치되어 있는 바닥 타일

        List<Vector2Int> ListPlacingTile = new List<Vector2Int>(_NowPlacingTile);//배치가능여부타일을 리스트로 만든것
        //1
        int index = 0;
        while(index < ListPlacingTile.Count)
        {
            _PlaceablePosition.Remove(ListPlacingTile[index]);
            ++index;
        }//여기까지가 설치할 수 있는 타일을 파악하는 용도, _PlaceablePosition에 저장함
        List<Vector2Int> ListPlaceablePos = new List<Vector2Int>(_PlaceablePosition);

        //2
        Vector2Int originZeroPosition = Vector2Int.zero;//원점 변수
        Vector2Int originFarPosition = Vector2Int.zero;//원점과 거리비교해서 저장할 변수
        float distance = 0;
        float compareDistance = 0;

        //거리 비교 포문 만들기
        for (index =0; index<ListPlaceablePos.Count;index++)
        {
            compareDistance = Vector2Int.Distance(ListPlaceablePos[index], originZeroPosition);//비교할 거리 계산
            if(distance<compareDistance)//저장되어 있는 거리값이 위에서 계산한 거리비교계산보다 작다면
            {
                originFarPosition = ListPlaceablePos[index];//오리진파포지션에 값을 저장하고
                distance = compareDistance;//거리에 비교거리값 저장
            }
        }//예상 결과는 가장 멀리 있는 위치가 originFarPosition에 저장될 것임
        originFarPosition += Vector2Int.left;//타일 크기 때문에 위치 조정
        propPos.Add(originFarPosition);//설치할 타일에 텔레포트 포지션 추가
        return propPos;
    }

    public HashSet<Vector2Int> SetStartTilePosition(
        HashSet<Vector2Int> _TeleportPosition, HashSet<Vector2Int> _PlaceablePosition)
    {
        //Debug.Log("함수 진입");
        //Debug.Log("들어오고 나서 개수 확인 "+_TeleportPosition.Count);
        //Debug.Log("_PlaceablePosition 들어오고 나서 개수 확인 " + _PlaceablePosition.Count);

        //텔레포트 포지션에 -1,-1을 각각 곱해서 반대 위치를 찾고,
        //거기에 가장 가까운 PlaceablePosition을 찾고,
        //찾은 위치를 스타트 포지션으로 지정
        List<Vector2Int> ListTeleportPos = new List<Vector2Int>(_TeleportPosition);//해시를 리스트로 변환
        List<Vector2Int> ListPlaceablePos = new List<Vector2Int>(_PlaceablePosition);

        propPos.Remove(ListTeleportPos[0]);//아마도 초기화

        Vector2Int MultipleMinusTeleportPosition=Vector2Int.zero;//-1 곱한걸 저장할 변수
        for(int i=0; i< ListTeleportPos.Count;i++)
        {
            //Debug.Log("-1 포 문 진입");
            MultipleMinusTeleportPosition.x = ListTeleportPos[i].y * -1;
            MultipleMinusTeleportPosition.y = ListTeleportPos[i].x * -1;
        }
        //Debug.Log("ListTeleportPos.x" + ListTeleportPos[0].x);
        //Debug.Log("ListTeleportPos.y" + ListTeleportPos[0].y);
        //Debug.Log("MultipleMinusTeleportPosition.x" + MultipleMinusTeleportPosition.x);
        //Debug.Log("MultipleMinusTeleportPosition.y" + MultipleMinusTeleportPosition.y);
        Vector2Int FarPosition = MultipleMinusTeleportPosition;//거리비교해서 저장할 변수
        
        float distance = 10000;
        float compareDistance = 0;

        //거리 비교 포문 만들기
        for (int index = 0; index < ListPlaceablePos.Count; index++)
        {
            //Debug.Log("거리비교 함수 진입");
            compareDistance = Vector2Int.Distance(ListPlaceablePos[index], FarPosition);//비교할 거리 계산
            if (distance > compareDistance)//저장되어 있는 거리값이 위에서 계산한 거리비교계산보다 작다면
            {
               indexPosition = ListPlaceablePos[index];//인덱스포지션에 값을 저장하고
               distance = compareDistance;//거리에 비교거리값 저장
            }
        }
        indexPosition += Vector2Int.up;
        indexPosition += Vector2Int.right;
        propPos.Add(indexPosition);
        //Debug.Log("공정 완료");
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
        //chestTile += Vector2Int.right;//위치보정
        propPos.Add(chestTile);
        return propPos;
    }
}