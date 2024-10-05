using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public int maxItems = 8; // �ִ� 8���� �������� ���� �� ����
    private List<GameObject> items = new List<GameObject>();

    public void PlaceItem(GameObject item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            item.transform.position = GetItemPosition(items.Count - 1); // ������ ��ġ ����
            item.transform.SetParent(this.transform); // Ÿ���� �ڽ����� ����
        }
        else
        {
            Debug.Log("�� Ÿ�Ͽ� ���� �� �ִ� �������� �ִ�ġ�� �����߽��ϴ�.");
        }
    }

    private Vector3 GetItemPosition(int index)
    {
        float spacing = 0.5f; // ������ ���� ���� ����
        return new Vector3(spacing * (index % 4), 0, spacing * (index / 4)); // 2x4 �׸��� ���·� ������ ��ġ
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
