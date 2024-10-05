using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemBlueprint", menuName = "Blueprints/ItemBlueprint")]
public class ItemBlueprint : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private ItemType itemType;
    [SerializeField] private List<ItemAttribute> itemAttribute;
    

    public Image Icon => icon;
    public string ItemName => itemName;
    public string Description => description;
    public ItemType ItemType => itemType;
    public List<ItemAttribute> Attribute => itemAttribute;
}