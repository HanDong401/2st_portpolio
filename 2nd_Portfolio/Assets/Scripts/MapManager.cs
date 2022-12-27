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

    public int col = 8;//����, ���ο�
    public int row = 8;

    [SerializeField] private Count wallCount = new Count(5, 9);
    [SerializeField] private Count decoCount = new Count(1, 5);
    [SerializeField] private Count rockCount = new Count(5, 9);

    [SerializeField] private MapTile exit;
    [SerializeField] private MapTile[] floorTiles;//�� ���� �� ������ ���� ������Ʈ �迭��
    [SerializeField] private MapTile[] inGameWallTiles;
    [SerializeField] private MapTile[] decorationTiles;
    [SerializeField] private MapTile[] rockTiles;
    [SerializeField] private MapTile[] outerWallTiles;

    private Transform boardHolder;//���̾��Ű���� �� ������Ʈ���� ������ �θ� ������Ʈ
    private List<Vector3> gridPositions = new List<Vector3>();//��ġ����, �ش� ��ġ�� ������Ʈ�� �ִ��� ������ üũ

    void InitializeList()
    {
        gridPositions.Clear();
        for (int x = 1; x < col - 1; x++)//���� ��x�� ������� �׷����� 2�� ���� ���
        {
            for (int y = 1; y < row - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
                //-1�� �� ������ �����ڸ��� ���� ����� Ż���� ���� �� �����Ƿ�,
                //������ �� �ٷ� �� ĭ���� �̵��� �� ���� Ÿ���� ������ �ʵ��� �ϱ� ���ؼ���
            }
        }
    }
    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < col + 1; x++)//�ٴڰ� ��Ÿ���� �����ϱ� ���� 2�� ����
        {
            for (int y = -1; y < row + 1; y++)//-1�� +1�� �ܺ���
            {
                //0~floorTile ���̿��� �����ϰ� �����ϰ�, �� �迭�� ���� �������
                MapTile toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == col || y == -1 || y == row)//�ν��Ͻ�ȭ �� �ٱ� ��Ÿ�� ����
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                MapTile instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);//������ �ν��Ͻ��� �θ� ����
            }
        }
    }

    Vector3 RandomPos()//����Ʈ���� ���� ��ġ �̾ƿ��� �Լ�
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
            MapTile tileChoice = tileArray[Random.Range(0, tileArray.Length)];//����Ÿ�� ��ȯ
            Instantiate(tileChoice, randomPosition, Quaternion.identity);//��������

        }
    }

    public void SetUpScene(int _level)//���Ӻ��忡�� ������� �� ���� �Ŵ����� ȣ���� ����
    {
        BoardSetUp();
        InitializeList();
        LayoutObjectAtRandom(inGameWallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(decorationTiles, decoCount.minimum, decoCount.maximum);
        LayoutObjectAtRandom(rockTiles, rockCount.minimum, rockCount.maximum);
        //�ⱸ�� �׻� ������ ���� �����̱� ������, ���� ���� ����� �ٲ� �ش� ��ġ�� ����
        Instantiate(exit, new Vector3(col - 1, row - 1, 0f), Quaternion.identity);
    }
}
