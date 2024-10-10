using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageDataBluePrint[] stages; // 각 스테이지 정보를 담는 ScriptableObject 배열

    private int currentStage = 0;  // 현재 스테이지
    private int currentRound = 1;  // 현재 라운드

    void Start()
    {
        StartStage(currentStage);
    }

    // 스테이지 시작
    public void StartStage(int stageIndex)
    {
        currentStage = stageIndex;
        currentRound = 1; // 라운드 1부터 시작

        Debug.Log($"Stage {stages[stageIndex].stageNumber} 시작. 총 {stages[stageIndex].totalRounds} 라운드");
    }

    // 라운드 클리어
    public void CompleteRound()
    {
        currentRound++;
        if (currentRound > stages[currentStage].totalRounds)
        {
            if (currentStage < stages.Length - 1)
            {
                // 다음 스테이지로 이동
                currentStage++;
                StartStage(currentStage);
            }
            else
            {
                Debug.Log("모든 스테이지 클리어");
            }
        }
        else
        {
            Debug.Log($"라운드 {currentRound} 진행 중.");
        }
    }

    // 패배 시 데미지 계산
    public int CalculateDamage(int remainingEnemyUnits)
    {
        StageDataBluePrint currentStageData = stages[currentStage];
        int totalDamage = currentStageData.baseDamage + (currentStageData.damagePerEnemyUnit * remainingEnemyUnits);
        return totalDamage;
    }
}
