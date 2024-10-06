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
                return "#C3C2C5"; // 회색
            case ChampionCost.TwoCost:
                return "#93DCC3"; // 녹색
            case ChampionCost.ThreeCost:
                return "#56BAF8"; // 파란색
            case ChampionCost.FourCost:
                return "#D500FF"; // 보라색
            case ChampionCost.FiveCost:
                return "#FFD150"; // 노란색
            default:
                return "등록되지 않은 챔피언 단계";
        }
    }

    public static Color SetSlotColor(ChampionCost championCost)
    {
        ColorUtility.TryParseHtmlString(SetCostColor(championCost), out Color color);
        return color;
    }
}
