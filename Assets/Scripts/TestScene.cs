using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
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
    }
}
