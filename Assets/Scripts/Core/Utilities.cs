using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class Utilities
{
    public static T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() ?? obj.AddComponent<T>();
    }

    #region Cost Color
    private static string SetCostColor(ChampionCost championCost)
    {
        switch (championCost)
        {
            case ChampionCost.OneCost:
                return "#C3C2C5"; // ȸ��
            case ChampionCost.TwoCost:
                return "#93DCC3"; // ���
            case ChampionCost.ThreeCost:
                return "#56BAF8"; // �Ķ���
            case ChampionCost.FourCost:
                return "#D500FF"; // �����
            case ChampionCost.FiveCost:
                return "#FFD150"; // �����
            default:
                return "��ϵ��� ���� è�Ǿ� �ܰ�";
        }
    }

    public static Color SetSlotColor(ChampionCost championCost)
    {
        ColorUtility.TryParseHtmlString(SetCostColor(championCost), out Color color);
        return color;
    }
    #endregion


    #region Cost IntValue
    private static int SetCost(ChampionCost championCost)
    {
        switch (championCost)
        {
            case ChampionCost.OneCost:
                return 1;
            case ChampionCost.TwoCost:
                return 2;
            case ChampionCost.ThreeCost:
                return 3; 
            case ChampionCost.FourCost:
                return 4; 
            case ChampionCost.FiveCost:
                return 5;
            default:
                return 0;
        }
    }

    public static int SetSlotCost(ChampionCost championCost)
    {
        int cost = SetCost(championCost);
        return cost;
    }
    #endregion

    #region ItemAttribute Description

    private static string SetDescription(ItemAttributeType iType)
    {
        switch (iType)
        {
            case ItemAttributeType.AD_Power:
                return "���ݷ�";
            case ItemAttributeType.AD_Speed:
                return "���� �ӵ�";
            case ItemAttributeType.AD_Defense:
                return "����";
            case ItemAttributeType.AP_Power:
                return "�ֹ���";
            case ItemAttributeType.AP_Defense:
                return "���� ���׷�";
            case ItemAttributeType.Mana:
                return "����";
            case ItemAttributeType.HP:
                return "ü��";
            case ItemAttributeType.CriticalPercent:
                return "ġ��Ÿ Ȯ��";
            case ItemAttributeType.BloodSuck:
                return "��� ���� ����";
            case ItemAttributeType.Total_Power:
                return "���ط�";
            case ItemAttributeType.Special:
                return "������";
            default:
                return "��ϵ��� ���� Ÿ��";
        }
    }

    public static string SetItemAttributeDescription(ItemAttributeType iType)
    {
        string desc = SetDescription(iType);
        return desc;
    }
    #endregion
}
