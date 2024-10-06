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

    private static string SetCostColor(ChampionCost itemTier)
    {
        switch (itemTier)
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
}
