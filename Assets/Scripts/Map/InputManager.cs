using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Unit selectedUnit;
    void Start()
    {

    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // ���� ����
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null)
                {
                    selectedUnit = unit;
                    Debug.Log("���� ���õ�: " + unit.name);
                }
                else if (selectedUnit != null)
                {
                    // Ÿ�� ���� �� ���� �̵�
                    HexTile tile = hit.collider.GetComponent<HexTile>();
                    if (tile != null)
                    {
                        Debug.Log("Ÿ�� ���õ�: " + tile.name + ", isOccupied: " + tile.isOccupied);
                        if (!tile.isOccupied)
                        {
                            selectedUnit.PlaceOnTile(tile);
                            Debug.Log("���� �̵�: " + selectedUnit.name + " -> " + tile.name);
                            selectedUnit = null;
                        }
                        else
                        {
                            Debug.Log("Ÿ���� �̹� �����Ǿ� �ֽ��ϴ�.");
                        }
                    }
                }
            }
        }*/
    }
}
