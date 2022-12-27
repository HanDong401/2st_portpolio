using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [System.Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int col = 8;//세로, 가로열
    public int row = 8;

    [SerializeField] private Count wallCount = new Count(5, 9);
    [SerializeField] private Count decoCount = new Count(1, 5);
    [SerializeField] private Count rockCount = new Count(5, 9);

    [SerializeField] private MapTile exit;
    [SerializeField] private MapTile[] floorTiles;//맵 만들 때 설정할 게임 오브젝트 배열들
    [SerializeField] private MapTile[] inGameWallTiles;
    [SerializeField] private MapTile[] decorationTiles;
    [SerializeField] private MapTile[] rockTiles;
    [SerializeField] private MapTile[] outerWallTiles;

    private Transform boardHolder;//하이어라키에서 위 오브젝트들을 관리할 부모 오브젝트
    private List<Vector3> gridPositions = new List<Vector3>();//위치추적, 해당 위치에 오브젝트가 있는지 없는지 체크

    void InitializeList()
    {
        gridPositions.Clear();
        for (int x = 1; x < col - 1; x++)//맵이 행x열 모양으로 그려지게 2중 포문 사용
        {
            for (int y = 1; y < row - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
                //-1을 한 이유는 가장자리에 벽이 생기면 탈출을 못할 수 있으므로,
                //무조건 벽 바로 앞 칸에는 이동할 수 없는 타일이 생기지 않도록 하기 위해서임
            }
        }
    }
    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < col + 1; x++)//바닥과 벽타일을 구성하기 위한 2중 포문
        {
            for (int y = -1; y < row + 1; y++)//-1과 +1은 외벽임
            {
                //0~floorTile 사이에서 랜덤하게 추출하고, 그 배열이 같게 만들어줌
                MapTile toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == col || y == -1 || y == row)//인스턴스화 할 바깥 벽타일 선택
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                MapTile instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);//생성된 인스턴스의 부모를 설정
            }
        }
    }

    Vector3 RandomPos()//리스트에서 랜덤 위치 뽑아오는 함수
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPos = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPos;
    }

    void LayoutObjectAtRandom(MapTile[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPos();
            MapTile tileChoice = tileArray[Random.Range(0, tileArray.Length)];//랜덤타일 소환
            Instantiate(tileChoice, randomPosition, Quaternion.identity);//동적생성

        }
    }

    public void SetUpScene(int _level)//게임보드에서 만들어질 때 게임 매니저가 호출할 것임
    {
        BoardSetUp();
        InitializeList();
        LayoutObjectAtRandom(inGameWallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(decorationTiles, decoCount.minimum, decoCount.maximum);
        LayoutObjectAtRandom(rockTiles, rockCount.minimum, rockCount.maximum);
        //출구는 항상 오른쪽 위로 고정이기 때문에, 게임 보드 사이즈가 바뀌어도 해당 위치에 있음
        Instantiate(exit, new Vector3(col - 1, row - 1, 0f), Quaternion.identity);
    }
}
