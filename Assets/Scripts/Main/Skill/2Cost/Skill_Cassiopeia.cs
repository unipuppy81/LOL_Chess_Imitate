using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_Cassiopeia : BaseSkill
{
    public override void UseSkill(GameObject thisGameObject)
    {
        //GameObject champion = Manager.Game.MyChampionList.FirstOrDefault(obj => obj.name == "Champion_Cassiopeia");
        ChampionBase cBase = thisGameObject.GetComponent<ChampionBase>();
        SkillLevelData skillData = new SkillLevelData();

        cBase.IsEnchanceAttack = true;
        cBase.EnchancedAttackCount = 3;
        cBase.RemainingEnhancedAttacks = 3;

        skillData.Value = cBase.ChampionBlueprint.SkillBlueprint.SkillLevelData[0].Value;
        cBase.EnchancedDamage = skillData.Value[cBase.ChampionLevel];
    }
}
