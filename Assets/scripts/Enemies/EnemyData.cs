using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public string id;
    public int scenceIndex;
    public bool survival;
    public float currentLingQi;
    public float maxLingQi;
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public Vector3 currentPosition;
    public EnemyData()
    {
        id = "";
        scenceIndex = 1;
        survival = true;
        maxLingQi = 400;
        currentLingQi = 400;
        maxHealth = 60;
        currentHealth = 60;
        moveSpeed = 1;
        currentPosition = new Vector3(0, 0, 0);
    }
    public EnemyData(string id, int scenceIndex, bool survival, float currentLingQi, float maxLingQi, float maxHealth, float curenttHealth, float moveSpeed, Vector3 currentPosition)
    {
        this.id = id;
        this.scenceIndex = scenceIndex;
        this.survival = survival;
        this.currentLingQi = currentLingQi;
        this.maxLingQi = maxLingQi;
        this.maxHealth = maxHealth;
        this.currentHealth = curenttHealth;
        this.moveSpeed = moveSpeed;
        this.currentPosition = currentPosition;
    }
    public void ShowInControlwindow()
    {
        Debug.Log("id" + id);
        Debug.Log("scenceIndex" + scenceIndex);
        Debug.Log("survival" + survival);
        Debug.Log("currentLingQi" + currentLingQi);
        Debug.Log("maxLingQi" + maxLingQi);
        Debug.Log("maxHealth" + maxHealth);
        Debug.Log("curenttHealth" + currentHealth);
        Debug.Log("moveSpeed" + moveSpeed);
        Debug.Log("currentPosition" + currentPosition);
    }
}
