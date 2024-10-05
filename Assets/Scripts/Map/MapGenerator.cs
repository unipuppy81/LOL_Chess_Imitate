using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 7;
    public int height = 8;
    public int rectWidth = 9; // ���簢�� Ÿ���� ����
    public float desiredMapWidth = 20f; // ���ϴ� ���� ���� ũ�� (����: ����Ƽ ���� ��ǥ)
    public float tileSize; // Ÿ���� ũ�� (�ڵ� ���� ����)

    public GameObject hexTilePrefab; // ���� Ÿ�� ������
    public GameObject rectTilePrefab; // ���簢�� Ÿ�� ������
    public GameObject itemTilePrefab; // ������ Ÿ�� ������

    public float gapBetweenTiles = 0.1f; // Ÿ�� ���� ������ ����

    private float rectWidthSize; // �簢�� Ÿ�� ��

    public int maxGoldSlots = 5; // �ִ� ��� ǥ�� ĭ ��
    public float goldSlotSize = 1f; // ��� ǥ�� ĭ�� ũ��
    public float goldSlotSpacing = 0.1f; // ��� ǥ�� ĭ ���� ����

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
        float hexWidth = Mathf.Sqrt(3); // Ÿ�� ũ�Ⱑ 1�� ���� ���� Ÿ�� ��
        float totalHexWidth = hexWidth * width + hexWidth / 2f; // Ÿ�� ũ�Ⱑ 1�� ���� ��ü �� ���� ũ��

        tileSize = desiredMapWidth / totalHexWidth; // ���ϴ� �� ���� ũ�⿡ �°� tileSize ���

        float rectWidthRatio = 0.90f; // ���簢�� Ÿ�� ���� ���� (��: 80%)
        rectWidthSize = (Mathf.Sqrt(3) * tileSize) * rectWidthRatio; // ���簢�� Ÿ���� ��
    }

    void GenerateMap()
    {
        float hexWidth = Mathf.Sqrt(3) * tileSize;
        float hexHeight = tileSize * 2f; // ������ ����

        // ���� ��ü ũ�� ���
        mapWidthSize = hexWidth * width - hexWidth / 2f;
        mapHeightSize = hexHeight * (height - 1) * 0.75f;

        // �׸����� �߾��� �������� ������ ���
        float xOffset = mapWidthSize / 2f - hexWidth / 2f;
        float zOffset = mapHeightSize / 2f;

        // ���� Ÿ�� ����
        for (int r = 0; r < height; r++)
        {
            int numCols = width; // ��� �࿡�� Ÿ�� ������ �����ϰ� ����

            for (int q = 0; q < numCols; q++)
            {
                float xPos = q * hexWidth - (r % 2) * (hexWidth / 2f) - xOffset;
                float zPos = r * (hexHeight * 0.75f) - zOffset;

                Vector3 position = new Vector3(xPos, 0, zPos);

                CreateHexTile(position, q, r);
            }
        }

        // �� ���� �� �Ʒ��� ���簢�� Ÿ�� ����
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

        float zOffset = (tileSize * 2f * 0.75f) / 2f; // ������ Ÿ�� ������ ����

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

        // ���� ��ü ũ�� ���
        mapWidthSize = Mathf.Max(hexWidth * width + hexWidth / 2f, rectWidthSize * rectWidth);
        mapHeightSize = hexHeight * (height - 1) * 0.75f + hexHeight * 0.75f * 2f;

        // ī�޶��� �߽� ��ġ ���
        Vector3 centerPosition = new Vector3(0, 0, 0);

        // ī�޶� ��ġ ����
        float cameraHeight = Mathf.Max(mapWidthSize, mapHeightSize); // �� ũ�⿡ ���� ī�޶� ���� ����
        Camera.main.transform.position = centerPosition + new Vector3(0, 18f, -21f);
        Camera.main.transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        // ī�޶� ���� ��� ����
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = cameraHeight / 1.8f;

        // ī�޶� Ŭ���� �÷��� ����
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

            // �� �Ʒ� ���簢�� Ÿ���� �����ɴϴ�.
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

        // �������� ���� �� �ִ� ������Ʈ �߰�
        ItemHolder itemHolder = tile.AddComponent<ItemHolder>();
        itemHolder.maxItems = 8; // �ִ� 8���� �������� ���� �� �ֵ��� ����
    }

    void CreateCornerTiles()
    {
        float cornerTileOffset = rectWidthSize * 0.6f; // �� �����ڸ����� �ణ ����߸��� ���� ������
        float adjustedTileOffset = rectWidthSize; // ������ ����� Ÿ�� ������

        // ----------------------
        // ���� �Ʒ� �ڳ�
        // ----------------------

        // �⺻ �ڳ� Ÿ��
        Vector3 bottomLeftPos = new Vector3(-mapWidthSize / 2f - cornerTileOffset, 0, -mapHeightSize / 2f - cornerTileOffset + 1f);
        CreateCornerTile(bottomLeftPos, 0);

        // �������� �ϳ� �߰� (���� �Ʒ� �ڳ� ����)
        Vector3 bottomLeftPosLeft = new Vector3(bottomLeftPos.x - adjustedTileOffset, bottomLeftPos.y, bottomLeftPos.z);
        CreateCornerTile(bottomLeftPosLeft, 2); // Ÿ�� ID 2

        // �Ʒ��� �ϳ� �߰� (�߰��� ���� Ÿ�� ����)
        Vector3 bottomLeftPosDown = new Vector3(bottomLeftPosLeft.x, bottomLeftPosLeft.y, bottomLeftPosLeft.z - 2.309f);
        CreateCornerTile(bottomLeftPosDown, 3); // Ÿ�� ID 3

        // ----------------------
        // ������ �� �ڳ�
        // ----------------------

        // �⺻ �ڳ� Ÿ��
        Vector3 topRightPos = new Vector3(mapWidthSize / 2f + cornerTileOffset, 0, mapHeightSize / 2f + cornerTileOffset - 1f);
        CreateCornerTile(topRightPos, 1);

        // ���������� �ϳ� �߰� (������ �� �ڳ� ����)
        Vector3 topRightPosRight = new Vector3(topRightPos.x + adjustedTileOffset, topRightPos.y, topRightPos.z);
        CreateCornerTile(topRightPosRight, 4); // Ÿ�� ID 4

        // ���� �ϳ� �߰� (�߰��� ������ Ÿ�� ����)
        Vector3 topRightPosUp = new Vector3(topRightPosRight.x, topRightPosRight.y, topRightPosRight.z + 2.309f);
        CreateCornerTile(topRightPosUp, 5); // Ÿ�� ID 5
    }

    void CreateItemsOnCornerTiles()
    {
        for (int i = 0; i < 2; i++) // �ڳʰ� 2���ϱ� �� �� �ݺ�
        {
            GameObject itemObj = Instantiate(itemTilePrefab); // ������ �������� ����
            itemObj.name = $"Item_{i}";

            // �ڳ� Ÿ�� ��������
            ItemHolder cornerTile = GameObject.Find($"CornerTile_{i}").GetComponent<ItemHolder>();
            cornerTile.PlaceItem(itemObj); // �������� Ÿ�Ͽ� ��ġ
        }
    }
}
