using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private ItemDataContainerBlueprint itemDataBase;
    private Dictionary<string, ItemBlueprint> itemDataDictionary;





    private List<ItemBlueprint> totalItems; // ��� ���� ������
    private List<ItemBlueprint> nomralItem;
    private List<ItemBlueprint> combineItem;
    private List<ItemBlueprint> usingItem;
    private List<ItemBlueprint> symbolItem;



    #region Properties

    public Dictionary<string, ItemBlueprint> ItemDataDictionary => itemDataDictionary;

    #endregion


    #region Init
    public void Init()
    {
        totalItems = new List<ItemBlueprint>();
        nomralItem = new List<ItemBlueprint>();
        combineItem = new List<ItemBlueprint>();
        usingItem = new List<ItemBlueprint>();  
        symbolItem = new List<ItemBlueprint>();

        itemDataDictionary = new Dictionary<string, ItemBlueprint>();

        ParseItemData();
    }

    #endregion

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
}


[System.Serializable]
public class ItemAttribute
{
    public ItemAttributeType ItemAttributeType;
    public float AttributeValue;
}
