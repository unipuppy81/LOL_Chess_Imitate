using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageDataBluePrint[] stages; // �� �������� ������ ��� ScriptableObject �迭

    private int currentStage = 0;  // ���� ��������
    private int currentRound = 1;  // ���� ����

    void Start()
    {
        StartStage(currentStage);
    }

    // �������� ����
    public void StartStage(int stageIndex)
    {
        currentStage = stageIndex;
        currentRound = 1; // ���� 1���� ����

        Debug.Log($"Stage {stages[stageIndex].stageNumber} ����. �� {stages[stageIndex].totalRounds} ����");
    }

    // ���� Ŭ����
    public void CompleteRound()
    {
        currentRound++;
        if (currentRound > stages[currentStage].totalRounds)
        {
            if (currentStage < stages.Length - 1)
            {
                // ���� ���������� �̵�
                currentStage++;
                StartStage(currentStage);
            }
            else
            {
                Debug.Log("��� �������� Ŭ����");
            }
        }
        else
        {
            Debug.Log($"���� {currentRound} ���� ��.");
        }
    }

    // �й� �� ������ ���
    public int CalculateDamage(int remainingEnemyUnits)
    {
        StageDataBluePrint currentStageData = stages[currentStage];
        int totalDamage = currentStageData.baseDamage + (currentStageData.damagePerEnemyUnit * remainingEnemyUnits);
        return totalDamage;
    }
}
