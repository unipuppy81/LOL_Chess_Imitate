using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private ItemDataContainerBlueprint itemDataBase;
    private Dictionary<string, ItemBlueprint> itemDataDictionary;
    public Dictionary<string, ItemBlueprint> ItemDataDictionary => itemDataDictionary;


    private List<ItemBlueprint> totalItems; // 모든 조합 아이템
    private List<ItemBlueprint> nomralItem;
    private List<ItemBlueprint> combineItem;
    private List<ItemBlueprint> usingItem;
    private List<ItemBlueprint> symbolItem;



    private Dictionary<(BaseItem, BaseItem), BaseItem> combinations;


    public void Init()
    {
        totalItems = new List<ItemBlueprint>();
        nomralItem = new List<ItemBlueprint>();
        combineItem = new List<ItemBlueprint>();
        usingItem = new List<ItemBlueprint>();  
        symbolItem = new List<ItemBlueprint>();

        combinations = new Dictionary<(BaseItem, BaseItem), BaseItem>();
        itemDataDictionary = new Dictionary<string, ItemBlueprint>();

        ParseItemData();
    }


    #region ItemDataMethod

    public void ParseItemData()
    {
        itemDataBase = Manager.Asset.GetBlueprint("ItemDataContainer") as ItemDataContainerBlueprint;
        foreach (var itemData in itemDataBase.ItemDatas)
        {
            itemDataDictionary.Add(itemData.ItemName, itemData);

            nomralItem = itemDataBase.FindItemType('A');
            combineItem = itemDataBase.FindItemType('B');
            symbolItem = itemDataBase.FindItemType('C');
        }
    }
    #endregion

    public void InitDic()
    {
        combinations = new Dictionary<(BaseItem, BaseItem), BaseItem>();
        // 조합 추가
        

    }

    public BaseItem GetCombinationResult(BaseItem item1, BaseItem item2)
    {
        if (combinations.TryGetValue((item1, item2), out BaseItem result))
        {
            return result;
        }
        return null; // 조합이 없는 경우
    }
}


[System.Serializable]
public class ItemAttribute
{
    public ItemAttributeType ItemAttributeType;
    public float AttributeValue;
}
