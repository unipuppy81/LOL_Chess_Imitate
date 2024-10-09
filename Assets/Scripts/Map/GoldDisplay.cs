using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    private int playerGold = 0;
    private int enemyGold = 0;

    [SerializeField]
    private List<GameObject> PlayerGoldList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> EnemyGoldList = new List<GameObject>();

    public int currentGold = 0;

    private int maxGold = 5;

    // Start is called before the first frame update
    void Start()
    {
        GoldListAdd();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGoldTiles();
        //Debug.Log(activeGoldTiles);
    }

    private void GoldListAdd()
    {
        for (int i = 0; i < maxGold; i++)
        {
            GameObject leftTile = GameObject.Find($"PlayerGold_{i}");
            if (leftTile != null)
            {
                PlayerGoldList.Add(leftTile);
                leftTile.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
            }

            GameObject rightTile = GameObject.Find($"EnemyGold_{i}");
            if (rightTile != null)
            {
                EnemyGoldList.Add(rightTile);
                rightTile.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
            }
        }
    }

    private void UpdateGoldTiles()
    {
        // Ȱ��ȭ�� ��� Ÿ���� �� ���
        int activePlayerGold = Mathf.Clamp(playerGold / 10, 0, maxGold);
        int activeEnemyGold = Mathf.Clamp(enemyGold / 10, 0, maxGold);

        // ���� ��� Ÿ�� ������Ʈ
        for (int i = 0; i < PlayerGoldList.Count; i++)
        {
            if (i < activePlayerGold)
                PlayerGoldList[i].SetActive(true);
            else
                PlayerGoldList[i].SetActive(false);
        }

        // ������ ��� Ÿ�� ������Ʈ
        for (int i = 0; i < EnemyGoldList.Count; i++)
        {
            int index = EnemyGoldList.Count - 1 - i; // ��������
            if (i < activeEnemyGold)
                EnemyGoldList[index].SetActive(true);
            else
                EnemyGoldList[index].SetActive(false);
        }
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        playerGold = Mathf.Clamp(playerGold, 0, maxGold);
        UpdateGoldTiles();
    }

    public void SpendGold(int amount)
    {
        playerGold -= amount;
        playerGold = Mathf.Clamp(playerGold, 0, maxGold);
        UpdateGoldTiles();
    }
}
