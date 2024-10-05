using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public int maxItems = 8; // 최대 8개의 아이템을 놓을 수 있음
    private List<GameObject> items = new List<GameObject>();

    public void PlaceItem(GameObject item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            item.transform.position = GetItemPosition(items.Count - 1); // 아이템 위치 설정
            item.transform.SetParent(this.transform); // 타일의 자식으로 설정
        }
        else
        {
            Debug.Log("이 타일에 놓을 수 있는 아이템이 최대치에 도달했습니다.");
        }
    }

    private Vector3 GetItemPosition(int index)
    {
        float spacing = 0.5f; // 아이템 간의 간격 설정
        return new Vector3(spacing * (index % 4), 0, spacing * (index / 4)); // 2x4 그리드 형태로 아이템 배치
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
