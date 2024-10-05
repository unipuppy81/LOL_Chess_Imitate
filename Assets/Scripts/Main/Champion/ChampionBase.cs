using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionBase : MonoBehaviour
{
    [SerializeField] private Canvas UICanvas;

    #region Fields
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


    private Rigidbody rigid;
    private ChampionView championView;
    

    #endregion

    #region Init

    /// <summary>
    /// blueprint로 UI 생성하고 클릭해서 구매하면 SetChampion 호출
    /// </summary>
    /// <param name="blueprint"></param>
    /// <param name="position"></param>
    /// <param name="hpWeight"></param>
    /// <param name="atkWeight"></param>
    /// <param name="goldWeight"></param>
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

    public void SetHpBar()
    {

    }

    public void ResetHealth()
    {
        curHp = maxHp;
    }

    #endregion

    #region Unity Flow
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        championView = GetComponent<ChampionView>();
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    #endregion

    #region Attack Method

    public void CreateNormalAttack()
    {

    }

    private IEnumerator AttackRoutine()
    {
        yield return null;
    }

    public void SkillAttack()
    {
        if (baseSkill == null)
            return;

        baseSkill.Skill();
    }

    public void FloatingDamage(Vector3 position, int damage)
    {

    }

    #endregion

    #region Health Method

    public void TakeDamage(int damage)
    {

    }

    private void Die()
    {

    }

    #endregion
}
