using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillBlueprint", menuName = "Blueprints/SkillBlueprint")]
public class SkillBlueprint : ScriptableObject
{
    [Header("Skill Info")]
    [SerializeField] private string skillName;
    [SerializeField] private SkillType skillType;
    [SerializeField] private string description;
    [SerializeField] List<SkillLevelData> skillLevelData;

}
