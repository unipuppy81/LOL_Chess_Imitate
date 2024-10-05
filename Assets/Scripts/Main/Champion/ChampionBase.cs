using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionBase : MonoBehaviour
{
    private ChampionBlueprint championBlueprint;
    private SkillBlueprint skillBlueprint;
    private GameObject skillObject;
    private BaseSkill baseSkill;

    private string championName;
    private ChampionLine line_first;
    private ChampionLine line_second;
    private ChampionJob job_first;
    private ChampionJob job_second;
    private ChampionCost cost;

    private int championLevel;
    private int curHp;
    private int maxHp;
    private int power;

    private float attack_Speed;
    private float ad_Defense;
    private float ap_Defense;
    private float speed;
    private float curMana;
    private float maxMana;
    private int attack_Range;

    private int purchase_Cost;
    private int sell_Cost;


    #region Init
    public void SetChampion(ChampionBlueprint blueprint, Vector3 position, long hpWeight, long atkWeight, long goldWeight)
    {
        championBlueprint = blueprint;
        skillBlueprint = blueprint.SkillBlueprint;
        skillObject = blueprint.SkillBlueprint.SkillObject;
        baseSkill = blueprint.SkillBlueprint.SkillObject.GetComponent<BaseSkill>();

        championName = blueprint.ChampionName;
        line_first = blueprint.ChampionLine_First;
        line_second = blueprint.ChampionLine_Second;
        job_first = blueprint.ChampionJob_First;
        job_second = blueprint.ChampionJob_Second;
        cost = blueprint.ChampionCost;

        power = blueprint.ChampionLevelData[0].Power;
        maxHp = blueprint.ChampionLevelData[0].Hp;
        curHp = maxHp;

        attack_Speed = blueprint.AttackSpeed;
        ad_Defense = blueprint.AD_Defense;
        ap_Defense = blueprint.AP_Defense;
        speed = blueprint.Speed;
        curMana = blueprint.Mana_Cur;
        maxMana = blueprint.Mana_Total;
        attack_Range = blueprint.Attack_Range;

        purchase_Cost = 1;
        sell_Cost = purchase_Cost * championLevel - 1;
    }

    #endregion

    #region Unity Flow
    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    #endregion

    #region Attack 

    public void NormalAttack()
    {

    }

    public void SkillAttack()
    {
        if (baseSkill == null)
            return;

        baseSkill.Skill();
    }


    #endregion
}
