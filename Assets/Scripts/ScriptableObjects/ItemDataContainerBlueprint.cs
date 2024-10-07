using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemDataContainer", menuName = "Blueprints/ItemDataContainer")]
public class ItemDataContainerBlueprint : ScriptableObject
{
    [SerializeField] private List<ItemBlueprint> itemDatas = new();

    public List<ItemBlueprint> ItemDatas => itemDatas;

    public List<ItemBlueprint> FindItemType(ItemType type)
    {
        return itemDatas.Where(item => item.ItemType == type).ToList();
    }
}

[System.Serializable]
public class ItemBlueprint
{
    [Header("Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private string itemId;
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string description;
    [SerializeField] private ItemType itemType;
    [SerializeField] private List<ItemAttribute> itemAttribute;

    public Image Icon => icon;
    public string ItemId => itemId;
    public string ItemName => itemName;
    public string Description => description;
    public ItemType ItemType => itemType;
    public List<ItemAttribute> Attribute => itemAttribute;
}
