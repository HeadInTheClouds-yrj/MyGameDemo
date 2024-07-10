using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestQuestStep : QuestStep
{
    protected override void SetStepState(string newState)
    {
        isSpawn = newState;
    }
    private Vector3[] vector3s;
    private string isSpawn = "0";
    private void OnEnable()
    {
        EventManager.Instance.enimiesEvent.OnEnimyDie += EnemyDead;
    }
    private void OnDisable()
    {
        EventManager.Instance.enimiesEvent.OnEnimyDie -= EnemyDead;

    }
    private void EnemyDead(NpcCell obj)
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 && NpcManager.instance.GetAllSceneNPCData()[5].Count <=1)
        {
            FinishQuestStep();
        }
    }

    // Start is called before the first frame update
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 && isSpawn.Equals("0"))
        {
            Spawn();
            isSpawn = "1";
            ChangeStepState(isSpawn);
        }

    }
    void Spawn()
    {
        vector3s = new Vector3[3];
        for (int i = 0; i < 3; i++)
        {
            vector3s[i] = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        }
        for (int i = 0; i < 3; i++)
        {

                NpcManager.instance.factoryNpc("npcs/enemy", vector3s[i]);


        }
    }

}
