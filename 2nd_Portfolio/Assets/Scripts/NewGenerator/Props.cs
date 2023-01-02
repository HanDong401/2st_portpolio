using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Props : AbstractProps
{
    [SerializeField] private HashSet<Vector2Int> propPos;
    [SerializeField] private int maxCount = 15;
    protected override void SetPropsPosition()
    {

    }

    public HashSet<Vector2Int> SetPropRandomVector2Pos(HashSet<Vector2Int> _placeablePosition)
    {
        Debug.Log("프롭스 스크립트 진입");
        List<Vector2Int> positionList = new List<Vector2Int>(_placeablePosition);
        int index;
        propPos = _placeablePosition;
        while (propPos.Count > maxCount)
        {
            index = Random.Range(0, _placeablePosition.Count);
            //    Debug.Log("index + " +index);
            //    Debug.Log("positionList[index] = " + positionList[index]);
            //    Debug.Log("propPos.Count = "+propPos.Count);
            propPos.Remove(positionList[index]);
            positionList.RemoveAt(index);
            Debug.Log("이건 왜 안됨???");
        }
        //for (int i=0; i < propPos.Count - maxCount;i++)
        //{
        //    index = Random.Range(0,_placeablePosition.Count);
        //    propPos.Remove(positionList[index]);
        //    positionList.RemoveAt(index);
        //    Debug.Log("잘됨" + propPos.Count);
        //}
        Debug.Log("리턴 프롭 포지션 진입");
        return propPos;
    }
}
