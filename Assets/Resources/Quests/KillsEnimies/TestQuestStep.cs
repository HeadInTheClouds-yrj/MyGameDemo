using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestQuestStep : QuestStep
{
    protected override void SetStepState(string newState)
    {
        
    }
    [SerializeField]
    private GameObject OgameObject;
    private Transform[] gamobjList;
    private Vector3[] vector3s;
    private bool isSpawn = false;
    // Start is called before the first frame update
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 && isSpawn)
        {
            Spawn();
            isSpawn = true;
            FinishQuestStep();
        }
    }
    void Spawn()
    {
        gamobjList = OgameObject.GetComponentsInChildren<Transform>();
        vector3s = new Vector3[gamobjList.Length];
        for (int i = 0; i < gamobjList.Length; i++)
        {
            vector3s[i] = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        }
        for (int i = 0; i < gamobjList.Length; i++)
        {
            if (i != 0)
            {
                NpcManager.instance.factoryNpc("npcs/Skeleton", gamobjList[i].transform, vector3s[i]);
            }


        }
    }

}
