using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataBluePrint", menuName = "Blueprints/StageDataBluePrint")]
public class StageDataBluePrint : ScriptableObject
{
    public int stageNumber;        // �������� ��ȣ
    public int totalRounds;        // �� ���� ��
    public int baseDamage;         // ���� �й� �� �⺻ ���ط�
    public int damagePerEnemyUnit; // ���� �� ���� �� �߰� ���ط�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
