using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private List<ItemBlueprint> totalItems; // ��� ���� ������
    private List<ItemBlueprint> nomralItem;
    private List<ItemBlueprint> combineItem;
    private List<ItemBlueprint> usingItem;
    private List<ItemBlueprint> symbolItem;
    private List<ItemBlueprint> specialItem;
    private List<ItemBlueprint> supportItem;


    private Dictionary<(Item, Item), Item> combinations;


    public void Init()
    {
        totalItems = new List<ItemBlueprint>();
    }

    public void InitDic()
    {
        combinations = new Dictionary<(Item, Item), Item>();
        // ���� �߰�
        

    }

    public Item GetCombinationResult(Item item1, Item item2)
    {
        if (combinations.TryGetValue((item1, item2), out Item result))
        {
            return result;
        }
        return null; // ������ ���� ���
    }
}


[System.Serializable]
public class ItemAttribute
{
    public ItemAttributeType ItemAttributeType;
    public float AttributeValue;
}
