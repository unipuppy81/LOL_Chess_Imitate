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
    public Material hexMaterial;
    public Material rectMaterial;

    private Mesh hexMesh;
    private Mesh quadMesh;

    public float gapBetweenTiles = 0.1f; // Ÿ�� ���� ������ ����

    void Start()
    {
        CalculateTileSize();
        GenerateMeshes();
        GenerateMap();
        AdjustCamera();
        CreatePlayerUnits();
    }

    void CalculateTileSize()
    {
        float hexWidth = Mathf.Sqrt(3); // Ÿ�� ũ�Ⱑ 1�� ���� ���� Ÿ�� ��
        float totalHexWidth = hexWidth * width + hexWidth / 2f; // Ÿ�� ũ�Ⱑ 1�� ���� ��ü �� ���� ũ��

        tileSize = desiredMapWidth / totalHexWidth; // ���ϴ� �� ���� ũ�⿡ �°� tileSize ���
    }

    void GenerateMeshes()
    {
        // ���� �޽� ����
        hexMesh = HexagonMeshGenerator.GenerateHexagonMesh(tileSize);

        // ���簢�� Ÿ���� ���� ���̸� ����
        float rectWidthSize = Mathf.Sqrt(3) * tileSize; // ���� Ÿ���� ���� �����ϰ�
        float rectHeightSize = tileSize * 2f * 0.75f; // ���� Ÿ���� ���� ���ݰ� �����ϰ�

        // ���簢�� �޽� ����
        quadMesh = GenerateQuadMesh(rectWidthSize, rectHeightSize);
    }

    Mesh GenerateQuadMesh(float width, float height)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        vertices[0] = new Vector3(-halfWidth, 0, -halfHeight);
        vertices[1] = new Vector3(halfWidth, 0, -halfHeight);
        vertices[2] = new Vector3(-halfWidth, 0, halfHeight);
        vertices[3] = new Vector3(halfWidth, 0, halfHeight);

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    void GenerateMap()
    {
        float hexWidth = Mathf.Sqrt(3) * tileSize;
        float hexHeight = tileSize * 2f; // ������ ����

        // ���� ��ü ũ�� ���
        float mapWidthSize = hexWidth * width + hexWidth / 2f;
        float mapHeightSize = hexHeight * (height - 1) * 0.75f;

        // �׸����� �߾��� �������� ������ ���
        float xOffset = mapWidthSize / 2f - hexWidth / 2f;
        float zOffset = mapHeightSize / 2f;

        // ���� Ÿ�� ����
        for (int r = 0; r < height; r++)
        {
            int numCols = width; // ��� �࿡�� Ÿ�� ������ �����ϰ� ����

            for (int q = 0; q < numCols; q++)
            {
                float xPos = q * hexWidth + (r % 2) * (hexWidth / 2f) - xOffset;
                float zPos = r * (hexHeight * 0.75f) - zOffset;

                Vector3 position = new Vector3(xPos, 0, zPos);

                CreateHexTile(position, q, r, hexMesh, hexMaterial);
            }
        }

        // �� ���� �� �Ʒ��� ���簢�� Ÿ�� ����
        CreateRectangularRow(-1, -hexHeight * 0.75f - zOffset);
        CreateRectangularRow(height, hexHeight * 0.75f * (height) - zOffset);
    }

    void CreateHexTile(Vector3 position, int q, int r, Mesh mesh, Material material)
    {
        GameObject tile = new GameObject($"Hex_{q}_{r}");
        tile.transform.position = position;
        tile.transform.parent = this.transform;

        MeshFilter meshFilter = tile.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = tile.AddComponent<MeshRenderer>();
        meshRenderer.material = material;

        meshFilter.mesh = mesh;

        MeshCollider meshCollider = tile.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        HexTile hexTile = tile.AddComponent<HexTile>();
        hexTile.q = q;
        hexTile.r = r;
        hexTile.s = -q - r;
    }

    void CreateRectangularRow(int row, float zPos)
    {
        float hexWidth = Mathf.Sqrt(3) * tileSize;

        // ���簢�� Ÿ���� ��ü �� ���
        float rectRowWidth = hexWidth * rectWidth;
        float xOffset = rectRowWidth / 2f - hexWidth / 2f;

        float zOffset = (tileSize * 2f * 0.75f) / 2f; // ������ Ÿ�� ������ ����

        if (row == -1)
            zPos -= zOffset + gapBetweenTiles;
        else if (row == height)
            zPos += zOffset + gapBetweenTiles;

        for (int x = 0; x < rectWidth; x++)
        {
            float xPos = x * hexWidth - xOffset;

            Vector3 position = new Vector3(xPos, 0, zPos);

            GameObject tile = new GameObject($"Rect_{x}_{row}");
            tile.transform.position = position;
            tile.transform.parent = this.transform;

            MeshFilter meshFilter = tile.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = tile.AddComponent<MeshRenderer>();
            meshRenderer.material = rectMaterial;

            meshFilter.mesh = quadMesh;

            MeshCollider meshCollider = tile.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = quadMesh;

            HexTile hexTile = tile.AddComponent<HexTile>();
            hexTile.isRectangularTile = true;
        }
    }

    void AdjustCamera()
    {
        float hexWidth = Mathf.Sqrt(3) * tileSize;
        float hexHeight = tileSize * 2f;

        // ���� ��ü ũ�� ���
        float mapWidthSize = hexWidth * width + hexWidth / 2f;
        float mapHeightSize = hexHeight * (height - 1) * 0.75f + hexHeight * 0.75f * 2f;

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
}
