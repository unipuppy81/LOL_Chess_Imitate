using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataBluePrint", menuName = "Blueprints/StageDataBluePrint")]
public class StageDataBluePrint : ScriptableObject
{
    public int stageNumber;        // 스테이지 번호
    public int totalRounds;        // 총 라운드 수
    public int baseDamage;         // 라운드 패배 시 기본 피해량
    public int damagePerEnemyUnit; // 남은 적 유닛 당 추가 피해량

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
