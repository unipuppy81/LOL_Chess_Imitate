using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChampionBlueprint", menuName = "Blueprints/ChampionBlueprint")]
public class ChampionBlueprint : ScriptableObject
{
    [Header("Champion Info")]
    [SerializeField] private string championName;
    [SerializeField] private GameObject championObj;
    [SerializeField] private ChampionLine championLine_First;
    [SerializeField] private ChampionLine championLine_Second;
    [SerializeField] private ChampionJob championJob;
    [SerializeField] private ChampionCost championCost;


    [Header("Enemy Stats")]
    [SerializeField] private long damage;
    [SerializeField] private long level;        // µî±Þ
    [SerializeField] private long count;
    [SerializeField] private long range;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float moveSpeed;


    public string ChampionName => championName;

    public GameObject ChampionObj => championObj;

    public ChampionLine ChampionLine_First => championLine_First;

    public ChampionLine ChampionLine_Second => championLine_Second;

    public ChampionJob ChampionJob => championJob;

    public ChampionCost ChampionCost => championCost;

    public long Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public long Level
    {
        get { return level; }
        set { level = value; }
    }

    public long Count
    {
        get { return count; }
        set { count = value; }
    }

    public long Range
    {
        get { return range; }
        set { range = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
}

