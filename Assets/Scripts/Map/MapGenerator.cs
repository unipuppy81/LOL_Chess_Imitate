using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 7;
    public int height = 8;
    public int rectWidth = 9; // 직사각형 타일의 개수
    public float desiredMapWidth = 20f; // 원하는 맵의 가로 크기 (단위: 유니티 월드 좌표)
    public float tileSize; // 타일의 크기 (자동 계산될 예정)

    public GameObject hexTilePrefab; // 헥사곤 타일 프리팹
    public GameObject rectTilePrefab; // 직사각형 타일 프리팹
    public GameObject itemTilePrefab; // 아이템 타일 프리팹

    public float gapBetweenTiles = 0.1f; // 타일 간격 조정용 변수

    private float rectWidthSize; // 사각형 타일 폭

    public int maxGoldSlots = 5; // 최대 골드 표시 칸 수
    public float goldSlotSize = 1f; // 골드 표시 칸의 크기
    public float goldSlotSpacing = 0.1f; // 골드 표시 칸 간의 간격

    private float mapWidthSize;
    private float mapHeightSize;

    void Start()
    {
        CalculateTileSize();
        //GenerateMeshes();
        GenerateMap();
        CreateCornerTiles();
        AdjustCamera();
        CreatePlayerUnits();
    }

    void CalculateTileSize()
    {
        float hexWidth = Mathf.Sqrt(3); // 타일 크기가 1일 때의 헥사곤 타일 폭
        float totalHexWidth = hexWidth * width + hexWidth / 2f; // 타일 크기가 1일 때의 전체 맵 가로 크기

        tileSize = desiredMapWidth / totalHexWidth; // 원하는 맵 가로 크기에 맞게 tileSize 계산

        float rectWidthRatio = 0.90f; // 직사각형 타일 폭의 비율 (예: 80%)
        rectWidthSize = (Mathf.Sqrt(3) * tileSize) * rectWidthRatio; // 직사각형 타일의 폭
    }

    void GenerateMap()
    {
        float hexWidth = Mathf.Sqrt(3) * tileSize;
        float hexHeight = tileSize * 2f; // 헥사곤의 높이

        // 맵의 전체 크기 계산
        mapWidthSize = hexWidth * width - hexWidth / 2f;
        mapHeightSize = hexHeight * (height - 1) * 0.75f;

        // 그리드의 중앙을 기준으로 오프셋 계산
        float xOffset = mapWidthSize / 2f - hexWidth / 2f;
        float zOffset = mapHeightSize / 2f;

        // 헥사곤 타일 생성
        for (int r = 0; r < height; r++)
        {
            int numCols = width; // 모든 행에서 타일 개수를 동일하게 설정

            for (int q = 0; q < numCols; q++)
            {
                float xPos = q * hexWidth - (r % 2) * (hexWidth / 2f) - xOffset;
                float zPos = r * (hexHeight * 0.75f) - zOffset;

                Vector3 position = new Vector3(xPos, 0, zPos);

                CreateHexTile(position, q, r);
            }
        }

        // 맨 위와 맨 아래에 직사각형 타일 생성
        CreateRectangularRow(-1, -hexHeight * 0.75f - zOffset);
        CreateRectangularRow(height, hexHeight * 0.75f * (height) - zOffset);
    }

    void CreateHexTile(Vector3 position, int q, int r)
    {
        GameObject tile = Instantiate(hexTilePrefab, position, Quaternion.identity, this.transform);
        tile.name = $"Hex_{q}_{r}";

        HexTile hexTile = tile.GetComponent<HexTile>();
        hexTile.q = q;
        hexTile.r = r;
        hexTile.s = -q - r;
    }

    void CreateRectangularRow(int row, float zPos)
    {
        float rectRowWidth = rectWidthSize * rectWidth;
        float xOffset = rectRowWidth / 2f - rectWidthSize / 2f;

        float zOffset = (tileSize * 2f * 0.75f) / 2f; // 육각형 타일 높이의 절반

        if (row == -1)
            zPos -= zOffset + gapBetweenTiles;
        else if (row == height)
            zPos += zOffset + gapBetweenTiles;

        for (int x = 0; x < rectWidth; x++)
        {
            float xPos = x * rectWidthSize - xOffset;

            Vector3 position = new Vector3(xPos, 0, zPos);

            GameObject tile = Instantiate(rectTilePrefab, position, Quaternion.identity, this.transform);
            tile.name = $"Rect_{x}_{row}";

            HexTile hexTile = tile.GetComponent<HexTile>();
            hexTile.isRectangularTile = true;
        }
    }

    void AdjustCamera()
    {
        float hexWidth = Mathf.Sqrt(3) * tileSize;
        float hexHeight = tileSize * 2f;

        // 맵의 전체 크기 계산
        mapWidthSize = Mathf.Max(hexWidth * width + hexWidth / 2f, rectWidthSize * rectWidth);
        mapHeightSize = hexHeight * (height - 1) * 0.75f + hexHeight * 0.75f * 2f;

        // 카메라의 중심 위치 계산
        Vector3 centerPosition = new Vector3(0, 0, 0);

        // 카메라 위치 설정
        float cameraHeight = Mathf.Max(mapWidthSize, mapHeightSize); // 맵 크기에 따라 카메라 높이 조정
        Camera.main.transform.position = centerPosition + new Vector3(0, 18f, -21f);
        Camera.main.transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        // 카메라 투영 방식 설정
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = cameraHeight / 1.8f;

        // 카메라 클리핑 플레인 설정
        Camera.main.nearClipPlane = -10f;
        Camera.main.farClipPlane = 100f;
    }

    void CreatePlayerUnits()
    {
        for (int x = 0; x < rectWidth; x++)
        {
            GameObject unitObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            unitObj.name = $"Unit_{x}";
            unitObj.transform.localScale = new Vector3(0.5f, 1f, 0.5f);

            Unit unit = unitObj.AddComponent<Unit>();

            // 맨 아래 직사각형 타일을 가져옵니다.
            HexTile tile = GameObject.Find($"Rect_{x}_-1").GetComponent<HexTile>();

            unit.PlaceOnTile(tile);
        }
    }
    void CreateCornerTile(Vector3 position, int cornerId)
    {
        GameObject tile = Instantiate(rectTilePrefab, position, Quaternion.identity, this.transform);
        tile.name = $"CornerTile_{cornerId}";

        HexTile hexTile = tile.GetComponent<HexTile>();
        hexTile.isRectangularTile = true;

        // 아이템을 놓을 수 있는 컴포넌트 추가
        ItemHolder itemHolder = tile.AddComponent<ItemHolder>();
        itemHolder.maxItems = 8; // 최대 8개의 아이템을 놓을 수 있도록 설정
    }

    void CreateCornerTiles()
    {
        float cornerTileOffset = rectWidthSize * 0.6f; // 맵 가장자리에서 약간 떨어뜨리기 위한 오프셋
        float adjustedTileOffset = rectWidthSize; // 간격을 고려한 타일 오프셋

        // ----------------------
        // 왼쪽 아래 코너
        // ----------------------

        // 기본 코너 타일
        Vector3 bottomLeftPos = new Vector3(-mapWidthSize / 2f - cornerTileOffset, 0, -mapHeightSize / 2f - cornerTileOffset + 1f);
        CreateCornerTile(bottomLeftPos, 0);

        // 왼쪽으로 하나 추가 (왼쪽 아래 코너 기준)
        Vector3 bottomLeftPosLeft = new Vector3(bottomLeftPos.x - adjustedTileOffset, bottomLeftPos.y, bottomLeftPos.z);
        CreateCornerTile(bottomLeftPosLeft, 2); // 타일 ID 2

        // 아래로 하나 추가 (추가된 왼쪽 타일 기준)
        Vector3 bottomLeftPosDown = new Vector3(bottomLeftPosLeft.x, bottomLeftPosLeft.y, bottomLeftPosLeft.z - 2.309f);
        CreateCornerTile(bottomLeftPosDown, 3); // 타일 ID 3

        // ----------------------
        // 오른쪽 위 코너
        // ----------------------

        // 기본 코너 타일
        Vector3 topRightPos = new Vector3(mapWidthSize / 2f + cornerTileOffset, 0, mapHeightSize / 2f + cornerTileOffset - 1f);
        CreateCornerTile(topRightPos, 1);

        // 오른쪽으로 하나 추가 (오른쪽 위 코너 기준)
        Vector3 topRightPosRight = new Vector3(topRightPos.x + adjustedTileOffset, topRightPos.y, topRightPos.z);
        CreateCornerTile(topRightPosRight, 4); // 타일 ID 4

        // 위로 하나 추가 (추가된 오른쪽 타일 기준)
        Vector3 topRightPosUp = new Vector3(topRightPosRight.x, topRightPosRight.y, topRightPosRight.z + 2.309f);
        CreateCornerTile(topRightPosUp, 5); // 타일 ID 5
    }

    void CreateItemsOnCornerTiles()
    {
        for (int i = 0; i < 2; i++) // 코너가 2개니까 두 번 반복
        {
            GameObject itemObj = Instantiate(itemTilePrefab); // 아이템 프리팹을 생성
            itemObj.name = $"Item_{i}";

            // 코너 타일 가져오기
            ItemHolder cornerTile = GameObject.Find($"CornerTile_{i}").GetComponent<ItemHolder>();
            cornerTile.PlaceItem(itemObj); // 아이템을 타일에 배치
        }
    }
}
