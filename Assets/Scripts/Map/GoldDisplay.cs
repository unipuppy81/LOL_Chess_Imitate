using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    public int playerGold = 0;
    public int enemyGold = 0;

    [SerializeField]
    private List<GameObject> PlayerGoldList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> EnemyGoldList = new List<GameObject>();

    public int currentGold = 0;

    public int maxGoldSlots = 5;
    public bool isGold = false;

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

    void GoldListAdd()
    {
        for (int i = 0; i < maxGoldSlots; i++)
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

    public void UpdateGoldTiles()
    {
        // Ȱ��ȭ�� ��� Ÿ���� �� ���
        int activePlayerGold = Mathf.Clamp(playerGold / 10, 0, maxGoldSlots);
        int activeEnemyGold = Mathf.Clamp(enemyGold / 10, 0, maxGoldSlots);

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
}
