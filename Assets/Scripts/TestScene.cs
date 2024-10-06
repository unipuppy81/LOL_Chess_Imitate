using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestScene : UIBase
{
    [SerializeField] private List<GameObject> championSlot;
    [SerializeField] private Button btn_Reroll;

    private bool isLoadComplete = false;

    private void Start()
    {
        Manager.Asset.LoadAllAsync((count, totalCount) =>
        {
            if (count >= totalCount)
            {
                isLoadComplete = true;
                Debug.Log("Complete");
            }
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var obj = Manager.Asset.InstantiatePrefab("ChampionFrame");
            obj.transform.position = transform.position;

            if(obj == null)
            {
                Debug.Log("Null");
            }
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            Init();
        }
    }

    public new void Init()
    {
        Debug.Log("Init");
        SetUI<Button>();

        SetButtonEvent("Btn_Reroll", UIEventType.Click, UpdateChampionSlot);
    }

    private void UpdateChampionSlot(PointerEventData enterEvent)
    {
        // 랜덤 설계도 가져오기
        //var randomEnemyName = StageConfig.Enemies[Random.Range(0, StageConfig.Enemies.Length)];
        //var enemyBlueprint = Manager.Asset.GetBlueprint(randomEnemyName) as EnemyBlueprint;
    }
}
