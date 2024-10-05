using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    public int playerGold = 0;
    public int enemyGold = 0;
    public int maxGoldSlots = 5;

    public void UpdateGoldDisplay()
    {
        int playerGoldSlots = Mathf.Min(playerGold / 10, maxGoldSlots);
        int enemyGoldSlots = Mathf.Min(enemyGold / 10, maxGoldSlots);

        // 플레이어 골드 표시 업데이트
        for (int i = 0; i < maxGoldSlots; i++)
        {
            GameObject slot = GameObject.Find($"PlayerGoldSlot_{i}");
            slot.SetActive(i < playerGoldSlots);
        }

        // 적 골드 표시 업데이트
        for (int i = 0; i < maxGoldSlots; i++)
        {
            GameObject slot = GameObject.Find($"EnemyGoldSlot_{i}");
            slot.SetActive(i < enemyGoldSlots);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
