using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionManager
{
    private Dictionary<string, ChampionBlueprint> championDataDictionary;

    #region Init
    public void Init()
    {
        championDataDictionary = new Dictionary<string, ChampionBlueprint>();
        ParseItemData();
    }

    #endregion


    #region ChampionDataMethod
    private void ParseItemData()
    {

    }

    #endregion
}

[System.Serializable]
public class ChampionLevelData
{
    public int Level;        // µî±Þ
    public int Hp;
    public int Power;
}

[System.Serializable]
public class ChampionData
{
    public int Cost;
    public string[] Names;
}

[System.Serializable]
public class ChampionRandomData
{
    public int Level;
    public float[] Probability;
}

[System.Serializable]
public class ChampionMaxCount
{
    public int Cost;
    public int MaxCount;
}