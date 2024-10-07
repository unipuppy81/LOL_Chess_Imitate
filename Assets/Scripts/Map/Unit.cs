using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public HexTile currentTile;
    private Vector3 offset;
    //private bool isDragging = false;



    public void PlaceOnTile(HexTile tile)
    {
        if (currentTile != null)
        {
            // ���� Ÿ�Ͽ��� ���� ����
            currentTile.isOccupied = false;
        }

        currentTile = tile;
        currentTile.isOccupied = true;

        transform.position = tile.transform.position + new Vector3(0, 0.5f, 0); // ������ Ÿ�� ���� �ø�
    }
}
