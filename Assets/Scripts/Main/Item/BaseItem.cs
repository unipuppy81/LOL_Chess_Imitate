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



    // ���� �Լ� ȣ��
    public virtual void CheckCombine()
    {

    }

    // ������ �ɷ�ġ ����

    public virtual void ApplyItemStats()
    {

    }

    // ������ ���� �Ӽ� ���� �� ����
    public virtual void ApplyUniqueItem()
    {

    }
}
