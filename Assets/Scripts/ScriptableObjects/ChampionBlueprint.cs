using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChampionBlueprint", menuName = "Blueprints/ChampionBlueprint")]
public class ChampionBlueprint : ScriptableObject
{
    [Header("Champion Info")]
    [SerializeField] private string championName;
    [SerializeField] private GameObject championObj;
    [SerializeField] private ChampionLine championLine_First;
    [SerializeField] private ChampionLine championLine_Second;
    [SerializeField] private ChampionJob championJob_First;
    [SerializeField] private ChampionJob championJob_Second;
    [SerializeField] private ChampionCost championCost;


    [Header("Enemy Stats")]
    [SerializeField] private List<ChampionLevelData> championLevelData;
    [SerializeField] private float attack_Speed;
    [SerializeField] private long ad_Defense;
    [SerializeField] private long ap_Defense;
    [SerializeField] private float speed;
    [SerializeField] private float mana_Total;
    [SerializeField] private float mana_Cur;
    [SerializeField] private int attack_Range;
    [SerializeField] private SkillBlueprint skillBlueprint;

    private float hp_Total;
    private float hp_Cur;
    private int purchase_Cost;
    private int sell_Cost;



    public string ChampionName => championName;

    public GameObject ChampionObj => championObj;

    public ChampionLine ChampionLine_First => championLine_First;

    public ChampionLine ChampionLine_Second => championLine_Second;

    public ChampionJob ChampionJob_First => championJob_First;

    public ChampionJob ChampionJob_Second => championJob_Second;

    public ChampionCost ChampionCost => championCost;
}

