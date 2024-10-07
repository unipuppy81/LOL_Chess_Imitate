using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour
{
    #region SerializeField 
    [Header("Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private ItemType itemType;
    [SerializeField] private List<ItemAttribute> itemAttributes;


    #endregion


    #region Properity

    public Image Icon => icon;
    public string ItemName => itemName;
    public string Description => description;
    public ItemType ItemType => itemType;
    public List<ItemAttribute> ItemAttributes => itemAttributes;

    #endregion



    // 조합 함수 호출
    public virtual void CheckCombine()
    {

    }

    // 아이템 능력치 적용

    public virtual void ApplyItemStats()
    {

    }

    // 아이템 고유 속성 적용 및 구현
    public virtual void ApplyUniqueItem()
    {

    }
}
