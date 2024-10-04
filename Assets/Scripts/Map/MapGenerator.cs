using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 7;
    public int height = 8;
    public int rectWidth = 9; // 직사각형 타일의 개수를 9로 설정
    public float tileSize = 1f;
    public Material hexMaterial;
    public Material rectMaterial;

    private Mesh hexMesh;
    private Mesh quadMesh;

    public float gapBetweenTiles = 0.1f; // 타일 간격 조정용 변수

    void Start()
    {
        GenerateMeshes();
        GenerateMap();
        AdjustCamera();
        CreatePlayerUnits();
    }

    void GenerateMeshes()
    {
        // 헥사곤 메쉬 생성
        hexMesh = HexagonMeshGenerator.GenerateHexagonMesh(tileSize);

        // 직사각형 타일의 폭과 높이를 설정
        float rectWidthSize = Mathf.Sqrt(3) * tileSize; // 헥사곤 타일의 폭과 동일하게
        float rectHeightSize = tileSize * 2f * 0.75f; // 헥사곤 타일의 수직 간격과 동일하게

        // 직사각형 메쉬 생성
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
        float hexHeight = tileSize * 2f; // 헥사곤의 높이

        // 맵의 전체 크기 계산
        float mapWidthSize = hexWidth * (width + 0.5f);
        float mapHeightSize = hexHeight * (height * 0.75f + 0.25f);

        // 그리드의 중앙을 기준으로 오프셋 계산
        float xOffset = mapWidthSize / 2f - hexWidth / 2f;
        float zOffset = mapHeightSize / 2f - hexHeight / 2f;

        // 헥사곤 타일 생성
        for (int r = 0; r < height; r++)
        {
            bool isOffsetRow = r % 2 == 1; // 홀수 행 여부
            int numCols = width;

            for (int q = 0; q < numCols; q++)
            {
                float xPos = q * hexWidth + (isOffsetRow ? hexWidth / 2f : 0) - xOffset;
                float zPos = r * (hexHeight * 0.75f) - zOffset;

                Vector3 position = new Vector3(xPos, 0, zPos);

                CreateHexTile(position, q, r, hexMesh, hexMaterial);
            }
        }

        // 맨 위와 맨 아래에 직사각형 타일 생성
        CreateRectangularRow(-1, -zOffset - (hexHeight * 0.75f));
        CreateRectangularRow(height, -zOffset + height * (hexHeight * 0.75f));
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

        // 직사각형 타일의 전체 폭 계산
        float rectRowWidth = rectWidth * hexWidth;
        float xOffset = rectRowWidth / 2f - hexWidth / 2f;

        // Z축 위치 조정 값
        float zOffset = (tileSize * 2f * 0.2f) / 2f; // 육각형 타일 높이의 절반

        // row 값에 따라 Z축 위치 조정
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

        // 맵의 전체 크기 계산
        float mapWidthSize = Mathf.Max(width * hexWidth + hexWidth / 2f, rectWidth * hexWidth);
        float mapHeightSize = (height + 2) * hexHeight * 0.75f + gapBetweenTiles * 2f;

        // 카메라의 중심 위치 계산
        Vector3 centerPosition = new Vector3(0, 0, 0);

        // 카메라 위치 설정
        float cameraHeight = 50f; // 필요에 따라 조정
        Camera.main.transform.position = centerPosition + new Vector3(0, cameraHeight, 0);
        Camera.main.transform.rotation = Quaternion.Euler(80f, 0f, 0f);

        // 카메라 투영 방식 설정
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = mapHeightSize / 1.5f;

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
}
