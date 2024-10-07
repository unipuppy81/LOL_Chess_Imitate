using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour
{
    [Header("Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private ItemType itemType;

    public void CombineItem()
    {
        //Manager.Item.CombineItem();
    }
}
