using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemiesDataGroup
{
    public int currentNPCBelongScenceIndex;
    public List<string> ids;
    public SerializableDictionary<string,bool> isSurvivals;
    public SerializableDictionary<string,float> currentLingQis;
    public SerializableDictionary<string, float> maxLingQis;
    public SerializableDictionary<string, float> maxHealths;
    public SerializableDictionary<string, float> currentHealths;
    public SerializableDictionary<string, float> moveSpeeds;
    public SerializableDictionary<string, Vector3> currentPositions;
    public EnemiesDataGroup()
    {
        currentNPCBelongScenceIndex = 0;
        ids = new List<string>();
        isSurvivals = new SerializableDictionary<string, bool>();
        maxLingQis = new SerializableDictionary<string, float>();
        currentLingQis = new SerializableDictionary<string, float>();
        maxHealths = new SerializableDictionary<string, float>();
        currentHealths = new SerializableDictionary<string, float>();
        moveSpeeds = new SerializableDictionary<string, float>();
        currentPositions = new SerializableDictionary<string, Vector3>();
    }
    public void ImportEnemyDataByObject(EnemyData enemyData)
    {
        if (enemyData != null)
        {
            if (!ids.Contains(enemyData.id))
            {
                ids.Add(enemyData.id);
                isSurvivals.Add(enemyData.id, enemyData.survival);
                maxLingQis.Add(enemyData.id, enemyData.maxLingQi);
                maxHealths.Add(enemyData.id, enemyData.maxHealth);
                currentLingQis.Add(enemyData.id, enemyData.currentLingQi);
                currentHealths.Add(enemyData.id, enemyData.currentHealth);
                moveSpeeds.Add(enemyData.id, enemyData.moveSpeed);
                currentPositions.Add(enemyData.id, enemyData.currentPosition);
            }
            else
            {
                isSurvivals[enemyData.id] = enemyData.survival;
                maxLingQis[enemyData.id] = enemyData.maxLingQi;
                maxHealths[enemyData.id] = enemyData.maxHealth;
                currentLingQis[enemyData.id] = enemyData.currentLingQi;
                currentHealths[enemyData.id] = enemyData.currentHealth;
                moveSpeeds[enemyData.id] = enemyData.moveSpeed;
                currentPositions[enemyData.id] = enemyData.currentPosition;
            }
        }
        
        currentNPCBelongScenceIndex = enemyData.scenceIndex;
    }
    public void ImportEnemyDataByObject(List<EnemyData> enemyDatas)
    {
        ids.Clear();
        isSurvivals.Clear();
        maxLingQis.Clear();
        maxHealths.Clear();
        currentLingQis.Clear();
        currentHealths.Clear();
        moveSpeeds.Clear();
        currentPositions.Clear();
        foreach (EnemyData enemyData in enemyDatas)
        {
            currentNPCBelongScenceIndex = enemyData.scenceIndex;
            ids.Add(enemyData.id);
            isSurvivals.Add(enemyData.id,enemyData.survival);
            maxLingQis.Add(enemyData.id, enemyData.maxLingQi);
            maxHealths.Add(enemyData.id, enemyData.maxHealth);
            currentLingQis.Add(enemyData.id, enemyData.currentLingQi);
            currentHealths.Add(enemyData.id, enemyData.currentHealth);
            moveSpeeds.Add(enemyData.id, enemyData.moveSpeed);
            currentPositions.Add(enemyData.id, enemyData.currentPosition);
        }
    }
    public List<EnemyData> GetScenceEnemiesGroup()
    {
        List<EnemyData> enemies= new List<EnemyData>();
        for (int i = 0; i < ids.Count; i++)
        {
            enemies.Add(new EnemyData(this.ids[i], currentNPCBelongScenceIndex, isSurvivals[ids[i]],
                currentLingQis[ids[i]], maxLingQis[ids[i]], maxHealths[ids[i]], currentHealths[ids[i]],
                moveSpeeds[ids[i]], currentPositions[ids[i]]));
        }
        return enemies;
    }
}
